using Newtonsoft.Json;

namespace LeagueInformer.Models
{
    public class Game
    {
        [JsonProperty("lane")]
        public string Lane { get; set; }

        [JsonProperty("gameId")]
        public string GameId { get; set; }

        [JsonProperty("champion")]
        public string Champion { get; set; }

        [JsonProperty("timestamp")]
        public string Date { get; set; }
    }
}