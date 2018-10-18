using System.Net.Http;
using System.Threading.Tasks;
using LeagueInformer.Enums;
using LeagueInformer.Resources;

namespace LeagueInformer.Api
{
   public class ApiClient
    {
        private readonly HttpClient _client = new HttpClient();

        public async Task<string> GetJsonFromUrl(string url)
        {
            return await _client.GetStringAsync(url);
        }

        public string MapErrorToString(ErrorEnum error)
        {
            switch (error)
            {
                case ErrorEnum.DownloadingError:
                    return AppResources.Error_DownloadingData;
                default:
                    return AppResources.Error_Undefined;
            }
        }
    }
}