using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using LeagueInformer.Interfaces;

namespace LeagueInformer.Services
{
    public class FileHandler : IFileHandler
    {
        public List<string> GetListOfLastNicknames()
        {
            var nicknamesList = new List<string>();
            try
            {
                string path = AppSettings.PathToSaveNicknameFile;
                if (!File.Exists(path))
                {
                    return nicknamesList;
                }

                using (StreamReader reader = File.OpenText(path))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        nicknamesList.Add(line);
                    }
                }

                return nicknamesList;
            }
            catch (Exception)
            {
                return nicknamesList;
            }
        }

        public async Task<bool> SaveNicknameToList(string nickname)
        {
            try
            {
                if (!CheckIfAppDirectoryExists(AppSettings.ApplicationDataPath))
                {
                    if (!CreateAppDirectory(AppSettings.ApplicationDataPath))
                    {
                        return false;
                    }
                }

                string path = AppSettings.PathToSaveNicknameFile;
                if (!File.Exists(path))
                {
                    using (StreamWriter writer = File.CreateText(path))
                    {
                        await writer.WriteAsync(nickname);
                        writer.Close();
                        return true;
                    }
                }

                if (GetListOfLastNicknames().Contains(nickname))
                {
                    return true;
                }

                var nicknameList = GetListOfLastNicknames();
                if (nicknameList.Count > 9)
                {
                    nicknameList.RemoveAt(0);
                    nicknameList.Add(nickname);
                    File.Delete(path);
                    bool first = false;

                    using (StreamWriter writer = File.CreateText(path))
                    {
                        foreach (var nick in nicknameList)
                        {
                            if (!first)
                            {
                                await writer.WriteAsync(nick);
                                first = true;
                            }
                            else
                            {
                                await writer.WriteAsync(Environment.NewLine + nick);
                            }
                        }
                        writer.Close();
                        return true;
                    }
                }

                File.AppendAllText(path,
                    Environment.NewLine + nickname);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool CheckIfAppDirectoryExists(string path) => Directory.Exists(path);

        public bool CreateAppDirectory(string path) => Directory.CreateDirectory(path).Exists;
    }
}