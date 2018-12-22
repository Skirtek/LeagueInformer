using System;
using System.Net;
using LeagueInformer.Interfaces;

namespace LeagueInformer.Services
{
    public class ConnectionService: IConnection
    {
        public bool HasInternetConnection()
        {
            var address = new Uri(AppSettings.CheckInternetConnectionString);
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(address);
                request.Timeout = 5000;
                request.Credentials = CredentialCache.DefaultCredentials;
                var response = (HttpWebResponse)request.GetResponse();

                return response.StatusCode == HttpStatusCode.NoContent;
            }
            catch
            {
                return false;
            }
        }
    }
}