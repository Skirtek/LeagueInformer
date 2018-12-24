namespace LeagueInformer.Utils.Interfaces
{
    public interface IDateHandler
    {
        /// <summary>
        /// Parses time in milliseconds to human readable date
        /// </summary>
        /// <param name="time">Time in milliseconds</param>
        /// <returns></returns>
        string ParseTimeToDate(string time);
    }
}