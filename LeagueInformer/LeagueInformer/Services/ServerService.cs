using System;
using System.Threading.Tasks;
using LeagueInformer.Api;
using LeagueInformer.Enums;
using LeagueInformer.Interfaces;
using LeagueInformer.Models;
using Newtonsoft.Json.Linq;

namespace LeagueInformer.Services
{
    public class ServerService: IServerService
    {
        private readonly ApiClient _apiClient = new ApiClient();

        public async Task<ServerStatusResponse> GetServerStatus(string serverName)
        {
            try
            {
                JObject response = JObject.Parse(await _apiClient.GetJsonFromUrl(
                    $"https://eun1.api.riotgames.com/lol/status/v3/shard-data?api_key={AppSettings.AuthorizationApiKey}"));

                if (response == null)
                {
                    return new ServerStatusResponse
                    {
                        IsSuccess = false,
                        Message = ServerStatus.Error.ToString()
                    };
                }

                JArray servicesStatus = JArray.FromObject(response.GetValue("services"));
                foreach (var status in servicesStatus)
                {
                    var x = status.ToObject<Server>();
                }

                return new ServerStatusResponse
                {
                    IsSuccess = true,
                    //Name = response.GetValue("name").ToString()
                };
            }
            catch (Exception ex)
            {
                return new ServerStatusResponse
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }
    }
}
