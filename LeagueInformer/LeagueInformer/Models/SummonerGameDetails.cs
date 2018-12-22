using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LeagueInformer.Models
{
    public class SummonerGameDetails
    {
        [JsonProperty("gameMode")]
        public string GameMode { get; set; }

        [JsonProperty("Observers")]
        public JToken Observers { get; set; }

        [JsonProperty("gameId")]
        public string GameId { get; set; }
    }
}