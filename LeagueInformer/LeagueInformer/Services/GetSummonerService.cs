using System;
using System.Threading.Tasks;
using LeagueInformer.Api;
using LeagueInformer.Enums;
using LeagueInformer.Interfaces;
using LeagueInformer.Models;
using LeagueInformer.Utils;
using LeagueInformer.Utils.Interfaces;
using Newtonsoft.Json.Linq;

namespace LeagueInformer.Services
{
    public class GetSummonerService : IGetSummoner
    {
        private readonly ApiClient _apiClient = new ApiClient();
        private readonly IErrorHandler _errorHandler = new ErrorHandler();

        public async Task<Summoner> GetInformationAboutSummoner(string nickname, string region = "eun1")
        {
            try
            {
                JObject response = JObject.Parse(await _apiClient.GetJsonFromUrl(
                    $"https://{region}.api.riotgames.com/lol/summoner/v4/summoners/by-name/{nickname}?api_key={AppSettings.AuthorizationApiKey}"));

                return response == null ? new Summoner { IsSuccess = false } :
                    new Summoner
                    {
                        IsSuccess = true,
                        Name = response.GetValue("name").ToString(),
                        Puuid = response.GetValue("puuid").ToString(),
                        Id = response.GetValue("id").ToString(),
                        AccountId = response.GetValue("accountId").ToString()
                    };
            }
            catch (Exception ex)
            {
                return new Summoner { IsSuccess = false, Message = _errorHandler.Error_Handler(ex.Message) };
            }
        }

        public string GetChampionForId(string id)
        {
            return id != null
                ? int.TryParse(id, out var championId) && Enum.IsDefined(typeof(Champions), championId)
                    ? ((Champions)championId).ToString()
                    : Champions.Nieznany.ToString()
                : Champions.Nieznany.ToString();
        }
    }
}