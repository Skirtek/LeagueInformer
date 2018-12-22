using System;
using System.Threading.Tasks;
using LeagueInformer.Api;
using LeagueInformer.Interfaces;
using LeagueInformer.Models;
using LeagueInformer.Utils;
using LeagueInformer.Utils.Interfaces;
using Newtonsoft.Json.Linq;

namespace LeagueInformer.Services
{
    public class GetLeagueOfSummoner : IGetLeagueOfSummonerInformation
    {
        private readonly ApiClient _apiClient = new ApiClient();
        private readonly IErrorHandler _errorHandler = new ErrorHandler();

        public async Task<LeagueOfSummoner> GetLeagueOfSummonerInformation(string id, string region = "eun1")
        {
            try
            {
                JArray response = JArray.Parse(await _apiClient.GetJsonFromUrl(
                   $"https://{region}.api.riotgames.com/lol/league/v4/positions/by-summoner/{id}?api_key={AppSettings.AuthorizationApiKey}"));

                var data = JObject.FromObject(response[0]).ToObject<SummonerLeagueInfo>();
                return new LeagueOfSummoner
                {
                    IsSuccess = true,
                    SummonerLeagueInfo = data
                };
            }
            catch (Exception ex)
            {
                return new LeagueOfSummoner { IsSuccess = false, Message = _errorHandler.Error_Handler(ex.Message) };
            }
        }
    }
}