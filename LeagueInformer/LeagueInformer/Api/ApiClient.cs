using System.Threading.Tasks;
using LeagueInformer.Api.Interfaces;
using LeagueInformer.Enums;
using LeagueInformer.Resources;

namespace LeagueInformer.Api
{
    public class ApiClient : IApiClient
    {
        private readonly IHttpClient _client;

        public ApiClient(IHttpClient client)
        {
            _client = client;
        }

        public async Task<string> GetJsonFromUrl(string url)
        {
            return await _client.GetStringAsync(url);
        }

        public string MapErrorToString(ErrorEnum error)
        {
            switch (error)
            {
                case ErrorEnum.DownloadingError:
                    return AppResources.Error_DownloadingData;
                case ErrorEnum.NotFound:
                    return AppResources.Error_PlayerNotFound;
                case ErrorEnum.PlayerHasNotMatchHistory:
                    return AppResources.Error_PlayerWithoutGamesHistory;
                case ErrorEnum.RequestTimeout:
                    return string.Format(AppResources.Error_RequestTimedOut, AppResources.Common_TryAgainLater);
                case ErrorEnum.InternalServerError:
                    return string.Format(AppResources.Error_RiotServersAreDown, AppResources.Common_TryAgainLater);
                case ErrorEnum.RequestAppError:
                    return string.Format(AppResources.Error_RequestAppError, AppResources.Common_TryAgainLater);
                default:
                    return AppResources.Error_Undefined;
            }
        }
    }
}