using System.Collections.Generic;

namespace LeagueInformer.Models
{
    public class ChallengersList
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
        /// List of all passed in JSON challengers
        /// </summary>
        public List<Challengers> ChallengersResponseList { get; set; }
    }
}