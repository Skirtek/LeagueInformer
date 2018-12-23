using Newtonsoft.Json;

namespace LeagueInformer.Models
{
    public class Game
    {
        [JsonProperty("queue")]
        public int QueueId { get; set; }

        [JsonProperty("champion")]
        public string Champion { get; set; }

        [JsonProperty("timestamp")]
        public string Date { get; set; }

        [JsonProperty("season")]
        public int SeasonId { get; set; }
    }
}