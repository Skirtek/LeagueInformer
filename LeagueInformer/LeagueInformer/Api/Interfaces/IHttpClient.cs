using System.Threading.Tasks;

namespace LeagueInformer.Api.Interfaces
{
    public interface IHttpClient
    {
        Task<string> GetStringAsync(string requestedUrl);
    }
}