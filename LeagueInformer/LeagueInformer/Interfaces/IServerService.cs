using System.Threading.Tasks;
using LeagueInformer.Models;

namespace LeagueInformer.Interfaces
{
    public interface IServerService
    {
        /// <summary>
        /// Indicates if server is online or not
        /// </summary>
        /// <param name="serverName"></param>
        /// <returns></returns>
        Task<ServerStatusResponse> GetServerStatus(string serverName);
    }
}