using System;
using System.IO;
using LeagueInformer.Interfaces;
using LeagueInformer.Models;
using LeagueInformer.Resources;
using Microsoft.Win32;

namespace LeagueInformer.Services
{
    public class Spectator : ISpectator
    {
        public bool OpenSpectateClient(
            string encryptionKey,
            SummonerGameDetails details,
            string regionCode,
            string serverAddress,
            string port)
        {
            try
            {
                string keyPath = string.Empty;

                foreach (var path in AppSettings.LocalMachineRegisterKeysPath)
                {
                    if (CheckIfLocalRegistryKeyExists(path))
                    {
                        keyPath = path;
                        break;
                    }
                }

                if (string.IsNullOrEmpty(keyPath))
                {
                    foreach (var path in AppSettings.CurrentUserRegisterKeysPath)
                    {
                        if (CheckIfUserRegistryKeyExists(path))
                        {
                            keyPath = path;
                            break;
                        }
                    }
                }

                if (string.IsNullOrEmpty(keyPath))
                {
                    Console.WriteLine(AppResources.OpenSpectateClient_CannotFindRegistryKey);
                    return false;
                }

                using (var regKey = Registry.LocalMachine.OpenSubKey(keyPath))
                {
                    if (regKey == null)
                    {
                        Console.WriteLine(AppResources.OpenSpectateClient_CannotFindRegistryKey);
                        return false;
                    }

                    var installationPath = regKey.GetValue("Location");
                    string combinedPath = $"{installationPath}\\RADS\\solutions\\lol_game_client_sln\\releases";
                    DirectoryInfo getDirectoryInfo = new DirectoryInfo(combinedPath);

                    if (!getDirectoryInfo.Exists)
                    {
                        Console.WriteLine(AppResources.OpenSpectateClient_CannotFindClientDirectory);
                        return false;
                    }

                    var directoriesList = getDirectoryInfo.GetDirectories();

                    if (directoriesList.Length == 0)
                    {
                        Console.WriteLine(AppResources.OpenSpectateClient_CannotFindClientDirectory);
                        return false;
                    }

                    if (string.IsNullOrEmpty(encryptionKey) || string.IsNullOrEmpty(details.GameId))
                    {
                        Console.WriteLine(AppResources.Error_Undefined);
                        return false;
                    }

                    string leagueClientPath = $"{directoriesList[0].FullName}\\deploy";
                    string parameters =
                        $"\"spectator spectator.{serverAddress}.lol.riotgames.com:{port} {encryptionKey} {details.GameId} {regionCode}\"";

                    if (!CreateBatchFile(leagueClientPath, parameters))
                    {
                        Console.WriteLine(AppResources.OpenSpectateClient_CannotCreateBatchFile);
                        return false;
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private bool CreateBatchFile(string path, string parameters)
        {
            try
            {
                using (StreamWriter batchFileWriter = new StreamWriter(AppSettings.PathToSaveBatchFile))
                {
                    batchFileWriter.Write(AppSettings.BatchFileSkeleton, path, Environment.NewLine, parameters);
                    batchFileWriter.Close();
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private bool CheckIfLocalRegistryKeyExists(string keyName) => Registry.LocalMachine.OpenSubKey(keyName) != null;

        private bool CheckIfUserRegistryKeyExists(string keyName) => Registry.CurrentUser.OpenSubKey(keyName) != null;
    }
}