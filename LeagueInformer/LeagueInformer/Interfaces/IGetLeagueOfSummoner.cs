using System.Threading.Tasks;
using LeagueInformer.Models;

namespace LeagueInformer.Interfaces
{
    public interface IGetLeagueOfSummonerInformation
    {
        /// <summary>
        /// Gets information about summoner with given nickname
        /// </summary>
        Task<LeagueOfSummoner> GetLeagueOfSummonerInformation(string id, string region);
    }
}
