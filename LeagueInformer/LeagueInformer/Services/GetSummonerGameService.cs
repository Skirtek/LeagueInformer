using System;
using System.Threading.Tasks;
using LeagueInformer.Api;
using LeagueInformer.Enums;
using LeagueInformer.Interfaces;
using LeagueInformer.Models;
using Newtonsoft.Json.Linq;

namespace LeagueInformer.Services
{
    public class GetSummonerGame : IGetSummonerGame
    {
        private readonly ApiClient _apiClient = new ApiClient();

        public async Task<SummonerGame> GetSummonerGameInformation(string id)
        {
            try
            {

                JObject response = JObject.Parse(await _apiClient.GetJsonFromUrl(
                    $"https://eun1.api.riotgames.com/lol/spectator/v4/active-games/by-summoner/{id}?api_key={AppSettings.AuthorizationApiKey}"));

                return response == null
                    ? new SummonerGame { IsSuccess = false }
                    : new SummonerGame
                    {
                        IsSuccess = true,
                        gameMode = response.GetValue("gameMode").ToString()
                    };
            }
            catch (Exception ex)
            {
                return new SummonerGame { IsSuccess = false, Message = ex.Message };
            }
        }
    }
}