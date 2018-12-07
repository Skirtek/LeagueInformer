namespace LeagueInformer.Models
{
    public class LeagueOfSummoner
    {

        /// <summary>
        /// necessery data for LeagueOfSummoner
        /// </summary>
        //TODO Zmienić nazwy propertów na duże litery 
        //zgodnie z konwencją https://stackoverflow.com/questions/12045711/is-there-any-c-sharp-naming-convention-for-a-variable-used-in-a-property
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