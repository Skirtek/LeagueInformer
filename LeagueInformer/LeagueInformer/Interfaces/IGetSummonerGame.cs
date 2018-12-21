using System.Threading.Tasks;
using LeagueInformer.Models;

namespace LeagueInformer.Interfaces
{
    public interface IGetSummonerGame
    {
        /// <summary>
        /// Gets information about summoner with given nickname
        /// </summary>
        Task<SummonerGame> GetSummonerGameInformation(string id);
    }
}
