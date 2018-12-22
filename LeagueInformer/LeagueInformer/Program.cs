using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using LeagueInformer.Models;
using LeagueInformer.Resources;
using LeagueInformer.Services;
using LeagueInformer.Utils;
using Newtonsoft.Json.Linq;

namespace LeagueInformer
{
    public class Program
    {
        //TODO dodać regiony dla większej czytelności w ramach porządkowania kodu
        private static readonly ConnectionService ConnectionService = new ConnectionService();
        private static readonly GetSummonerService SummonerService = new GetSummonerService();
        private static readonly GetMastersService MastersService = new GetMastersService();
        private static readonly GetLeagueOfSummoner LeagueOfSummonerService = new GetLeagueOfSummoner();
        private static readonly ServerService ServerService = new ServerService();
        private static readonly GetLeagueInfoService LeagueInfoService = new GetLeagueInfoService();
        private static readonly GetSummonerGame SummonerGameService = new GetSummonerGame();
        private static readonly FileHandler FileHandler = new FileHandler();
        private static readonly Spectator Spectator = new Spectator();
        private static readonly GetLastGamesService LastGameService = new GetLastGamesService();
        private static readonly DateHandler DateHandler = new DateHandler();

        public static void Main(string[] args)
        {
            if (ConnectionService.HasInternetConnection())
            {
                string option;
                do
                {
                    MaximizeConsoleWindow();
                    MainMenu();
                    option = Console.ReadLine();
                    Console.WriteLine(AppResources.Common_ChosenOption, Environment.NewLine, option);
                    switch (option)
                    {
                        case "1":
                            GetLeagueOfSummoner().Wait();
                            break;
                        case "2":
                            GetBestMasters().Wait();
                            break;
                        case "3":
                            GetSummonerLeagueInfo().Wait();
                            break;
                        case "4":
                            GetSummonerHistory().Wait();
                            break;
                        case "5":
                            GetSummonerGame().Wait();
                            break;
                        case "6":
                            GetServerStatus().Wait();
                            break;
                        case "7":
                            AboutApp();
                            break;
                        case "8":
                            Environment.Exit(1);
                            break;
                        default:
                            Console.WriteLine(AppResources.Common_OptionIsNotAvailable);
                            break;
                    }
                } while (option != null && option != "8");
            }
            else
            {
                Console.WriteLine(AppResources.Main_NoInternetConnection);
                ExitApp();
            }
        }

        private static void MainMenu()
        {
            int iterator = 1;
            Console.WriteLine();
            Console.WriteLine(AppResources.Main_WelcomeUser);
            Console.WriteLine(AppResources.Main_ChooseFunction);

            foreach (var menuOption in AppSettings.MenuOptions)
            {
                Console.WriteLine(AppResources.Common_TwoVerbatimStringWithDot, iterator, menuOption);
                iterator++;
            }

            Console.WriteLine();
        }

        private static void ExitApp()
        {
            Console.WriteLine(AppResources.Common_ExitApp);
            Console.ReadLine();
            Environment.Exit(1);
        }

        private static async Task GetLeagueOfSummoner()
        {
            var chosenServer = PrintListOfServers();

            if (!chosenServer.IsSuccess)
            {
                return;
            }

            string regionCode = chosenServer.RegionCode;

            Console.Write(AppResources.GetLeagueOfSummoner_EnterName);
            string summonerName = await PrintListOfSavedNicknames();

            if (string.IsNullOrEmpty(summonerName))
            {
                Console.WriteLine(AppResources.Error_SummonerNameCannotBeEmpty);
                return;
            }

            var summonerResponse = await SummonerService.GetInformationAboutSummoner(summonerName, regionCode);
            if (!summonerResponse.IsSuccess)
            {
                Console.WriteLine(
                    string.IsNullOrEmpty(summonerResponse.Message)
                    ? AppResources.Error_Undefined
                    : summonerResponse.Message);
                return;
            }

            string summonerId = summonerResponse.Id;
            var result = await LeagueOfSummonerService.GetLeagueOfSummonerInformation(summonerId, regionCode);

            if (!result.IsSuccess)
            {
                Console.WriteLine(
                    string.IsNullOrEmpty(result.Message)
                    ? AppResources.Error_Undefined
                    : result.Message);
                return;
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(result.IsSuccess ?
                $"{Environment.NewLine}Nazwa przywoływacza: {result.SummonerLeagueInfo.summonerName} " +
                $"{Environment.NewLine}Nazwa ligi: {result.SummonerLeagueInfo.leagueName} " +
                $"{Environment.NewLine}Tier: {result.SummonerLeagueInfo.tier} " +
                $"{Environment.NewLine}Ranga: {result.SummonerLeagueInfo.rank} " +
                $"{Environment.NewLine}Wygrane: {result.SummonerLeagueInfo.wins} " +
                $"{Environment.NewLine}Przegrane: {result.SummonerLeagueInfo.losses} " +
                $"{Environment.NewLine}Typ kolejki: {result.SummonerLeagueInfo.queueType}" : result.Message);
            Console.ResetColor();
        }

        private static async Task GetSummonerLeagueInfo()
        {
            var chosenServer = PrintListOfServers();

            if (!chosenServer.IsSuccess)
            {
                return;
            }

            string regionCode = chosenServer.RegionCode;

            Console.WriteLine(AppResources.GetSummonerLeagueInfo_GiveSummonerNick);

            string summonerName = await PrintListOfSavedNicknames();

            if (string.IsNullOrEmpty(summonerName))
            {
                Console.WriteLine(AppResources.Error_SummonerNameCannotBeEmpty);
                return;
            }

            var response = await LeagueInfoService.GetListOfSummonerLeague(summonerName, regionCode);

            if (!response.IsSuccess)
            {
                Console.WriteLine(AppResources.Error_Undefined);
                return;
            }

            var sortedMembers = response.LeagueDetailsResponseList.OrderByDescending(x => x.Points).ToList();
            int position = 1;
            Console.WriteLine(AppResources.Common_TwoVerbatimStrings,
                response.LeagueInfo.SummonerLeagueInfo.leagueName,
                response.LeagueInfo.SummonerLeagueInfo.rank);
            foreach (var member in sortedMembers)
            {
                Console.ForegroundColor = member.SummonerName == summonerName
                    ? ConsoleColor.Red
                    : ConsoleColor.White;

                Console.WriteLine(
                    AppResources.Common_StatisticsPatten,
                    position,
                    member.SummonerName,
                    member.Wins,
                    member.Losses,
                    member.Points);
                position++;
            }

            Console.ForegroundColor = ConsoleColor.White;
        }

        private static async Task<string> PrintListOfSavedNicknames()
        {
            try
            {
                int position = 1;
                Console.WriteLine(AppResources.PrintListOfSavedNicknames_Instruction, Environment.NewLine);

                var nicknamesList = FileHandler.GetListOfLastNicknames();
                if (nicknamesList.Any())
                {
                    foreach (var nickname in nicknamesList)
                    {
                        Console.WriteLine(AppResources.Common_TwoVerbatimStringWithDot, position, nickname);
                        position++;
                    }
                }

                Console.WriteLine(AppResources.PrintListOfSavedNicknames_Information, Environment.NewLine);

                string summonerName = Console.ReadLine();

                if (int.TryParse(summonerName, out int result))
                {
                    summonerName = nicknamesList[result - 1];
                }
                else
                {
                    await FileHandler.SaveNicknameToList(summonerName);
                }

                return summonerName;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        private static async Task GetSummonerHistory()
        {
            var chosenServer = PrintListOfServers();

            if (!chosenServer.IsSuccess)
            {
                return;
            }

            string regionCode = chosenServer.RegionCode;

            Console.WriteLine(AppResources.GetSummonerLeagueInfo_GiveSummonerNick);

            string summonerName = await PrintListOfSavedNicknames();

            if (string.IsNullOrEmpty(summonerName))
            {
                Console.WriteLine(AppResources.Error_SummonerNameCannotBeEmpty);
                return;
            }

            var summonerResponse = await SummonerService.GetInformationAboutSummoner(summonerName);
            if (!summonerResponse.IsSuccess)
            {
                Console.WriteLine(
                    string.IsNullOrEmpty(summonerResponse.Message)
                        ? AppResources.Error_Undefined
                        : summonerResponse.Message);
                return;
            }

            string accountId = summonerResponse.AccountId;

            var response = await LastGameService.GetLastTenGames(accountId, regionCode);

            if (!response.IsSuccess)
            {
                Console.WriteLine(
                    string.IsNullOrEmpty(summonerResponse.Message)
                        ? AppResources.Error_Undefined
                        : summonerResponse.Message);
                return;
            }

            var matchList = response.Games.GetRange(0, 10).ToList();
            int position = 1;

            Console.WriteLine(AppResources.GetSummonerHistory_LastTenMatchesInfo,
                Environment.NewLine,
                summonerName,
                Environment.NewLine);

            foreach (var match in matchList)
            {
                string lane = match.Lane != "NONE" ? match.Lane + ", " : string.Empty;
                Console.WriteLine(AppResources.GetSummonerHistory_MatchFormat,
                    position,
                    SummonerService.GetChampionForId(match.Champion),
                    lane,
                    DateHandler.ParseTimeToDate(match.Date));
                position++;
            }
        }

        private static async Task GetBestMasters()
        {
            var chosenServer = PrintListOfServers();

            if (!chosenServer.IsSuccess)
            {
                return;
            }

            string regionCode = chosenServer.RegionCode;

            var response = await MastersService.GetListOfMasterLeague(regionCode);

            if (!response.IsSuccess)
            {
                Console.WriteLine(AppResources.Error_Undefined);
                return;
            }

            var bestMasters = response.MastersResponseList.OrderByDescending(x => x.Points).ToList()
                .GetRange(1, 10);
            int position = 1;

            foreach (var master in bestMasters)
            {
                Console.WriteLine(
                    AppResources.Common_StatisticsPatten,
                    position,
                    master.SummonerName,
                    master.Wins,
                    master.Losses,
                    master.Points);
                Console.WriteLine(master.Veteran
                    ? AppResources.GetBestMasters_IsVeteran
                    : AppResources.GetBestMasters_IsNotVeteran);
                Console.WriteLine(master.HotStreak
                    ? AppResources.GetBestMasters_HasHotStreak
                    : AppResources.GetBestMasters_HasNotHotStreak);
                Console.WriteLine();
                position++;
            }
        }

        private static async Task GetServerStatus()
        {
            Console.WriteLine(AppResources.GetServerStatus_ChooseServerFromList, Environment.NewLine, Environment.NewLine);
            int position = 1;
            foreach (var serverName in AppSettings.ServerAddresses.Keys)
            {
                Console.WriteLine(AppResources.Common_TwoVerbatimStringWithDot,
                    position,
                    serverName);
                position++;
            }

            bool getPosition = int.TryParse(Console.ReadLine(), out int pos);
            if (!getPosition || pos > AppSettings.ServerAddresses.Count)
            {
                Console.WriteLine(AppResources.GetServerStatus_ParsingFailed);
                return;
            }

            bool getValue = AppSettings.ServerAddresses.TryGetValue(
                AppSettings.ServerAddresses.Keys.ElementAt(pos - 1), out string regionCode);
            if (!getValue)
            {
                Console.WriteLine(AppResources.Error_Undefined);
                return;
            }

            var response = await ServerService.GetServerStatus(regionCode);
            if (!response.IsSuccess)
            {
                Console.WriteLine(AppResources.Error_Undefined);
                return;
            }

            Console.WriteLine(AppResources.GetServerStatus_DataForServer, Environment.NewLine,
                response.Name, Environment.NewLine);

            foreach (var serviceStatus in response.ServicesStatuses)
            {
                Console.WriteLine(serviceStatus.Name);
                Console.ForegroundColor = serviceStatus.ServerStatusState == Enums.ServerStatus.Online
                    ? ConsoleColor.Green : ConsoleColor.Red;

                Console.WriteLine(serviceStatus.ServerStatusState);
                Console.ResetColor();
                Console.WriteLine();
            }
        }
        private static async Task GetSummonerGame()
        {
            var chosenServer = PrintListOfServers();
            if (!chosenServer.IsSuccess)
            {
                return;
            }

            string regionCode = chosenServer.RegionCode;
            int pos = chosenServer.Position;

            Console.Write(AppResources.GetSummonerGame_GiveSummonerNick,
                AppSettings.ServerSpectateAddresses.ElementAt(pos - 1).ServerName);
            string summonerName = Console.ReadLine();

            var summonerResponse = await SummonerService.GetInformationAboutSummoner(summonerName, regionCode);
            if (!summonerResponse.IsSuccess)
            {
                Console.WriteLine(
                    string.IsNullOrEmpty(summonerResponse.Message)
                    ? AppResources.Error_Undefined
                    : summonerResponse.Message);
                return;
            }

            string summonerId = summonerResponse.Id;
            var result = await SummonerGameService.GetSummonerGameInformation(summonerId, regionCode);

            if (!result.IsSuccess)
            {
                Console.WriteLine(
                    string.IsNullOrEmpty(result.Message)
                    ? AppResources.Error_Undefined
                    : AppResources.GetSummonerGame_SummonerDontPlay);
                return;
            }

            Console.WriteLine(
                AppResources.GetSummonerGame__SummonerIsInGame,
                Environment.NewLine,
                summonerName,
                result.Details.GameMode);

            Console.WriteLine(AppResources.GetSummonerGame_IfUserWantsToOpenSpectate);

            if (Console.ReadLine() != "T")
            {
                Console.WriteLine(AppResources.ClickToContinue);
                Console.ReadKey();
                return;
            }

            string serverAddress = AppSettings.ServerSpectateAddresses.ElementAt(pos - 1).ServerAddress;
            string serverPort = AppSettings.ServerSpectateAddresses.ElementAt(pos - 1).Port;

            var encryptionKey = result.Details.Observers.ToObject<JObject>().GetValue("encryptionKey").ToObject<string>();
            bool wasClientOpened = Spectator.OpenSpectateClient(
                encryptionKey,
                result.Details,
                regionCode.ToUpper(),
                serverAddress,
                serverPort);

            if (!wasClientOpened)
            {
                Console.WriteLine(AppResources.ClickToContinue);
                Console.ReadKey();
                return;
            }

            Process.Start(AppSettings.PathToSaveBatchFile);
            Console.WriteLine(AppResources.ClickToContinue);
            Console.ReadKey();
        }

        private static void AboutApp()
        {
            foreach (var role in AppSettings.AboutAppProjectRoles)
            {
                Console.ForegroundColor = role.Value;
                Console.WriteLine(role.Key);
            }
            Console.ResetColor();
            Console.ReadKey();
        }

        private static ChosenServer PrintListOfServers()
        {
            Console.WriteLine(AppResources.GetServerStatus_ChooseServerFromList, Environment.NewLine, Environment.NewLine);
            int position = 1;
            foreach (var server in AppSettings.ServerSpectateAddresses)
            {
                Console.WriteLine(AppResources.Common_TwoVerbatimStringWithDot,
                    position,
                    server.ServerName);
                position++;
            }

            bool getPosition = int.TryParse(Console.ReadLine(), out int pos);
            if (!getPosition || pos > AppSettings.ServerSpectateAddresses.Count)
            {
                Console.WriteLine(AppResources.GetServerStatus_ParsingFailed);
                return new ChosenServer
                {
                    IsSuccess = false
                };
            }

            return new ChosenServer
            {
                RegionCode = AppSettings.ServerSpectateAddresses.ElementAt(pos - 1).ServerCode,
                Position = pos,
                IsSuccess = true
            };
        }

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int cmdShow);

        private static void MaximizeConsoleWindow()
        {
            Process p = Process.GetCurrentProcess();
            ShowWindow(p.MainWindowHandle, 3);
        }
    }
}