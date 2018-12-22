namespace LeagueInformer.Models
{
    public class Summoner
    {
        /// <summary>
        /// summonerId used by Riot to get information about summoner
        /// </summary>
        public string Id { get; set; } 

        /// <summary>
        /// Nickname of summoner
        /// </summary>
        public string Name { get; set; }

        public string Puuid { get; set; }

        public string AccountId { get; set; }

        public bool IsSuccess { get; set; }

        public string Message { get; set; }
    }
}