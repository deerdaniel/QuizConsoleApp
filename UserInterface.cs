using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizConsoleApp
{
    public static class UserInterface
    {
        public static void WriteOptions()
        {
            Console.WriteLine("0. Check quiz folder");
            Console.WriteLine("1. Open a quiz from .txt file.");
            Console.WriteLine("2. Create a new quiz");
            Console.WriteLine("3. Leaderboard");
            Console.WriteLine("4. Reset stats");
            Console.WriteLine("5. Exit");
        }
        public static void WriteQuizFolderList(FileInfo[] files)
        {
            Console.WriteLine("Choose quiz:");
            for (int i = 0;  i < files.Length; i++)
            {               
                Console.WriteLine($"{i + 1} {files[i].Name}");
            }
        }
        private static void _writeBackCommunicate(string message, string communicateType)
        {    
            _writeCommunicate(message, communicateType);
            Console.Write("> Click enter to back");
            Console.ReadLine();
            Console.Clear();
        }
        private static void _writeCommunicate(string message, string communicateType)
        {
            Console.Clear();
            Console.WriteLine("===={0}====", communicateType);
            Console.WriteLine(message);
            Console.WriteLine("=============\n");
        }
        public static void WriteWrongCharacter()
        {
            Console.WriteLine("You choosed a character other than a, b, c, d");
        }
        public static void WriteCurrentPoints(UserProfile user)
        {
            var stateMessage = (user.IsCorrectAnswer ? "Correct!" : "Wrong!") + "\nPoints for " ;
            _writePoints(user,  stateMessage);
        }
        public static void WriteFinalPoints(UserProfile user)
        {
            var finalMessage = "Final score for ";
            _writePoints(user, finalMessage);
        }
        private static void _writePoints(UserProfile user, string extendMessage)
        {
            var name = user.Name;
            var point = user.Points;
            var allPossiblePoints = user.AllPossiblePoints;
            var pointMessage = extendMessage + $"{name}: {point}/{allPossiblePoints}";

            _writeBackCommunicate(pointMessage,"SCORE");
        }   
        public static void WriteError(string message = "Bad error here")
        {
            _writeBackCommunicate(message, "ERROR");
        }
        public static void WriteAskOverwrite()
        {
            string message = "A file exist on this path. Do you want to overwrite?";
            _writeCommunicate(message, "IMPORTANT");
            Console.WriteLine("1. Yes | 2. No");
        }
        public static void WriteEnterAsk()
        {
            Console.Write("Enter path of quiz: ");
        }
        public static void WriteAskFileName()
        {
            Console.Write("It will be save in: QuizConsoleApp\\bin\\Debug\\net6.0\\Quiz\nEnter file name:");
        }
        public static void WriteAskNameProfile()
        {
            Console.WriteLine("Enter your name:");
        }
        public static void WriteLeaderboard(List<UserProfile> profiles)
        {
            Console.Clear();
            Console.WriteLine("List of user:");
            profiles.Sort(
                delegate (UserProfile user1, UserProfile user2)
                {
                    return user2.Points.CompareTo(user1.Points);
                }
                );

            for (int i = 0; i < profiles.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {profiles[i]}");
            }

            Console.WriteLine("\n>Enter to back");
            Console.ReadLine();
            Console.Clear();
        }
        public static void WriteDeleteCommunicate()
        {
            string message = "Are you sure, you want to delete all stats?";
            _writeCommunicate(message, "WARNING");
            Console.WriteLine("Write \"yes\" to confirm, or just click enter to back");
        }
    }
}
