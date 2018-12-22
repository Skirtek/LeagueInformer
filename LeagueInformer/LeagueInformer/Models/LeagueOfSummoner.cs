namespace LeagueInformer.Models
{
    public class LeagueOfSummoner
    {
        /// <summary>
        /// Necessary data for LeagueOfSummoner
        /// </summary>
        public bool IsSuccess { get; set; }
        
        public string Message { get; set; }

        public SummonerLeagueInfo SummonerLeagueInfo { get; set; }
    }
}