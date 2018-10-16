using System;
using LeagueInformer.Resources;
using LeagueInformer.Services;

namespace LeagueInformer
{
    public class Program
    {
        private static readonly ConnectionService ConnectionService = new ConnectionService();

        public static void Main(string[] args)
        {
            if (ConnectionService.HasInternetConnection())
            {
                Console.WriteLine(AppResources.Main_WelcomeUser);
                Console.WriteLine(AppResources.Main_ChooseFunction);
                Console.WriteLine("1. Opcja nr 1");
                Console.WriteLine("2. Opcja nr 2");
                Console.WriteLine(AppResources.Main_Quit);
                var option = Console.ReadLine();
                do
                {
                    switch (option)
                    {
                        case "1":
                            break;
                        case "2":
                            break;
                        case "3":
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
    }
}