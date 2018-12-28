using System.Threading.Tasks;
using LeagueInformer.Models;

namespace LeagueInformer.Utils.Interfaces
{
    public interface IPrintMethods
    {
        ChosenServer PrintListOfSpectateServers();

        Task<string> PrintListOfSavedNicknames();
    }
}