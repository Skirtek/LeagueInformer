using System.Collections.Generic;

namespace LeagueInformer.Models
{
    public class ServerStatusResponse
    {
        /// <summary>
        /// Property which contains name of server
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Indicates if response was successful
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// String which contains error message
        /// </summary>
        public string Message { get; set; }

        private List<Server> ServicesStatuses { get; set; }
    }
}