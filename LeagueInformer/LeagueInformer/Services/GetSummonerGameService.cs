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

                return response == null ? new SummonerGame { IsSuccess = false } :
                    new SummonerGame
                    {
                        IsSuccess = true,
                        gameMode = response.GetValue("gameMode").ToString()
                    };
            }
            catch (Exception ex)
            {
                return new SummonerGame { IsSuccess = false, Message = Error_Handler(ex.Message) };
            }
        }


        //TODO W ramach sprzątania zrobić z tego metodę generyczną
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