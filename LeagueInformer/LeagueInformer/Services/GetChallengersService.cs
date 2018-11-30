using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LeagueInformer.Api;
using LeagueInformer.Interfaces;
using LeagueInformer.Models;
using Newtonsoft.Json.Linq;

namespace LeagueInformer.Services
{
    class GetChallengersService //: IGetChallengers
    {
        private readonly ApiClient _apiClient = new ApiClient();

       // public async Task<List<Challengers>> GetListOfChallengers() //Zbieramy tylko dla EUNE, wiec nie pobieramy serwera poki co
      //  {
            //try
            //{
            //    JObject response = JObject.Parse(await _apiClient.GetJsonFromUrl(
            //        $"https://eun1.api.riotgames.com/lol/league/v4/challengerleagues/by-queue/RANKED_SOLO_5x5?api_key={AppSettings.AuthorizationApiKey}"));

            //    //return response == null ? new Challengers { IsSuccess = false } :
            //    //    new Challengers
            //    //    {
            //    //        IsSuccess = true,
            //    //        SummonerName = (string)response.GetValue("name"),
            //    //        Points = (int)response.GetValue("leaguePoints"),
            //    //        Wins = (int)response.GetValue("wins"),
            //    //        Losses = (int)response.GetValue("losses"),
            //    //        HotStrike = (bool)response.GetValue("hotStreak"),
            //    //        Veteran = (bool)response.GetValue("veteran")
            //    //    };
            //}
            //catch (Exception ex)
            //{
            //    return new Challengers
            //    {
            //        IsSuccess = false
            //    };
            //}
       // }
    }
}
