using System.Threading.Tasks;
using LeagueInformer.Enums;

namespace LeagueInformer.Api.Interfaces
{
    public interface IApiClient
    {
        Task<string> GetJsonFromUrl(string url);

        string MapErrorToString(ErrorEnum error);
    }
}
