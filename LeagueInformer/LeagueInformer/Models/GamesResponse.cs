using System.Collections.Generic;

namespace LeagueInformer.Models
{
    public class GamesResponse
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; }

        public List<Game> Games { get; set; }
    }
}