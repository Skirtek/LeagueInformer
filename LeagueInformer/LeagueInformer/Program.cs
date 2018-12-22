using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using LeagueInformer.Interfaces;
using LeagueInformer.Resources;
using LeagueInformer.Services;
using LeagueInformer.Utils;
using LeagueInformer.Utils.Interfaces;
using Newtonsoft.Json.Linq;

namespace LeagueInformer
{
    public class Program
    {
        #region Interfaces 
        private static readonly IConnection Connection = new ConnectionService();
        private static readonly IGetSummoner SummonerService = new GetSummonerService();
        private static readonly IGetMasters MastersService = new GetMastersService();
        private static readonly IGetLeagueOfSummonerInformation LeagueOfSummonerService = new GetLeagueOfSummoner();
        private static readonly IServerService ServerService = new ServerService();
        private static readonly IGetLeagueInfo LeagueInfoService = new GetLeagueInfoService();
        private static readonly IGetSummonerGame SummonerGameService = new GetSummonerGame();
        private static readonly ISpectator Spectator = new Spectator();
        private static readonly IGetLastGames LastGameService = new GetLastGamesService();
        private static readonly IPrintMethods PrintMethods = new PrintMethods();
        private static readonly DateHandler DateHandler = new DateHandler();
        #endregion

        #region Main
        public static void Main(string[] args)
        {
            if (Connection.HasInternetConnection())
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
        #endregion

        #region PrivateMethods
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
            var chosenServer = PrintMethods.PrintListOfSpectateServers();

            if (!chosenServer.IsSuccess)
            {
                return;
            }

            string regionCode = chosenServer.RegionCode;

            string summonerName = await PrintMethods.PrintListOfSavedNicknames();

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
                $"{Environment.NewLine}Nazwa przywoływacza: {result.SummonerLeagueInfo.SummonerName} " +
                $"{Environment.NewLine}Nazwa ligi: {result.SummonerLeagueInfo.LeagueName} " +
                $"{Environment.NewLine}Tier: {result.SummonerLeagueInfo.Tier} " +
                $"{Environment.NewLine}Ranga: {result.SummonerLeagueInfo.Rank} " +
                $"{Environment.NewLine}Wygrane: {result.SummonerLeagueInfo.Wins} " +
                $"{Environment.NewLine}Przegrane: {result.SummonerLeagueInfo.Losses} " +
                $"{Environment.NewLine}Typ kolejki: {result.SummonerLeagueInfo.QueueType}" : result.Message);
            Console.ResetColor();
        }

        private static async Task GetSummonerLeagueInfo()
        {
            var chosenServer = PrintMethods.PrintListOfSpectateServers();

            if (!chosenServer.IsSuccess)
            {
                return;
            }

            string regionCode = chosenServer.RegionCode;

            string summonerName = await PrintMethods.PrintListOfSavedNicknames();

            if (string.IsNullOrEmpty(summonerName))
            {
                Console.WriteLine(AppResources.Error_SummonerNameCannotBeEmpty);
                return;
            }

            var summoner = await SummonerService.GetInformationAboutSummoner(summonerName, regionCode);

            if (!summoner.IsSuccess)
            {
                Console.WriteLine(summoner.Message);
                return;
            }

            var summonerLeague = await LeagueOfSummonerService.GetLeagueOfSummonerInformation(summoner.Id, regionCode);

            if (!summonerLeague.IsSuccess)
            {
                Console.WriteLine(summonerLeague.Message);
                return;
            }

            var response = await LeagueInfoService.GetListOfSummonerLeague(summonerName, summonerLeague, regionCode);

            if (!response.IsSuccess)
            {
                Console.WriteLine(AppResources.Error_Undefined);
                return;
            }

            var sortedMembers = response.LeagueDetailsResponseList.OrderByDescending(x => x.Points).ToList();
            int position = 1;
            Console.WriteLine(AppResources.Common_TwoVerbatimStrings,
                response.LeagueInfo.SummonerLeagueInfo.LeagueName,
                response.LeagueInfo.SummonerLeagueInfo.Rank);
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

        private static async Task GetSummonerHistory()
        {
            var chosenServer = PrintMethods.PrintListOfSpectateServers();

            if (!chosenServer.IsSuccess)
            {
                return;
            }

            string regionCode = chosenServer.RegionCode;

            string summonerName = await PrintMethods.PrintListOfSavedNicknames();

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
                    string.IsNullOrEmpty(response.Message)
                        ? AppResources.Error_Undefined
                        : response.Message);
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
            var chosenServer = PrintMethods.PrintListOfSpectateServers();

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
            var chosenServer = PrintMethods.PrintListOfSpectateServers();
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
        #endregion

        #region HelperMethods
        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int cmdShow);

        private static void MaximizeConsoleWindow()
        {
            Process p = Process.GetCurrentProcess();
            ShowWindow(p.MainWindowHandle, 3);
        }
        #endregion
    }
}