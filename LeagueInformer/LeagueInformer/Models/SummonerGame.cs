namespace LeagueInformer.Models
{
    public class SummonerGame
    {
        /// <summary>
        /// Necessary data for Summoner Game
        /// </summary>
        public bool IsSuccess { get; set; }

        public string Message { get; set; }

        public string summonerName { get; set; }

        public string gameMode { get; set; }
    }
}