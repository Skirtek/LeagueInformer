using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LeagueInformer.Api;
using LeagueInformer.Interfaces;
using LeagueInformer.Models;
using LeagueInformer.Utils;
using LeagueInformer.Utils.Interfaces;
using Newtonsoft.Json.Linq;

namespace LeagueInformer.Services
{
    public class GetLastGamesService : IGetLastGames
    {
        private readonly ApiClient _apiClient = new ApiClient();
        private readonly IErrorHandler _errorHandler = new ErrorHandler();

        public async Task<GamesResponse> GetLastTenGames(string accountId, string regionCode)
        {
            try
            {
                var matchesList = new List<Game>();
                JObject response = JObject.Parse(await _apiClient.GetJsonFromUrl(
                    $"https://{regionCode}.api.riotgames.com/lol/match/v4/matchlists/by-account/{accountId}?api_key={AppSettings.AuthorizationApiKey}"));

                if (response == null)
                {
                    return new GamesResponse
                    {
                        IsSuccess = false
                    };
                }

                bool getMatches = response.TryGetValue("matches", out JToken matches);

                if (!getMatches)
                {
                    return new GamesResponse
                    {
                        IsSuccess = false
                    };
                }

                JArray matchArray = JArray.FromObject(matches);
                foreach (var match in matchArray)
                {
                    matchesList.Add(match.ToObject<Game>());
                }

                return new GamesResponse
                {
                    IsSuccess = true,
                    Games = matchesList
                };
            }
            catch (Exception ex)
            {
                return new GamesResponse
                {
                    IsSuccess = false,
                    Message = _errorHandler.Error_Handler(ex.Message)
                };
            }
        }
    }
}