using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LeagueInformer.Api;
using LeagueInformer.Api.Interfaces;
using LeagueInformer.Enums;
using LeagueInformer.Interfaces;
using LeagueInformer.Models;
using Newtonsoft.Json.Linq;

namespace LeagueInformer.Services
{
    public class ServerService: IServerService
    {
        private readonly IApiClient _apiClient;

        #region CTOR
        public ServerService(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }
        #endregion

        public async Task<ServerStatusResponse> GetServerStatus(string serverName)
        {
            try
            {
                var servicesList = new List<Server>();
                JObject response = JObject.Parse(await _apiClient.GetJsonFromUrl(
                    $"https://{serverName}.api.riotgames.com/lol/status/v3/shard-data?api_key={AppSettings.AuthorizationApiKey}"));

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
                    var serverService = status.ToObject<Server>();
                    serverService.ServerStatusState = serverService.Status == "online" 
                                                          ? ServerStatus.Online
                                                          : ServerStatus.Offline;
                    servicesList.Add(serverService);
                }

                return new ServerStatusResponse
                {
                    IsSuccess = true,
                    Name = response.GetValue("name").ToString(),
                    ServicesStatuses = servicesList
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