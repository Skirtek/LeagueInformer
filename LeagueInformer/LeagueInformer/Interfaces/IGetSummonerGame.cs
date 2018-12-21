using System.Threading.Tasks;
using LeagueInformer.Models;

namespace LeagueInformer.Interfaces
{
    public interface IGetSummonerGameInformation
    {
        /// <summary>
        /// Gets information about summoner with given name
        /// </summary>
        Task<SummonerGame> GetSummonerGameInformation(string id);
    }
}