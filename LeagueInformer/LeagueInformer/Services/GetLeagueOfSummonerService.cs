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