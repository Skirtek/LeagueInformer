using Newtonsoft.Json;

namespace LeagueInformer.Models
{
    public class Masters
    {
        [JsonProperty("summonerName")]
        public string SummonerName { get; set; }

        [JsonProperty("leaguePoints")]
        public int Points { get; set; }

        [JsonProperty("wins")]
        public int Wins { get; set; }

        [JsonProperty("losses")]
        public int Losses { get; set; }

        [JsonProperty("hotStreak")]
        public bool HotStreak { get; set; }

        [JsonProperty("veteran")]
        public bool Veteran { get; set; }
    }
}