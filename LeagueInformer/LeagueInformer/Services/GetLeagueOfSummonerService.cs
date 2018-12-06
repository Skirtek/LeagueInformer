using System;
using System.Threading.Tasks;
using LeagueInformer.Api;
using LeagueInformer.Enums;
using LeagueInformer.Interfaces;
using LeagueInformer.Models;
using Newtonsoft.Json.Linq;

namespace LeagueInformer.Services
{
    public class GetSummonerService : IGetSummoner
    {
        private readonly ApiClient _apiClient = new ApiClient();

        public async Task<Summoner> GetInformationAboutSummoner(string id)
        {
            try
            {
                JObject response = JObject.Parse(await _apiClient.GetJsonFromUrl(
                    $"https://eun1.api.riotgames.com/lol/league/v4/positions/by-summoner/{id}?api_key={AppSettings.AuthorizationApiKey}"));

                return response == null ? new Summoner { IsSuccess = false } :
                    new Summoner
                    {
                        IsSuccess = true,
                        summonerName = response.GetValue("summonerName").ToString(),
                        tier = response.GetValue("tier").ToString(),
                        wins = response.GetValue("wins").ToString(),
                        losses = response.GetValue("losses").ToString(),
                        leagueName = response.GetValue("leagueName").ToString(),
                        queueType = response.GetValue("queueType").ToString()
                    };
            }
            catch (Exception ex)
            {
                return new Summoner { IsSuccess = false, Message = Error_Handler(ex.Message) };
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