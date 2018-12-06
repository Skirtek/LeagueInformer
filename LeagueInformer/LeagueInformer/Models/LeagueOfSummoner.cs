namespace LeagueInformer.Models
{
    public class Summoner
    {

        /// <summary>
        /// necessery data for LeagueOfSummoner
        /// </summary>
        public string summonerName { get; set; }

        public string tier { get; set; }

        public string wins { get; set; }

        public string losses { get; set; }

        public string leagueName { get; set; }

        public string queueType { get; set; }
    }
}