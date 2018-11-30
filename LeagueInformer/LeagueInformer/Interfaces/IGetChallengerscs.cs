using System.Collections.Generic;
using System.Threading.Tasks;
using LeagueInformer.Models;

namespace LeagueInformer.Interfaces
{
    public interface IGetChallengers
    {
        Task<List<Challengers>> GetListOfChallengers();
    }
}