using System;
using System.Collections.Generic;
using System.IO;
using LeagueInformer.Resources;

namespace LeagueInformer
{
    public static class AppSettings
    {
        public const string CheckInternetConnectionString = "http://clients3.google.com/generate_204";
        public const string AuthorizationApiKey = "RGAPI-2958ec04-0412-63bf-1ddc-2aba3fe0bfbe";

        public static readonly string PathToSaveNicknameFile = 
            Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "LeagueInformerNicknames.txt");

        public static readonly Dictionary<string, string> ServerAddresses = new Dictionary<string, string>
        {
            {"Rosja", "ru" },
            {"Korea", "kr" },
            {"PBE", "pbe1" },
            {"Brazylia", "br1" },
            {"Oceania", "oc1" },
            {"Japonia", "jp1" },
            {"Ameryka Północna", "na1" },
            {"Europa Północno-Wschodnia", "eun1" },
            {"Europa Zachodnia", "euw1" },
            {"Turcja", "tr1" },
            {"Ameryka Łacińska 1", "la1" },
            {"Ameryka Łacińska 2", "la2" },
        };

        public static readonly List<string> MenuOptions = new List<string>
        {
            AppResources.MainMenu_GetLeagueOfSummoner,
            AppResources.MainMenu_GetMasterList,
            AppResources.MainMenu_GetSummonerLeagueInfo,
            AppResources.MainMenu_GetServerStatus,
            AppResources.MainManu_AboutApp,
            AppResources.Main_Quit
        };

        public static readonly Dictionary<string, ConsoleColor> AboutAppProjectRoles = new Dictionary<string, ConsoleColor>
        {
            {"\tLeague Informer", ConsoleColor.Yellow},
            {$"{Environment.NewLine}\tApp version {Environment.NewLine}\t1.0.0{Environment.NewLine}", ConsoleColor.DarkYellow },
            {$"\tProject Owner{Environment.NewLine}\tBartosz Mróz{Environment.NewLine}", ConsoleColor.DarkRed},
            {$"\tCEO{Environment.NewLine}\tBartosz Mróz{Environment.NewLine}", ConsoleColor.Red },
            {$"\tUX Designer{Environment.NewLine}\tBartosz Mróz{Environment.NewLine}\tUI Designer{Environment.NewLine}Bartosz Mróz\tFilip Nowicki{Environment.NewLine}", ConsoleColor.Green},
            {$"{Environment.NewLine}\tDevelopers{Environment.NewLine}Bartosz Mróz\tFilip Nowicki{Environment.NewLine}Robert Dobiała\tIgor Drążkowski{Environment.NewLine}", ConsoleColor.Blue},
            {$"\tTesters{Environment.NewLine}Bartosz Mróz\t Filip Nowicki", ConsoleColor.Gray},
            {$"{Environment.NewLine}{AppResources.ClickToContinue}", ConsoleColor.Yellow }
        };
    }
}