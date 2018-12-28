using System.Net.Http;
using System.Threading.Tasks;
using LeagueInformer.Api.Interfaces;

namespace LeagueInformer.Api
{
    public class HttpClientHandler : IHttpClient
    {
        private readonly HttpClient _client = new HttpClient();

        public async Task<string> GetStringAsync(string requestedUrl) => await _client.GetStringAsync(requestedUrl);
    }
}