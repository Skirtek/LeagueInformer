using System;
using System.Linq;
using System.Threading.Tasks;
using LeagueInformer.Interfaces;
using LeagueInformer.Models;
using LeagueInformer.Resources;
using LeagueInformer.Services;
using LeagueInformer.Utils.Interfaces;

namespace LeagueInformer.Utils
{
    public class PrintMethods : IPrintMethods
    {
        private readonly IFileHandler _fileHandler;

        #region CTOR

        public PrintMethods()
        {
            _fileHandler = new FileHandler();
        }
        #endregion
        public ChosenServer PrintListOfSpectateServers()
        {
            Console.WriteLine(AppResources.GetServerStatus_ChooseServerFromList, Environment.NewLine, Environment.NewLine);
            int position = 1;
            foreach (var server in AppSettings.ServerSpectateAddresses)
            {
                Console.WriteLine(AppResources.Common_TwoVerbatimStringWithDot,
                    position,
                    server.ServerName);
                position++;
            }

            bool getPosition = int.TryParse(Console.ReadLine(), out int pos);
            if (!getPosition || !CheckIfPositionIsInBounds(pos))
            {
                Console.WriteLine(AppResources.GetServerStatus_ParsingFailed);
                return new ChosenServer
                {
                    IsSuccess = false
                };
            }

            return new ChosenServer
            {
                RegionCode = AppSettings.ServerSpectateAddresses.ElementAt(pos - 1).ServerCode,
                Position = pos,
                IsSuccess = true
            };
        }

        public async Task<string> PrintListOfSavedNicknames()
        {
            try
            {
                int position = 1;

                var nicknamesList = _fileHandler.GetListOfLastNicknames();
                if (nicknamesList.Any())
                {
                    Console.WriteLine(AppResources.PrintListOfSavedNicknames_Instruction, Environment.NewLine);

                    foreach (var nickname in nicknamesList)
                    {
                        Console.WriteLine(AppResources.Common_TwoVerbatimStringWithDot, position, nickname);
                        position++;
                    }

                    Console.WriteLine(AppResources.PrintListOfSavedNicknames_Information, Environment.NewLine);
                }

                else
                {
                    Console.Write(AppResources.GetLeagueOfSummoner_EnterName);
                }

                string summonerName = Console.ReadLine();

                if (int.TryParse(summonerName, out int result))
                {
                    summonerName = nicknamesList[result - 1];
                }
                else
                {
                    await _fileHandler.SaveNicknameToList(summonerName);
                }

                return summonerName;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        private bool CheckIfPositionIsInBounds(int position) =>
            position <= AppSettings.ServerSpectateAddresses.Count && position > 0;
    }
}