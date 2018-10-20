using System;
using System.Threading.Tasks;
using LeagueInformer.Api;
using LeagueInformer.Enums;
using LeagueInformer.Interfaces;
using LeagueInformer.Models;
using Newtonsoft.Json.Linq;

namespace LeagueInformer.Services
{
    public class GetSummonerService: IGetSummoner
    {
        private readonly ApiClient _apiClient = new ApiClient();

        public async Task<Summoner> GetInformationAboutSummoner(string nickname)
        {
            try
            {
                JObject response = JObject.Parse(await _apiClient.GetJsonFromUrl(
                    $"https://eun1.api.riotgames.com/lol/summoner/v3/summoners/by-name/{nickname}?api_key={AppSettings.AuthorizationApiKey}"));

                return response == null ? new Summoner {IsSuccess = false} : 
                    new Summoner
                    {
                        IsSuccess = true,
                        AccountId = (string)response.GetValue("accountId"),
                        Id = (string)response.GetValue("id")
                    };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new Summoner {IsSuccess = false};
            }
        }

        public string GetChampionForId(string id) => ((Champions)int.Parse(id)).ToString();
    }
}