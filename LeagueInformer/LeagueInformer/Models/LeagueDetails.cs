using Newtonsoft.Json;

namespace LeagueInformer.Models
{
    public class LeagueDetails
    {
        [JsonProperty("summonerName")]
        public string SummonerName { get; set; }

        [JsonProperty("leaguePoints")]
        public int Points { get; set; }

        [JsonProperty("wins")]
        public int Wins { get; set; }

        [JsonProperty("losses")]
        public int Losses { get; set; }
    }
}