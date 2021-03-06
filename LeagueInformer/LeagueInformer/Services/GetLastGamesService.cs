﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LeagueInformer.Api.Interfaces;
using LeagueInformer.Interfaces;
using LeagueInformer.Models;
using LeagueInformer.Utils.Interfaces;
using Newtonsoft.Json.Linq;

namespace LeagueInformer.Services
{
    public class GetLastGamesService : IGetLastGames
    {
        private readonly IApiClient _apiClient;
        private readonly IErrorHandler _errorHandler;

        #region CTOR       
        public GetLastGamesService(IApiClient apiClient, IErrorHandler errorHandler)
        {
            _apiClient = apiClient;
            _errorHandler = errorHandler;
        }
        #endregion

        public async Task<GamesResponse> GetLastTenGames(string accountId, string regionCode)
        {
            try
            {
                var matchesList = new List<Game>();
                string response = await _apiClient.GetJsonFromUrl(
                    $"https://{regionCode}.api.riotgames.com/lol/match/v4/matchlists/by-account/{accountId}?api_key={AppSettings.AuthorizationApiKey}");

                if (string.IsNullOrEmpty(response))
                {
                    return new GamesResponse
                    {
                        IsSuccess = false
                    };
                }

                JObject parsedResponse = JObject.Parse(response);

                bool getMatches = parsedResponse.TryGetValue("matches", out JToken matches);

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