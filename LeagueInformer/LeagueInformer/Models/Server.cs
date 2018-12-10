using LeagueInformer.Enums;
using Newtonsoft.Json;

namespace LeagueInformer.Models
{
    public class Server
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// Indicates status of response defined in enum
        /// </summary>
        public ServerStatus ServerStatusState { get; set; }
    }
}