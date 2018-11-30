using System;
using LeagueInformer.Resources;
using LeagueInformer.Services;

namespace LeagueInformer
{
    public class Program
    {
        private static readonly ConnectionService ConnectionService = new ConnectionService();
        private static readonly GetSummonerService GetSummonerService = new GetSummonerService();

        public static void Main(string[] args)
        {
            if (ConnectionService.HasInternetConnection())
            {
                Console.WriteLine(AppResources.Main_WelcomeUser);
                Console.WriteLine(AppResources.Main_ChooseFunction);
                Console.WriteLine("1. Opcja nr 1");
                Console.WriteLine("2. Opcja nr 2");
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
                } while (option != null && (!option.Equals("1") || !option.Equals("2") || !option.Equals("3") || !option.Equals("4")));

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