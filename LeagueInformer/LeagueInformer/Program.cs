using System;
using LeagueInformer.Resources;
using LeagueInformer.Services;

namespace LeagueInformer
{
    public class Program
    {
        private static readonly ConnectionService ConnectionService = new ConnectionService();
        private static readonly GetSummonerService GetSummonerService = new GetSummonerService();
        private static readonly ChallengerService challengerService = new ChallengerService();

        public static void Main(string[] args)
        {
            if (ConnectionService.HasInternetConnection())
            {
                Console.WriteLine(AppResources.Main_WelcomeUser);
                Console.WriteLine(AppResources.Main_ChooseFunction);
                Console.WriteLine("1. Opcja nr 1");
                Console.WriteLine("2. Wyswietlenie listy wszystkich challengerow na EUNE");
                Console.WriteLine("3. O aplikacj");
                Console.WriteLine(AppResources.Main_Quit);
                var option = Console.ReadLine();
                do
                {
                    switch (option)
                    {
                        case "1":
                            FirstOption();
                            break;
                        case "2":
                            SecondOption();
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
                } while (option != null && (!option.Equals("1") || !option.Equals("2") || !option.Equals("3")));

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
            var response = GetSummonerService.GetInformationAboutSummoner("Skirtek").Result;
            Console.WriteLine(response.IsSuccess ?
                response.AccountId : response.Message);
            ExitApp();
        }

        private static void AboutApp()
        {
            //TODO dopisanie informacji o aplikacji

        }

        private static void SecondOption()
        {
            //TODO do podmiany na funkcję w programie
            var response = challengerService.GetInformationAboutChallangerList("RANKED_SOLO_5x5").Result;
            Console.WriteLine();
            ExitApp();
        }
    }
}