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

        public static void Main(string[] args)
        {
            if (ConnectionService.HasInternetConnection())
            {
                Console.WriteLine(AppResources.Main_WelcomeUser);
                Console.WriteLine(AppResources.Main_ChooseFunction);
                Console.WriteLine("1. Opcja nr 1");
                Console.WriteLine(AppResources.MainMenu_GetChallengerList);
                Console.WriteLine(AppResources.Main_Quit);
                var option = Console.ReadLine();
                while (option != null && (!option.Equals("1") || !option.Equals("2") || !option.Equals("3")))
                {
                    switch (option)
                    {
                        case "1":
                            FirstOption();
                            break;
                        case "2":
                            GetBestChallengers().Wait();
                            break;
                        case "3":
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

        private static void FirstOption()
        {
            //TODO do podmiany na funkcję w programie
            var response = SummonerService.GetInformationAboutSummoner("Skirtek").Result;
            Console.WriteLine(response.IsSuccess ?
                response.AccountId : response.Message);
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
    }
}