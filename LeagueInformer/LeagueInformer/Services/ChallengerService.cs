using LeagueInformer.Api;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueInformer.Enums;
using LeagueInformer.Models;

namespace LeagueInformer.Services
{
    public class ChallengerService
    {
        private readonly ApiClient _apiClient = new ApiClient();

        public async Task<string> GetInformationAboutChallangerList(string queue) // RANKED SOLO 5x5
        {
            try
            {
                JObject response = JObject.Parse(await _apiClient.GetJsonFromUrl(
                    $"https://eun1.api.riotgames.com/lol/league/v3/challengerleagues/by-queue/{queue}?api_key={AppSettings.AuthorizationApiKey}"));
                JArray challengersArray = JArray.FromObject(response.GetValue("entries"));
                //foreach (var item in challengersArray)
                //{
                //    //challengersArray.
                //}
                var igrek = challengersArray.ToList();
                return "XDDDDDD";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return _apiClient.MapErrorToString(ErrorEnum.AppError);
            }
        }
    }
}
