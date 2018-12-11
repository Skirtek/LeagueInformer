using System;
using System.Linq;
using System.Threading.Tasks;
using LeagueInformer.Resources;
using LeagueInformer.Services;

namespace LeagueInformer
{
    public class Program
    {
        private static readonly ConnectionService ConnectionService = new ConnectionService();
        private static readonly GetSummonerService SummonerService = new GetSummonerService();
        private static readonly GetMastersService MastersService = new GetMastersService();
        private static readonly GetLeagueOfSummoner LeagueOfSummonerService = new GetLeagueOfSummoner();
        private static readonly ServerService ServerService = new ServerService();

        public static void Main(string[] args)
        {
            if (ConnectionService.HasInternetConnection())
            {
                string option;
                do
                {
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
                            GetServerStatus().Wait();
                            break;
                        case "4":
                            AboutApp();
                            break;
                        case "5":
                            Environment.Exit(1);
                            break;
                        default:
                            Console.WriteLine(AppResources.Common_OptionIsNotAvailable);
                            option = Console.ReadLine();
                            break;
                    }
                } while (option != null && option != "5");
            }
            else
            {
                Console.WriteLine(AppResources.Main_NoInternetConnection);
                ExitApp();
            }
        }

        private static void MainMenu()
        {
            Console.WriteLine();

            foreach (var menuOption in AppSettings.MenuOptions)
            {
                Console.WriteLine(menuOption);
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
            Console.Write(AppResources.GetLeagueOfSummoner_EnterName);
            string summonerName = Console.ReadLine();

            var summonerResponse = await SummonerService.GetInformationAboutSummoner(summonerName);
            if (!summonerResponse.IsSuccess)
            {
                Console.WriteLine(
                    string.IsNullOrEmpty(summonerResponse.Message)
                    ? AppResources.Error_Undefined
                    : summonerResponse.Message);
                return;
            }

            string summonerId = summonerResponse.Id;
            var result = await LeagueOfSummonerService.GetLeagueOfSummonerInformation(summonerId);

            if (!result.IsSuccess)
            {
                Console.WriteLine(
                    string.IsNullOrEmpty(result.Message)
                    ? AppResources.Error_Undefined
                    : result.Message);
                return;
            }

            Console.WriteLine(result.IsSuccess ?
                $" Nazwa Ligi: {result.leagueName} " +
                $"\n Summoner Name: {result.summonerName} " +
                $"\n Tier: {result.tier} " +
                $"\n Wygrane: {result.wins} " +
                $"\n Przegrane: {result.losses} " +
                $"\n Typ Kolejki: {result.queueType}" : result.Message);
        }

        private static async Task GetBestMasters()
        {
            var response = await MastersService.GetListOfMasterLeague();

            if (!response.IsSuccess)
            {
                Console.WriteLine(AppResources.Error_Undefined);
                return;
            }

            var bestMasters = response.MastersResponseList.OrderByDescending(x => x.Points).ToList()
                .GetRange(1, 10);
            var position = 1;

            foreach (var master in bestMasters)
            {
                Console.WriteLine(
                    AppResources.GetBestMasters_StatisticsPatten,
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
            var position = 1;
            foreach (var serverName in AppSettings.ServerAddresses.Keys)
            {
                Console.WriteLine(AppResources.GetServerStatus_PrintServersList,
                    position,
                    serverName);
                position++;
            }

            bool getPosition = int.TryParse(Console.ReadLine(), out int pos);
            if (!getPosition)
            {
                Console.WriteLine(AppResources.GetServerStatus_ParsingFailed);
                return;
            }

            bool getValue = AppSettings.ServerAddresses.TryGetValue(
                AppSettings.ServerAddresses.Keys.ElementAt(pos - 1), out string str);
            if (!getValue)
            {
                Console.WriteLine(AppResources.Error_Undefined);
                return;
            }

            var response = await ServerService.GetServerStatus(str);
            if (!response.IsSuccess)
            {
                Console.WriteLine(AppResources.Error_Undefined);
                return;
            }

            Console.WriteLine(AppResources.GetServerStatus_DataForServer, Environment.NewLine, response.Name, Environment.NewLine);

            foreach (var serviceStatus in response.ServicesStatuses)
            {
                Console.WriteLine(serviceStatus.Name);
                Console.WriteLine(serviceStatus.ServerStatusState);
                Console.WriteLine();
            }
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
    }
}