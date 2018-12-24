using System;
using System.Collections.Generic;
using System.IO;
using LeagueInformer.Models;
using LeagueInformer.Resources;

namespace LeagueInformer
{
    public static class AppSettings
    {
        public const string CheckInternetConnectionString = "http://clients3.google.com/generate_204";
        public const string AuthorizationApiKey = "RGAPI-2958ec04-0412-63bf-1ddc-2aba3fe0bfbe";
        public const string PathToSaveBatchFile = @"C:\temp\runGame.bat";
        private const string Tabulator = "\t";

        public static readonly string PathToSaveNicknameFile =
            Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "LeagueInformerNicknames.txt");

        public static readonly string BatchFileSkeleton =
            "cd {0}"
            + $"{Environment.NewLine}"
            + $"{Environment.NewLine}if exist \"League of Legends.exe\" ("
            + $"{Environment.NewLine}echo \"Gra uruchamia się...\""
            + "{1}@start \"\" \"League of Legends.exe\" {2}"
            + $"{Environment.NewLine})";

        public static readonly List<string> LocalMachineRegisterKeysPath = new List<string>
        {
            @"SOFTWARE\WOW6432Node\Riot Games, Inc\League of Legends",
            @"SOFTWARE\RIOT GAMES\RADS",
            @"Software\Wow6432Node\Riot Games\RADS"
        };

        public static readonly List<string> CurrentUserRegisterKeysPath = new List<string>
        {
            @"SOFTWARE\RIOT GAMES\RADS",
            @"SOFTWARE\Classes\VirtualStore\MACHINE\SOFTWARE\Wow6432Node\RIOT GAMES\RADS",
            @"SOFTWARE\Classes\VirtualStore\MACHINE\SOFTWARE\RIOT GAMES\RADS",
            @"SOFTWARE\RIOT GAMES\RADS"
        };

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
            {"Ameryka Środkowa", "la1" },
            {"Ameryka Południowa", "la2" },
        };

        public static readonly List<SpectateServer> ServerSpectateAddresses = new List<SpectateServer>
        {
            new SpectateServer
            {
                ServerName = "Rosja",
                ServerCode = "ru",
                ServerAddress = "ru",
                Port = "80"
            },
            new SpectateServer
            {
                ServerName = "Korea",
                ServerCode = "kr",
                ServerAddress = "kr",
                Port = "80"
            },
            new SpectateServer
            {
                ServerName = "Brazylia",
                ServerCode = "br1",
                ServerAddress = "br",
                Port = "80"
            },
            new SpectateServer
            {
                ServerName = "Oceania",
                ServerCode = "oc1",
                ServerAddress = "oc1",
                Port = "80"
            },
            new SpectateServer
            {
                ServerName = "Japonia",
                ServerCode = "jp1",
                ServerAddress = "jp1",
                Port = string.Empty
            },
            new SpectateServer
            {
                ServerName = "Ameryka Północna",
                ServerCode = "na1",
                ServerAddress = "na1",
                Port = "80"
            },
            new SpectateServer
            {
                ServerName = "Europa Północno-Wschodnia",
                ServerCode = "eun1",
                ServerAddress = "eu",
                Port = "8088"
            },
            new SpectateServer
            {
                ServerName = "Europa Zachodnia",
                ServerCode = "euw1",
                ServerAddress = "euw1",
                Port = "80"
            },
            new SpectateServer
            {
                ServerName = "Turcja",
                ServerCode = "tr1",
                ServerAddress = "tr",
                Port = "80"
            },
            new SpectateServer
            {
                ServerName = "Ameryka Środkowa",
                ServerCode = "la1",
                ServerAddress = "la1",
                Port = "80"
            },
            new SpectateServer
            {
                ServerName = "Ameryka Południowa",
                ServerCode = "la2",
                ServerAddress = "la2",
                Port = "80"
            }
        };

        public static readonly List<string> MenuOptions = new List<string>
        {
            AppResources.MainMenu_GetLeagueOfSummoner,
            AppResources.MainMenu_GetMasterList,
            AppResources.MainMenu_GetSummonerLeagueInfo,
            AppResources.MainMenu_GetSummonerHistory,
            AppResources.MainMenu_GetSummonerGame,
            AppResources.MainMenu_GetServerStatus,
            AppResources.MainManu_AboutApp,
            AppResources.Main_Quit
        };

        public static readonly Dictionary<string, ConsoleColor> AboutAppProjectRoles = new Dictionary<string, ConsoleColor>
        {
            {$"{Tabulator}League Informer", ConsoleColor.Yellow},
            {$"{Environment.NewLine}{Tabulator}App version {Environment.NewLine}{Tabulator}1.0.0{Environment.NewLine}", ConsoleColor.DarkYellow },
            {$"{Tabulator}Project Owner{Environment.NewLine}{Tabulator}Bartosz Mróz{Environment.NewLine}", ConsoleColor.DarkRed},
            {$"{Tabulator}CEO{Environment.NewLine}{Tabulator}Bartosz Mróz{Environment.NewLine}", ConsoleColor.Red },
            {$"{Tabulator}UX Designer{Environment.NewLine}{Tabulator}Bartosz Mróz{Environment.NewLine}{Tabulator}UI Designer{Environment.NewLine}Bartosz Mróz{Tabulator}Filip Nowicki{Environment.NewLine}", ConsoleColor.Green},
            {$"{Environment.NewLine}{Tabulator}Developers{Environment.NewLine}Bartosz Mróz{Tabulator}Filip Nowicki{Environment.NewLine}Robert Dobiała{Tabulator}Igor Drążkowski{Environment.NewLine}", ConsoleColor.Blue},
            {$"{Tabulator}Testers{Environment.NewLine}Bartosz Mróz{Tabulator} Filip Nowicki", ConsoleColor.Gray},
            {$"{Environment.NewLine}{AppResources.ClickToContinue}", ConsoleColor.Yellow }
        };
    }
}