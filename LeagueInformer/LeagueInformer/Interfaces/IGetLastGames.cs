using System.Threading.Tasks;
using LeagueInformer.Models;

namespace LeagueInformer.Interfaces
{
    public interface IGetLastGames
    {
        /// <summary>
        /// Returns list of last ten games for given summoner
        /// </summary>
        /// <param name="accountId">Encrypted accountId</param>
        /// <param name="regionCode"></param>
        /// <returns></returns>
        Task<GamesResponse> GetLastTenGames(string accountId, string regionCode);
    }
}