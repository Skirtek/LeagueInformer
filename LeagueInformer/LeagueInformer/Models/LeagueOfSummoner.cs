namespace LeagueInformer.Models
{
    public class LeagueOfSummoner
    {

        /// <summary>
        /// necessery data for LeagueOfSummoner
        /// </summary>
         
        public bool IsSuccess { get; set; }
        
        public string Message { get; set; }

        public string summonerName { get; set; }

        public string tier { get; set; }

        public string wins { get; set; }

        public string losses { get; set; }

        public string leagueName { get; set; }

        public string queueType { get; set; }
    }
}