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
            Console.WriteLine("1. Open a quiz from .txt file.");
        }
        private static void WriteBackCommunicate(string message, string communicateType)
        {    
            WriteCommunicate(message, communicateType);
            Console.Write("> Click enter to back");
            Console.ReadLine();
            Console.Clear();
        }
        private static void WriteCommunicate(string message, string communicateType)
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
        public static void WritePoints(QuizLoadFromFile quiz)
        {
            WriteBackCommunicate($"Your points: {quiz.Points}/{quiz.QuestionsAndAnswers.Count}","SCORE");
        }
        public static void WriteError(string message = "Bad error here")
        {
            WriteBackCommunicate(message, "ERROR");
        }

        public static void WriteAskOverwrite()
        {
            string message = "A file exist on this path. Do you want to overwrite?";
            WriteCommunicate(message, "IMPORTANT");
            Console.WriteLine("1. Yes | 2. No");
        }
        public static void WriteEnterAsk()
        {
            Console.Write("Enter path of quiz: ");
        }
    }
}
