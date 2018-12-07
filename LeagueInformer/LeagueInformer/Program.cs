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
        private static readonly GetChallengersService ChallengersService = new GetChallengersService();
        private static readonly GetLeagueOfSummoner LeagueOfSummonerService = new GetLeagueOfSummoner();

        public static void Main(string[] args)
        {
            if (ConnectionService.HasInternetConnection())
            {
                Console.WriteLine(AppResources.Main_WelcomeUser);
                Console.WriteLine(AppResources.Main_ChooseFunction);
                Console.WriteLine(AppResources.MainMenu_GetLeagueOfSummoner);
                Console.WriteLine(AppResources.MainMenu_GetChallengerList);
                Console.WriteLine(AppResources.MainManu_AboutApp);
                Console.WriteLine(AppResources.Main_Quit);
                var option = Console.ReadLine();
                while (option != null && (!option.Equals("1") || !option.Equals("2") || !option.Equals("3") || !option.Equals("4")))
                {
                    switch (option)
                    {
                        case "1":
                            GetLeagueOfSummoner().Wait();
                            break;
                        case "2":
                            GetBestChallengers().Wait();
                            break;
                        case "3":
                            AboutApp();
                            break;
                        case "4":
                            Environment.Exit(1);
                            break;
                        default:
                            Console.WriteLine(AppResources.Common_OptionIsNotAvailable);
                            option = Console.ReadLine();
                            break;
                    }
                }

            }
            else
            {
                Console.WriteLine(AppResources.Main_NoInternetConnection);
                ExitApp();
            }
        }

        private static void ExitApp()
        {
            Console.WriteLine(AppResources.Common_ExitApp);
            Console.ReadLine();
            Environment.Exit(1);
        }

        private async static Task GetLeagueOfSummoner()
        {
            Console.Write(AppResources.GetLeagueOfSummoner_EnterName);
            string summonerName = Console.ReadLine();
           
            var summonerResponse = await SummonerService.GetInformationAboutSummoner(summonerName);
            if (!summonerResponse.IsSuccess || summonerResponse == null)
            {
                Console.WriteLine(
                    string.IsNullOrEmpty(summonerResponse.Message)
                    ? AppResources.Error_Undefined
                    : summonerResponse.Message);
                ExitApp();
                return;
            }

            string summonerId = summonerResponse.Id;
            var result = await LeagueOfSummonerService.GetLeagueOfSummonerInformation(summonerId);

            if (!result.IsSuccess || result == null)
            {
                Console.WriteLine(
                    string.IsNullOrEmpty(result.Message)
                    ? AppResources.Error_Undefined
                    : result.Message);
                ExitApp();
                return;
            }

            Console.WriteLine(result.IsSuccess ?
                $" Nazwa Ligi: {result.leagueName} " +
                $"\n Summoner Name: {result.summonerName} " +
                $"\n Tier: {result.tier} " +
                $"\n Wygrane: {result.wins} " +
                $"\n Przegrane: {result.losses} " +
                $"\n Typ Kolejki: {result.queueType}" : result.Message);
            ExitApp();
        }

        private static async Task GetBestChallengers()
        {
            var response = await ChallengersService.GetListOfChallengers();

            if (response.IsSuccess)
            {
                var bestChallengers = response.ChallengersResponseList.OrderByDescending(x => x.Points).ToList().GetRange(0, 9);
                var position = 1;

                foreach (var challenger in bestChallengers)
                {
                    Console.WriteLine(
                        AppResources.GetBestChallengers_StatisticsPatten,
                        position,
                        challenger.SummonerName,
                        challenger.Wins,
                        challenger.Losses,
                        challenger.Points);
                    Console.WriteLine(challenger.Veteran
                        ? AppResources.GetBestChallengers_IsVeteran
                        : AppResources.GetBestChallengers_IsNotVeteran);
                    Console.WriteLine(challenger.HotStreak
                        ? AppResources.GetBestChallengers_HasHotStreak
                        : AppResources.GetBestChallengers_HasNotHotStreak);
                    Console.WriteLine();
                    position++;
                }
            }
            else
            {
                Console.WriteLine(AppResources.Error_Undefined);
            }
            ExitApp();
        }

        private static void AboutApp()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\tLeague Informer");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\n\tApp version \n\t1.0.0\n");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("\tProject Owner\n\tBartosz Mróz\n");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\tCEO\n\tBartosz Mróz\n");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\tUX Designer\n\tBartosz Mróz\n");
            Console.WriteLine("\tUI Designer\nBartosz Mróz\tFilip Nowicki\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\n\tDevelopers\nBartosz Mróz\tFilip Nowicki\nRobert Dobiała\tIgor Drążkowski\n");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("\tTesters\nBartosz Mróz\t Filip Nowicki");
            Console.ResetColor();
            ExitApp();
        }
    }
}