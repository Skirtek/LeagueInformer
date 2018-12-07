using System;
using System.Threading.Tasks;
using LeagueInformer.Api;
using LeagueInformer.Enums;
using LeagueInformer.Interfaces;
using LeagueInformer.Models;
using Newtonsoft.Json.Linq;

namespace LeagueInformer.Services
{
    public class GetLeagueOfSummoner : IGetLeagueOfSummonerInformation
    {
        private readonly ApiClient _apiClient = new ApiClient();

        public async Task<LeagueOfSummoner> GetLeagueOfSummonerInformation(string id)
        {
            try
            {
                //JObject response = JObject.Parse(await _apiClient.GetJsonFromUrl(
                //    $"https://eun1.api.riotgames.com/lol/league/v4/positions/by-summoner/{id}?api_key={AppSettings.AuthorizationApiKey}"));

                JArray response = JArray.Parse(await _apiClient.GetJsonFromUrl(
                    $"https://eun1.api.riotgames.com/lol/league/v4/positions/by-summoner/7n5j9NtjR5MO6gCvlmYfQWnxD6mhCrHD43Q8CJ3SVCksbns?api_key=RGAPI-10da1cca-dedc-4293-9c4d-2754a8497acf"));
                var data = JObject.FromObject(response[0]);
                return response == null ? new LeagueOfSummoner { IsSuccess = false } :
                    new LeagueOfSummoner
                    {
                        IsSuccess = true,
                        summonerName = data.GetValue("summonerName").ToString(),
                        tier = data.GetValue("tier").ToString(),
                        wins = data.GetValue("wins").ToString(),
                        losses = data.GetValue("losses").ToString(),
                        leagueName = data.GetValue("leagueName").ToString(),
                        queueType = data.GetValue("queueType").ToString()
                    };
            }
            catch (Exception ex)
            {
                return new LeagueOfSummoner { IsSuccess = false, Message = Error_Handler(ex.Message) };
            }
        }

        private string Error_Handler(string message)
        {
            switch (message)
            {
                case string error when error.Contains("404"):
                    return _apiClient.MapErrorToString(ErrorEnum.NotFound);
                case string error when error.Contains("422"):
                    return _apiClient.MapErrorToString(ErrorEnum.PlayerHasNotMatchHistory);
                case string error when error.Contains("504"):
                    return _apiClient.MapErrorToString(ErrorEnum.RequestTimeout);
                case string error when error.Contains("500") || error.Contains("502") || error.Contains("503"):
                    return _apiClient.MapErrorToString(ErrorEnum.InternalServerError);
                case string error when error.Contains("400") || error.Contains("401")
                                                             || error.Contains("403") || error.Contains("404")
                                                             || error.Contains("415") || error.Contains("429"):
                    return _apiClient.MapErrorToString(ErrorEnum.RequestAppError);
                default:
                    return _apiClient.MapErrorToString(ErrorEnum.DownloadingError);
            }
        }
    }
}