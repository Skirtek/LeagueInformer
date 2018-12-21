using System;
using System.Threading.Tasks;
using LeagueInformer.Api;
using LeagueInformer.Enums;
using LeagueInformer.Interfaces;
using LeagueInformer.Models;
using Newtonsoft.Json.Linq;

namespace LeagueInformer.Services
{
    public class GetSummonerGame : IGetSummonerGameInformation
    {
        private readonly ApiClient _apiClient = new ApiClient();

        public async Task<SummonerGame> GetSummonerGameInformation(string id)
        {
            try
            {//TODO sprawdzenie z kąd są pobierane dane JArray/JObject
                JArray response = JArray.Parse(await _apiClient.GetJsonFromUrl(
                   $"https://eun1.api.riotgames.com/lol/spectator/v4/active-games/by-summoner/{encryptedSummonerId}"));

                var data = JObject.FromObject(response[0]).ToObject<SummonerGame>();
                return new SummonerGame
                {
                    IsSuccess = true,
                    summonerName = data.GetValue("summonerName").ToString(),
                    //co to za mecz
                    gameMode = data.GetValue("gameMode").ToString(),
                };
            }
            catch (Exception ex)
            {
                return new SummonerGame { IsSuccess = false, Message = Error_Handler(ex.Message) };
            }
        }