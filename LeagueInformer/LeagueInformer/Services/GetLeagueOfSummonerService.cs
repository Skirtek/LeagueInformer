using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LeagueInformer.Api.Interfaces;
using LeagueInformer.Interfaces;
using LeagueInformer.Models;
using LeagueInformer.Utils.Interfaces;
using Newtonsoft.Json.Linq;

namespace LeagueInformer.Services
{
    public class GetLeagueOfSummoner : IGetLeagueOfSummonerInformation
    {
        private readonly IApiClient _apiClient;
        private readonly IErrorHandler _errorHandler;

        #region CTOR       
        public GetLeagueOfSummoner(IApiClient apiClient, IErrorHandler errorHandler)
        {
            _apiClient = apiClient;
            _errorHandler = errorHandler;
        }
        #endregion

        public async Task<LeagueOfSummoner> GetLeagueOfSummonerInformation(string id, string region = "eun1")
        {
            try
            {
                var queueList = new List<SummonerLeagueInfo>();
                JArray response = JArray.Parse(await _apiClient.GetJsonFromUrl(
                   $"https://{region}.api.riotgames.com/lol/league/v4/positions/by-summoner/{id}?api_key={AppSettings.AuthorizationApiKey}"));

                if (response.Count < 1)
                {
                    return new LeagueOfSummoner
                    {
                        IsSuccess = false,
                        Message = _errorHandler.Error_Handler("404")
                    };
                }

                foreach (var queue in response)
                {
                    queueList.Add(queue.ToObject<SummonerLeagueInfo>());
                }

                return new LeagueOfSummoner
                {
                    IsSuccess = true,
                    SummonerLeagueInfoList = queueList
                };
            }
            catch (Exception ex)
            {
                return new LeagueOfSummoner { IsSuccess = false, Message = _errorHandler.Error_Handler(ex.Message) };
            }
        }
    }
}