using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Autofac;
using LeagueInformer.Api;
using LeagueInformer.Api.Interfaces;
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
        #region Intefaces 
        private static IConnection _connection;
        private static IGetSummoner _summoner;
        private static IGetMasters _masters;
        private static IGetLeagueOfSummonerInformation _leagueOfSummoner;
        private static IServerService _serverService;
        private static IGetLeagueInfo _leagueInfo;
        private static IGetSummonerGame _summonerGame;
        private static ISpectator _spectator;
        private static IGetLastGames _lastGames;
        private static IPrintMethods _printMethods;
        private static IDateHandler _dateHandler;
        private static readonly LeagueConstants Constants = new LeagueConstants();
        private static IContainer Container { get; set; }
        #endregion

        #region Main
        public static void Main(string[] args)
        {

            Container = SetDependencies();
            ResolveDependencies();
            if (_connection.HasInternetConnection())
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
            var chosenServer = _printMethods.PrintListOfSpectateServers();

            if (!chosenServer.IsSuccess)
            {
                return;
            }

            string regionCode = chosenServer.RegionCode;

            string summonerName = await _printMethods.PrintListOfSavedNicknames();

            if (string.IsNullOrEmpty(summonerName))
            {
                Console.WriteLine(AppResources.Error_SummonerNameCannotBeEmpty);
                return;
            }

            var summonerResponse = await _summoner.GetInformationAboutSummoner(summonerName, regionCode);
            if (!summonerResponse.IsSuccess)
            {
                Console.WriteLine(
                    string.IsNullOrEmpty(summonerResponse.Message)
                    ? AppResources.Error_Undefined
                    : summonerResponse.Message);
                return;
            }

            string summonerId = summonerResponse.Id;
            var result = await _leagueOfSummoner.GetLeagueOfSummonerInformation(summonerId, regionCode);

            if (!result.IsSuccess)
            {
                Console.WriteLine(
                    string.IsNullOrEmpty(result.Message)
                    ? AppResources.Error_Undefined
                    : result.Message);
                return;
            }

            if (result.SummonerLeagueInfoList.Count == 0)
            {
                Console.WriteLine(AppResources.GetLeagueOfSummoner_PlayerDoesntParticipatesInAnyGames);
                return;
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            foreach (var queue in result.SummonerLeagueInfoList)
            {
                Console.WriteLine(
                    string.Format(AppResources.GetLeagueOfSummoner_SummonerName, Environment.NewLine, queue.SummonerName) +
                    string.Format(AppResources.GetLeagueOfSummoner_LeagueName, Environment.NewLine, queue.LeagueName) +
                    string.Format(AppResources.GetLeagueOfSummoner_Tier, Environment.NewLine, queue.Tier) +
                    string.Format(AppResources.GetLeagueOfSummoner_Rank, Environment.NewLine, queue.Rank) +
                    string.Format(AppResources.GetLeagueOfSummoner_Wins, Environment.NewLine, queue.Wins) +
                    string.Format(AppResources.GetLeagueOfSummoner_Losses, Environment.NewLine, queue.Losses) +
                    string.Format(AppResources.GetLeagueOfSummoner_QueueType, Environment.NewLine, queue.QueueType));
            }

            Console.ResetColor();
        }

        private static async Task GetSummonerLeagueInfo()
        {
            var chosenServer = _printMethods.PrintListOfSpectateServers();

            if (!chosenServer.IsSuccess)
            {
                return;
            }

            string regionCode = chosenServer.RegionCode;

            string summonerName = await _printMethods.PrintListOfSavedNicknames();

            if (string.IsNullOrEmpty(summonerName))
            {
                Console.WriteLine(AppResources.Error_SummonerNameCannotBeEmpty);
                return;
            }

            var summoner = await _summoner.GetInformationAboutSummoner(summonerName, regionCode);

            if (!summoner.IsSuccess)
            {
                Console.WriteLine(summoner.Message);
                return;
            }

            var summonerLeague = await _leagueOfSummoner.GetLeagueOfSummonerInformation(summoner.Id, regionCode);

            if (!summonerLeague.IsSuccess)
            {
                Console.WriteLine(summonerLeague.Message);
                return;
            }

            var rankedLeagueInfo = summonerLeague.SummonerLeagueInfoList.First(x => x.QueueType == "RANKED_SOLO_5x5");
            var response = await _leagueInfo.GetListOfSummonerLeague(summonerName, summonerLeague, rankedLeagueInfo.LeagueId, regionCode);

            if (!response.IsSuccess)
            {
                Console.WriteLine(AppResources.Error_Undefined);
                return;
            }

            if (response.LeagueDetailsResponseList.Count == 0)
            {
                Console.WriteLine(AppResources.GetSummonerLeagueInfo_PlayerDoesntParticipateInRankedGames);
                return;
            }

            var sortedMembers = response.LeagueDetailsResponseList.OrderByDescending(x => x.Points).ToList();
            int position = 1;
            Console.WriteLine(AppResources.Common_TwoVerbatimStrings,
                rankedLeagueInfo.LeagueName,
                rankedLeagueInfo.Rank);
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
            var chosenServer = _printMethods.PrintListOfSpectateServers();

            if (!chosenServer.IsSuccess)
            {
                return;
            }

            string regionCode = chosenServer.RegionCode;

            string summonerName = await _printMethods.PrintListOfSavedNicknames();

            if (string.IsNullOrEmpty(summonerName))
            {
                Console.WriteLine(AppResources.Error_SummonerNameCannotBeEmpty);
                return;
            }

            var summonerResponse = await _summoner.GetInformationAboutSummoner(summonerName);
            if (!summonerResponse.IsSuccess)
            {
                Console.WriteLine(
                    string.IsNullOrEmpty(summonerResponse.Message)
                        ? AppResources.Error_Undefined
                        : summonerResponse.Message);
                return;
            }

            string accountId = summonerResponse.AccountId;

            var response = await _lastGames.GetLastTenGames(accountId, regionCode);

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
                string seasonName = Constants.SeasonsDictionary.TryGetValue(match.SeasonId, out string season)
                    ? season
                    : AppResources.GetSummonerHistory_UndefinedSeason;

                var gameMode = Constants.GameModes.First(x => x.ModeId == match.QueueId);

                Console.WriteLine(AppResources.GetSummonerHistory_MatchFormat,
                    position,
                    _summoner.GetChampionForId(match.Champion),
                    gameMode.MapName,
                    gameMode.GameType,
                    seasonName,
                    _dateHandler.ParseTimeToDate(match.Date));
                position++;
            }
        }

        private static async Task GetBestMasters()
        {
            var chosenServer = _printMethods.PrintListOfSpectateServers();

            if (!chosenServer.IsSuccess)
            {
                return;
            }

            string regionCode = chosenServer.RegionCode;

            var response = await _masters.GetListOfMasterLeague(regionCode);

            if (!response.IsSuccess)
            {
                Console.WriteLine(AppResources.Error_Undefined);
                return;
            }

            if (response.MastersResponseList.Count < 10)
            {
                Console.WriteLine(AppResources.GetBestMasters_NotEnoughMasters);
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

            var response = await _serverService.GetServerStatus(regionCode);
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
            var chosenServer = _printMethods.PrintListOfSpectateServers();
            if (!chosenServer.IsSuccess)
            {
                return;
            }

            string regionCode = chosenServer.RegionCode;
            int pos = chosenServer.Position;

            Console.Write(AppResources.GetSummonerGame_GiveSummonerNick,
                AppSettings.ServerSpectateAddresses.ElementAt(pos - 1).ServerName);
            string summonerName = Console.ReadLine();
            
            var summonerResponse = await _summoner.GetInformationAboutSummoner(summonerName, regionCode);
            if (!summonerResponse.IsSuccess)
            {
                Console.WriteLine(
                    string.IsNullOrEmpty(summonerResponse.Message)
                    ? AppResources.Error_Undefined
                    : summonerResponse.Message);
                return;
            }
            
            string summonerId = summonerResponse.Id;
            var result = await _summonerGame.GetSummonerGameInformation(summonerId, regionCode);

            Console.ForegroundColor = ConsoleColor.Yellow;
            if (!result.IsSuccess)
            {
                Console.WriteLine(
                    string.IsNullOrEmpty(result.Message)
                    ? AppResources.Error_Undefined
                    : AppResources.GetSummonerGame_SummonerDontPlay);
                Console.ResetColor();
                return;
            }
            
            string gameMode = Constants.GameModesDictionary.TryGetValue(result.Details.GameMode, out string mode)
                ? mode
                : AppResources.GetSummonerGame_UndefinedGameType;

            Console.WriteLine(
                AppResources.GetSummonerGame__SummonerIsInGame,
                Environment.NewLine,
                summonerName,
                gameMode);

            Console.WriteLine(AppResources.GetSummonerGame_IfUserWantsToOpenSpectate);
            Console.ResetColor();
            string answer = Console.ReadLine();

            if (answer == null)
            {
                EndOption();
                return;
            }
            
            if (answer.ToLower() != "t")
            {
                EndOption();
                return;
            }

            string serverAddress = AppSettings.ServerSpectateAddresses.ElementAt(pos - 1).ServerAddress;
            string serverPort = AppSettings.ServerSpectateAddresses.ElementAt(pos - 1).Port;

            var encryptionKey = result.Details.Observers.ToObject<JObject>().GetValue("encryptionKey").ToObject<string>();
            bool wasClientOpened = _spectator.OpenSpectateClient(
                encryptionKey,
                result.Details,
                regionCode.ToUpper(),
                serverAddress,
                serverPort);

            if (!wasClientOpened)
            {
                EndOption();
                return;
            }

            Process.Start(AppSettings.PathToSaveBatchFile);
            EndOption();
        }

        private static void EndOption()
        {
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

        #region IoC
        private static IContainer SetDependencies()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<ConnectionService>().As<IConnection>();
            builder.RegisterType<ApiClient>().As<IApiClient>();
            builder.RegisterType<HttpClientHandler>().As<IHttpClient>();
            builder.RegisterType<ErrorHandler>().As<IErrorHandler>();
            builder.RegisterType<GetLeagueInfoService>().As<IGetLeagueInfo>();
            builder.RegisterType<ConnectionService>().As<IConnection>();
            builder.RegisterType<GetSummonerService>().As<IGetSummoner>();
            builder.RegisterType<GetMastersService>().As<IGetMasters>();
            builder.RegisterType<GetLeagueOfSummoner>().As<IGetLeagueOfSummonerInformation>();
            builder.RegisterType<ServerService>().As<IServerService>();
            builder.RegisterType<GetSummonerGame>().As<IGetSummonerGame>();
            builder.RegisterType<Spectator>().As<ISpectator>();
            builder.RegisterType<GetLastGamesService>().As<IGetLastGames>();
            builder.RegisterType<PrintMethods>().As<IPrintMethods>();
            builder.RegisterType<DateHandler>().As<IDateHandler>();
            builder.RegisterType<FileHandler>().As<IFileHandler>();
            return builder.Build();
        }

        private static void ResolveDependencies()
        {
            _leagueInfo = Container.Resolve<IGetLeagueInfo>();
            _connection = Container.Resolve<IConnection>();
            _summoner = Container.Resolve<IGetSummoner>();
            _masters = Container.Resolve<IGetMasters>();
            _leagueOfSummoner = Container.Resolve<IGetLeagueOfSummonerInformation>();
            _serverService = Container.Resolve<IServerService>();
            _summonerGame = Container.Resolve<IGetSummonerGame>();
            _spectator = Container.Resolve<ISpectator>();
            _lastGames = Container.Resolve<IGetLastGames>();
            _printMethods = Container.Resolve<IPrintMethods>();
            _dateHandler = Container.Resolve<IDateHandler>();
        }
        #endregion
    }
}