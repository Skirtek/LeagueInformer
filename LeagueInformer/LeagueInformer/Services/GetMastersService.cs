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
    public class GetMastersService : IGetMasters
    {
        private readonly ApiClient _apiClient = new ApiClient();

        public async Task<MastersList> GetListOfMasterLeague() //Zbieramy tylko dla EUNE, wiec nie pobieramy serwera poki co
        {
            try
            {
                List<Masters> mastersList = new List<Masters>();
                JObject response = JObject.Parse(await _apiClient.GetJsonFromUrl(
                    $"https://eun1.api.riotgames.com/lol/league/v4/masterleagues/by-queue/RANKED_SOLO_5x5?api_key={AppSettings.AuthorizationApiKey}"));

                if (response == null || !(response["entries"] is JArray mastersArray))
                {
                    return new MastersList
                    {
                        IsSuccess = false,
                        Message = _apiClient.MapErrorToString(ErrorEnum.DownloadingError)
                    };
                }
                    
                foreach (var master in mastersArray)
                {
                    mastersList.Add(master.ToObject<Masters>());
                }

                return new MastersList
                {
                    IsSuccess = true,
                    MastersResponseList = mastersList
                };
            }
            catch (Exception ex)
            {
                return new MastersList
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }
    }
}