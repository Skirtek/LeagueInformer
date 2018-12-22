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

        public async Task<Summoner> GetInformationAboutSummoner(string nickname, string region = "eun1")
        {
            try
            {
                JObject response = JObject.Parse(await _apiClient.GetJsonFromUrl(
                    $"https://{region}.api.riotgames.com/lol/summoner/v4/summoners/by-name/{nickname}?api_key={AppSettings.AuthorizationApiKey}"));

                return response == null ? new Summoner {IsSuccess = false} : 
                    new Summoner
                    {
                        IsSuccess = true,
                        Name = response.GetValue("name").ToString(),
                        Puuid = response.GetValue("puuid").ToString(),
                        Id = response.GetValue("id").ToString()
                    };
            }
            catch (Exception ex)
            {
                return new Summoner {IsSuccess = false,Message = Error_Handler(ex.Message)};
            }
        }

        public static string GetChampionForId(string id)
        {
            return id != null
                ? int.TryParse(id, out var championId) && Enum.IsDefined(typeof(Champions), championId)
                    ? ((Champions) championId).ToString()
                    : Champions.Nieznany.ToString()
                : Champions.Nieznany.ToString();
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