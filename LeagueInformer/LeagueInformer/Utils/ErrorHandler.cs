using LeagueInformer.Api;
using LeagueInformer.Enums;
using LeagueInformer.Utils.Interfaces;

namespace LeagueInformer.Utils
{
    public class ErrorHandler : IErrorHandler
    {
        private readonly IApiClient _apiClient;

        #region CTOR
        public ErrorHandler()
        {
            _apiClient = new ApiClient();
        }       
        #endregion

        public string Error_Handler(string message)
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
                case string error when error.Contains("400")
                                       || error.Contains("401") || error.Contains("403")
                                       || error.Contains("415") || error.Contains("429"):
                    return _apiClient.MapErrorToString(ErrorEnum.RequestAppError);
                default:
                    return _apiClient.MapErrorToString(ErrorEnum.DownloadingError);
            }
        }
    }
}