using System.Threading.Tasks;
using LeagueInformer.Models;

namespace LeagueInformer.Interfaces
{
    public interface IGetMasters
    {
        /// <summary>
        /// Returns league details about master league
        /// </summary>
        /// <returns></returns>
        Task<MastersList> GetListOfMasterLeague();
    }
}