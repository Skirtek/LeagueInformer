using System.Threading.Tasks;
using LeagueInformer.Enums;

namespace LeagueInformer.Api
{
    public interface IApiClient
    {
        Task<string> GetJsonFromUrl(string url);

        string MapErrorToString(ErrorEnum error);
    }
}
