using System;
using System.Threading.Tasks;
using LeagueInformer.Api;
using LeagueInformer.Api.Interfaces;
using LeagueInformer.Interfaces;
using LeagueInformer.Models;
using Newtonsoft.Json.Linq;

namespace LeagueInformer.Services
{
    public class GetSummonerGame : IGetSummonerGame
    {
        private readonly IApiClient _apiClient;

        #region CTOR
        public GetSummonerGame()
        {
            _apiClient = new ApiClient();
        }
        #endregion

        public async Task<SummonerGame> GetSummonerGameInformation(string id, string region = "eun1")
        {
            try
            {
                JObject response = JObject.Parse(await _apiClient.GetJsonFromUrl(
                    $"https://{region}.api.riotgames.com/lol/spectator/v4/active-games/by-summoner/{id}?api_key={AppSettings.AuthorizationApiKey}"));
               
                return response == null
                    ? new SummonerGame { IsSuccess = false }
                    : new SummonerGame
                    {
                        IsSuccess = true,
                        Details = response.ToObject<SummonerGameDetails>()
                    };
            }
            catch (Exception ex)
            {
                return new SummonerGame { IsSuccess = false, Message = ex.Message };
            }
        }
    }
}