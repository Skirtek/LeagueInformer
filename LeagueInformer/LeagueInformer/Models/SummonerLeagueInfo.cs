using Newtonsoft.Json;

namespace LeagueInformer.Models
{
    public class SummonerLeagueInfo
    {
        //TODO Zmienić nazwy propertów na duże litery 
        //zgodnie z konwencją https://stackoverflow.com/questions/12045711/is-there-any-c-sharp-naming-convention-for-a-variable-used-in-a-property

        /// <summary>
        /// Name of summoner
        /// </summary>
        [JsonProperty("summonerName")]
        public string summonerName { get; set; }

        /// <summary>
        /// Summoner tier e.g GOLD, BRONZE etc.
        /// </summary>
        [JsonProperty("tier")]
        public string tier { get; set; }

        /// <summary>
        /// Summoner rank e.g IV, III, II etc.
        /// </summary>
        [JsonProperty("rank")]
        public string rank { get; set; }

        /// <summary>
        /// Number of summoner wins
        /// </summary>
        [JsonProperty("wins")]
        public string wins { get; set; }

        /// <summary>
        /// Number of summoner losses
        /// </summary>
        [JsonProperty("losses")]
        public string losses { get; set; }

        /// <summary>
        /// Name of league where summoner is a member
        /// </summary>
        [JsonProperty("leagueName")]
        public string leagueName { get; set; }

        /// <summary>
        /// Queue type where he participates
        /// </summary>
        [JsonProperty("queueType")]
        public string queueType { get; set; }
    }
}