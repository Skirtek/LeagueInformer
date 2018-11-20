namespace LeagueInformer.Enums
{
    public enum ErrorEnum
    {
        DownloadingError = 0,
        RequestAppError = 400,
        NotFound = 404,
        PlayerHasNotMatchHistory = 422,
        InternalServerError = 500,
        RequestTimeout = 504,
        AppError = 997
    }
}