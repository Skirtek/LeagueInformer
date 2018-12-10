using System.Threading.Tasks;

namespace LeagueInformer.Interfaces
{
    public interface IServerService
    {
        /// <summary>
        /// Indicates if server is online or not
        /// </summary>
        /// <param name="serverName"></param>
        /// <returns></returns>
        Task<bool> GetServerStatus(string serverName);
    }
}
