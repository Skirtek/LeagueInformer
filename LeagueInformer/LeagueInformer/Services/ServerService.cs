using System;
using System.Threading.Tasks;
using LeagueInformer.Api;
using LeagueInformer.Interfaces;
using Newtonsoft.Json.Linq;

namespace LeagueInformer.Services
{
    public class ServerService: IServerService
    {
        private readonly ApiClient _apiClient = new ApiClient();

        public async Task<bool> GetServerStatus(string serverName)
        {
            try
            {
                JObject response = JObject.Parse(await _apiClient.GetJsonFromUrl(
                    $"https://eun1.api.riotgames.com/lol/status/v3/shard-data?api_key={AppSettings.AuthorizationApiKey}"));
                var x = response.GetValue("name").ToString();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
