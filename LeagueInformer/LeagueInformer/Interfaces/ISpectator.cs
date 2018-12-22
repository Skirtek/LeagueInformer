using LeagueInformer.Models;

namespace LeagueInformer.Interfaces
{
    public interface ISpectator
    {
        /// <summary>
        /// Allows user to open match in League of Legends client
        /// </summary>
        /// <param name="encryptionKey">Encryption key used to access game</param>
        /// <param name="details">Object with game id</param>
        /// <param name="regionCode">Server region code</param>
        /// <param name="serverAddress">Server address suffix</param>
        /// <param name="port">Server port</param>
        /// <returns></returns>
        bool OpenSpectateClient(string encryptionKey, SummonerGameDetails details, string regionCode, string serverAddress, string port);
    }
}