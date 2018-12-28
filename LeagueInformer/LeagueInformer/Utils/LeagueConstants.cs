using System.Collections.Generic;
using LeagueInformer.Models;

namespace LeagueInformer.Utils
{
    public class LeagueConstants
    {
        public readonly Dictionary<string, string> GameModesDictionary = new Dictionary<string, string>
        {
            {"CLASSIC","zwykłej rozgrywce"},
            {"ODIN","rozgrywce w trybie Dominion/Crystal Scar"},
            {"ARAM","rozgrywce w trybie ARAM"},
            {"TUTORIAL","tutorialu"},
            {"URF", "rozgrywce w trybie URF"},
            {"DOOMBOTSTEEMO","rozgrywce w trybie Doom Bots"},
            {"ONEFORALL","rozgrywce w trybie One for All"},
            {"ASCENSION","rozgrywce w trybie Ascension"},
            {"FIRSTBLOOD","rozgrywce w trybie Snowdown Showdown"},
            {"KINGPORO","rozgrywce w trybie Legend of the Poro King"},
            {"SIEGE","rozgrywce w trybie Nexus Siege"},
            {"ASSASSINATE","rozgrywce w trybie Blood Hunt Assassin"},
            {"ARSR","rozgrywce w trybie All Random Summoner's Rift"},
            {"DARKSTAR","rozgrywce w trybie Dark Star: Singularity"},
            {"STARGUARDIAN","rozgrywce w trybie Star Guardian Invasion"},
            {"PROJECT PROJECT","rozgrywce w trybie PROJECT: Hunters"},
            {"GAMEMODEX","rozgrywce w trybie Nexus Blitz"},
            {"ODYSSEY","rozgrywce w trybie Odyssey: Extraction"}
        };

        public readonly Dictionary<int, string> SeasonsDictionary = new Dictionary<int, string>
        {
            {0, "Presezon 3" },
            {1, "Sezon 3" },
            {2, "Presezon 2014" },
            {3, "Sezon 2014" },
            {4, "Presezon 2015"},
            {5, "Sezon 2015" },
            {6, "Presezon 2016" },
            {7, "Sezon 2016" },
            {8, "Presezon 2017" },
            {9, "Sezon 2017" },
            {10, "Presezon 2018" },
            {11, "Sezon 2018" }
        };

        public readonly List<GameMode> GameModes = new List<GameMode>
        {
            new GameMode
            {
                ModeId = 0,
                GameType = "Własna gra"
            },
            new GameMode
            {
                ModeId = 72,
                GameType = "1v1 Snowdown Showdown",
                MapName = "Howling Abyss"
            },
            new GameMode
            {
                ModeId = 73,
                GameType = "2v2 Snowdown Showdown",
                MapName = "Howling Abyss"
            },
            new GameMode
            {
                ModeId = 75,
                GameType = "6v6 Hexakill",
                MapName = "Summoner's Rift"
            },
            new GameMode
            {
                ModeId = 76,
                GameType = "Ultra Rapid Fire",
                MapName = "Summoner's Rift"
            },
            new GameMode
            {
                ModeId = 78,
                GameType = "One For All: Mirror Mode",
                MapName = "Howling Abyss"
            },
            new GameMode
            {
                ModeId = 83,
                GameType = "Co-op vs AI Ultra Rapid Fire",
                MapName = "Summoner's Rift"
            },
            new GameMode
            {
                ModeId = 98,
                GameType = "6v6 Hexakill",
                MapName = "Twisted Treeline"
            },
            new GameMode
            {
                ModeId = 100,
                GameType = "5v5 ARAM",
                MapName = "Butcher's Bridge"
            },
            new GameMode
            {
                ModeId = 310,
                GameType = "Nemesis",
                MapName = "Summoner's Rift"
            },
            new GameMode
            {
                ModeId = 313,
                GameType = "Black Market Brawlers",
                MapName = "Summoner's Rift"
            },
            new GameMode
            {
                ModeId = 317,
                GameType = "Definitely Not Dominion",
                MapName = "Crystal Scar"
            },
            new GameMode
            {
                ModeId = 325,
                GameType = "All Random",
                MapName = "Summoner's Rift"
            },
            new GameMode
            {
                ModeId = 400,
                GameType = "5v5 Draft Pick",
                MapName = "Summoner's Rift"
            },
            new GameMode
            {
                ModeId = 420,
                GameType = "5v5 Ranked Solo",
                MapName = "Summoner's Rift"
            },
            new GameMode
            {
                ModeId = 430,
                GameType = "5v5 Blind Pick",
                MapName = "Summoner's Rift"
            },
            new GameMode
            {
                ModeId = 440,
                GameType = "5v5 Ranked Flex",
                MapName = "Summoner's Rift"
            },
            new GameMode
            {
                ModeId = 450,
                GameType = "5v5 ARAM",
                MapName = "Howling Abyss"
            },
            new GameMode
            {
                ModeId = 460,
                GameType = "3v3 Blind Pick",
                MapName = "Twisted Treeline"
            },
            new GameMode
            {
                ModeId = 470,
                GameType = "3v3 Ranked Flex",
                MapName = "Twisted Treeline"
            },
            new GameMode
            {
                ModeId = 600,
                GameType = "Blood Hunt Assassin",
                MapName = "Summoner's Rift"
            },
            new GameMode
            {
                ModeId = 610,
                GameType = "Dark Star: Singularity",
                MapName = "Cosmic Ruins"
            },
            new GameMode
            {
                ModeId = 700,
                GameType = "Clash",
                MapName = "Summoner's Rift"
            },
            new GameMode
            {
                ModeId = 800,
                GameType = "Co-op vs. AI Intermediate Bot",
                MapName = "Twisted Treeline"
            },
            new GameMode
            {
                ModeId = 810,
                GameType = "Co-op vs. AI Intro Bot",
                MapName = "Twisted Treeline"
            },
            new GameMode
            {
                ModeId = 820,
                GameType = "Co-op vs. AI Beginner Bot",
                MapName = "Twisted Treeline"
            },
            new GameMode
            {
                ModeId = 830,
                GameType = "Co-op vs. AI Intro Bot",
                MapName = "Summoner's Rift"
            },
            new GameMode
            {
                ModeId = 840,
                GameType = "Co-op vs. AI Beginner Bot",
                MapName = "Summoner's Rift"
            },
            new GameMode
            {
                ModeId = 850,
                GameType = "Co-op vs. AI Intermediate Bot",
                MapName = "Summoner's Rift"
            },
            new GameMode
            {
                ModeId = 900,
                GameType = "ARURF",
                MapName = "Summoner's Rift"
            },
            new GameMode
            {
                ModeId = 910,
                GameType = "Ascension",
                MapName = "Crystal Scar"
            },
            new GameMode
            {
                ModeId = 920,
                GameType = "Legend of the Poro King",
                MapName = "Howling Abyss"
            },
            new GameMode
            {
                ModeId = 940,
                GameType = "Nexus Siege",
                MapName = "Summoner's Rift"
            },
            new GameMode
            {
                ModeId = 950,
                GameType = "Doom Bots Voting",
                MapName = "Summoner's Rift"
            },
            new GameMode
            {
                ModeId = 960,
                GameType = "Doom Bots Standard",
                MapName = "Summoner's Rift"
            },
            new GameMode
            {
                ModeId = 980,
                GameType = "Star Guardian Invasion: Normal",
                MapName = "Valoran City Park"
            },
            new GameMode
            {
                ModeId = 990,
                GameType = "Star Guardian Invasion: Onslaught",
                MapName = "Valoran City Park"
            },
            new GameMode
            {
                ModeId = 1000,
                GameType = "PROJECT: Hunters",
                MapName = "Overcharge"
            },
            new GameMode
            {
                ModeId = 1010,
                GameType = "Snow ARURF",
                MapName = "Summoner's Rift"
            },
            new GameMode
            {
                ModeId = 1020,
                GameType = "One for All",
                MapName = "Summoner's Rift"
            },
            new GameMode
            {
                ModeId = 1030,
                GameType = "Odyssey Extraction: Intro",
                MapName = "Crash Site"
            },
            new GameMode
            {
                ModeId = 1040,
                GameType = "Odyssey Extraction: Cadet",
                MapName = "Crash Site"
            },
            new GameMode
            {
                ModeId = 1050,
                GameType = "Odyssey Extraction: Crewmember",
                MapName = "Crash Site"
            },
            new GameMode
            {
                ModeId = 1060,
                GameType = "Odyssey Extraction: Captain",
                MapName = "Crash Site"
            },
            new GameMode
            {
                ModeId = 1070,
                GameType = "Odyssey Extraction: Onslaught",
                MapName = "Crash Site"
            },
            new GameMode
            {
                ModeId = 1200,
                GameType = "Nexus Blitz",
                MapName = "Nexus Blitz"
            }
        };
    }
}