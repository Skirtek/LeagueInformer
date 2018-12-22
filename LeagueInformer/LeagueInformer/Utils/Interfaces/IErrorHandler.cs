namespace LeagueInformer.Utils.Interfaces
{
    public interface IErrorHandler
    {
        /// <summary>
        /// Translate error codes from response to error message
        /// </summary>
        /// <param name="message">Message given by Riot Servers response</param>
        /// <returns></returns>
        string Error_Handler(string message);
    }
}
