using System.Collections.Generic;

namespace LeagueInformer.Models
{
    public class LeagueOfSummoner
    {
        /// <summary>
        /// Necessary data for LeagueOfSummoner
        /// </summary>
        public bool IsSuccess { get; set; }
        
        public string Message { get; set; }

        public List<SummonerLeagueInfo> SummonerLeagueInfoList { get; set; }
    }
}