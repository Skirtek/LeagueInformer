using System.Collections.Generic;

namespace LeagueInformer.Models
{
    public class MastersList
    {
        /// <summary>
        /// Indicates that response was success or not
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Contains error message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// List of all passed in JSON masters
        /// </summary>
        public List<Masters> MastersResponseList { get; set; }
    }
}