using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LeagueInformer.Api;
using LeagueInformer.Enums;
using LeagueInformer.Interfaces;
using LeagueInformer.Models;
using Newtonsoft.Json.Linq;

namespace LeagueInformer.Services
{
    public class GetChallengersService : IGetChallengers
    {
        private readonly ApiClient _apiClient = new ApiClient();

        public async Task<ChallengersList> GetListOfChallengers() //Zbieramy tylko dla EUNE, wiec nie pobieramy serwera poki co
        {
            try
            {
                List<Challengers> challengersList = new List<Challengers>();
                JObject response = JObject.Parse(await _apiClient.GetJsonFromUrl(
                    $"https://eun1.api.riotgames.com/lol/league/v4/masterleagues/by-queue/RANKED_SOLO_5x5?api_key={AppSettings.AuthorizationApiKey}"));

                if (response == null || !(response["entries"] is JArray challengersArray))
                {
                    return new ChallengersList
                    {
                        IsSuccess = false,
                        Message = _apiClient.MapErrorToString(ErrorEnum.DownloadingError)
                    };
                }
                    
                foreach (var challenger in challengersArray)
                {
                    challengersList.Add(challenger.ToObject<Challengers>());
                }

                return new ChallengersList
                {
                    IsSuccess = true,
                    ChallengersResponseList = challengersList
                };
            }
            catch (Exception ex)
            {
                return new ChallengersList
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }
    }
}