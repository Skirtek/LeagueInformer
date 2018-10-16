namespace LeagueInformer.Interfaces
{
    public interface IConnection
    {
        /// <summary>
        /// Checks if user has internet connection
        /// </summary>
        bool HasInternetConnection();
    }
}