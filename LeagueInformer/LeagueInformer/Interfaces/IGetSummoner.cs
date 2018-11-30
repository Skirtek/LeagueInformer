using System.Threading.Tasks;
using LeagueInformer.Models;

namespace LeagueInformer.Interfaces
{
   public interface IGetSummoner
   {
        /// <summary>
        /// Gets information about summoner with given nickname
        /// </summary>
        /// <param name="nickname">Nickname given by user</param>
        Task<Summoner> GetInformationAboutSummoner(string nickname);
   }
}