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
        private static readonly GetSummonerService SummonerService = new GetSummonerService();
        private static readonly GetLeagueOfSummoner LeagueOfSummonerService = new GetLeagueOfSummoner();

        public async Task<LeagueList> GetListOfSummonerLeague(string summonerName)
        {
            try
            {
                List<LeagueDetails> leagueMembersList = new List<LeagueDetails>();
                var summoner = await SummonerService.GetInformationAboutSummoner(summonerName);
                var summonerLeague = await LeagueOfSummonerService.GetLeagueOfSummonerInformation(summoner.Id);

                JObject response = JObject.Parse(await _apiClient.GetJsonFromUrl(
                    $"https://eun1.api.riotgames.com/lol/league/v4/leagues/{summonerLeague.SummonerLeagueInfo.LeagueId}?api_key={AppSettings.AuthorizationApiKey}"));

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
                    LeagueInfo = summonerLeague
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
