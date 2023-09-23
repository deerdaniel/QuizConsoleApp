using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace QuizConsoleApp
{
    public static class ProfileDeserialization
    {
        private static string _path = Path.Combine(Directory.GetCurrentDirectory(), "Leaderboard.json");
        /// <summary>
        /// It trys to desarialize. Method return list of UserProfile from json file. If file doesn't exist then method return an empty list of UserProfiles
        /// </summary>
        /// <returns></returns>
        public static List<UserProfile> TryDeserializeProfileFile()
        {
            try
            {
                return _getDeserializeProfiles();
            }
            catch
            {
                return new List<UserProfile>();
            }
        }
        private static List<UserProfile> _getDeserializeProfiles()
        {
            if (!File.Exists(_path))
            {
                throw new FileNotFoundException();
            }
            string jsonContent = File.ReadAllText(_path);
            List<UserProfile> usersProfile = JsonSerializer.Deserialize<List<UserProfile>>(jsonContent)!;
            return usersProfile;
        }
    }
}
