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
    public class GetLeagueInfoService : IGetLeagueInfo
    {
        private readonly ApiClient _apiClient = new ApiClient();

        public async Task<LeagueList> GetListOfSummonerLeague(string summonerName, LeagueOfSummoner summonerLeagueDetails, string regionCode = "eun1")
        {
            try
            {
                List<LeagueDetails> leagueMembersList = new List<LeagueDetails>();


                JObject response = JObject.Parse(await _apiClient.GetJsonFromUrl(
                    $"https://{regionCode}.api.riotgames.com/lol/league/v4/leagues/{summonerLeagueDetails.SummonerLeagueInfo.LeagueId}?api_key={AppSettings.AuthorizationApiKey}"));

                if (response == null || !(response["entries"] is JArray leagueMembersArray))
                {
                    return new LeagueList
                    {
                        IsSuccess = false,
                        Message = _apiClient.MapErrorToString(ErrorEnum.DownloadingError)
                    };
                }

                foreach (var member in leagueMembersArray)
                {
                    leagueMembersList.Add(member.ToObject<LeagueDetails>());
                }

                return new LeagueList
                {
                    IsSuccess = true,
                    LeagueDetailsResponseList = leagueMembersList,
                    LeagueInfo = summonerLeagueDetails
                };
            }
            catch (Exception ex)
            {
                return new LeagueList
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }
    }
}
