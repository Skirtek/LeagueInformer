using System.Threading.Tasks;
using LeagueInformer.Models;

namespace LeagueInformer.Interfaces
{
    public interface IGetMasters
    {
        Task<MastersList> GetListOfMasterLeague();
    }
}