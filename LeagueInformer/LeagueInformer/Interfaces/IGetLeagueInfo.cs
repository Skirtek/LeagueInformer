using System.Threading.Tasks;
using LeagueInformer.Models;

namespace LeagueInformer.Interfaces
{
    public interface IGetLeagueInfo
    {
        /// <summary>
        /// Returns league details about given summoner league
        /// </summary>
        /// <returns></returns>
        Task<LeagueList> GetListOfSummonerLeague(string summonerName);
    }
}
