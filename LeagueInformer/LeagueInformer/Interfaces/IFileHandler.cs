using System.Collections.Generic;
using System.Threading.Tasks;

namespace LeagueInformer.Interfaces
{
    public interface IFileHandler
    {
        /// <summary>
        /// Gets list of saved nicknames in file
        /// </summary>
        /// <returns></returns>
        List<string> GetListOfLastNicknames();

        /// <summary>
        /// Saves entered nickname to file
        /// </summary>
        /// <param name="nickname"></param>
        /// <returns></returns>
        Task<bool> SaveNicknameToList(string nickname);

        bool CheckIfAppDirectoryExists(string path);

        bool CreateAppDirectory(string path);
    }
}