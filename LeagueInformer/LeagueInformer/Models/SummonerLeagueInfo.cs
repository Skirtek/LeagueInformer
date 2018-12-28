using Newtonsoft.Json;

namespace LeagueInformer.Models
{
    public class SummonerLeagueInfo
    {
        /// <summary>
        /// Name of summoner
        /// </summary>
        [JsonProperty("summonerName")]
        public string SummonerName { get; set; }

        /// <summary>
        /// Summoner tier e.g GOLD, BRONZE etc.
        /// </summary>
        [JsonProperty("tier")]
        public string Tier { get; set; }

        /// <summary>
        /// Summoner rank e.g IV, III, II etc.
        /// </summary>
        [JsonProperty("rank")]
        public string Rank { get; set; }

        /// <summary>
        /// Number of summoner wins
        /// </summary>
        [JsonProperty("wins")]
        public string Wins { get; set; }

        /// <summary>
        /// Number of summoner losses
        /// </summary>
        [JsonProperty("losses")]
        public string Losses { get; set; }

        /// <summary>
        /// Name of league where summoner is a member
        /// </summary>
        [JsonProperty("leagueName")]
        public string LeagueName { get; set; }

        /// <summary>
        /// Queue type where summoner participates
        /// </summary>
        [JsonProperty("queueType")]
        public string QueueType { get; set; }

        /// <summary>
        /// Id used to recognize summoner's league
        /// </summary>
        [JsonProperty("leagueId")]
        public string LeagueId { get; set; }
    }
}