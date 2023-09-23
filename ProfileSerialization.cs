using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;


namespace QuizConsoleApp
{
    
    public static class ProfileSerialization
    {
        private static string _fileLocation = Path.Combine(Directory.GetCurrentDirectory(), "Leaderboard.json");
        public static void SerializeProfile(List<UserProfile> profile)
        {
            JsonSerializerOptions options = _getSerializeOptions();
            using (Stream json = File.Create(_fileLocation))
            {
                JsonSerializer.Serialize(utf8Json: json, value: profile, options);
            }
        }
        private static JsonSerializerOptions _getSerializeOptions()
        {
            return new JsonSerializerOptions()
            {
                //IncludeFields = true,
                //PropertyNameCaseInsensitive = true,
                //WriteIndented = true,
                //PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
        }
        public static void DeleteSerializationFile()
        {
            File.Delete(_fileLocation);
        }
    }
}
