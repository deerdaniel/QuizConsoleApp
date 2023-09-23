using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;

namespace QuizConsoleApp
{
    public class Menu
    {
        public void Options(char option, List<UserProfile> profiles)
        {
            string? path;
            Console.Clear();
            switch (option)
            {
                case '0':
                    var quizFromList = new QuizLoadFromFile();
                    string userChoice;
                    int userChoiceInt;
                    bool isInt;
                    UserInterface.WriteQuizFolderList(quizFromList.Files);
                    userChoice = Console.ReadLine();

                    isInt = Int32.TryParse(userChoice, out userChoiceInt);
                    if (!isInt || userChoiceInt > quizFromList.Files.Length)
                    {
                        Console.WriteLine("Bad number");
                        break;
                    }
                    string binPath = QuizWriteInFile.BinPath;

                    var quizLoadingFromList = new QuizLoadFromFile(@"" + quizFromList.Files[userChoiceInt - 1], profiles);
                    quizLoadingFromList.StartQuiz();
                    Console.Clear();
                    break;
                case '1':
                    try
                    {
                        UserInterface.WriteEnterAsk();
                        path = Console.ReadLine();
                        var quizLoading = new QuizLoadFromFile(@"" + path, profiles);
                        quizLoading.StartQuiz();                      
                    }
                    catch
                    {
                        UserInterface.WriteError("Invalid path! Enter correct path!");
                    }
                    Console.Clear();
                    break;
                case '2':
                        _writeQuiz();                   
                    Console.Clear();
                    break;
                case '3':
                    UserInterface.WriteLeaderboard(profiles);
                    Console.Clear();
                    break;
                case '4':
                    UserInterface.WriteDeleteCommunicate();
                    string input = Console.ReadLine();
                    if (input.ToLower() == "yes")
                    {
                        ProfileSerialization.DeleteSerializationFile();
                    }                 
                    Console.Clear();
                    break;
                case '5':
                    Environment.Exit(0);
                    break;
            }
        }
        private static void _writeQuiz()
        {
            string fileName;
            UserInterface.WriteAskFileName();
            fileName = Console.ReadLine();
            var quizWriting = new QuizWriteInFile(fileName);
            Console.Clear();

            if (File.Exists(quizWriting.FilePath))
            {
                char userOption = '0';
                UserInterface.WriteAskOverwrite();
                while (!(userOption.Equals('1') || userOption.Equals('2')))
                {
                    userOption = Console.ReadKey().KeyChar;
                }
                bool userOptionConvertToBool = userOption.Equals('1') ? true : false;

                if(userOptionConvertToBool)
                {
                    quizWriting.CreateFile();
                }
                
            }
            else
            {
                quizWriting.CreateFile();
            }

            bool isFinished = false;
            string questionInput;
            string[] answers = { "a. ", "b. ", "c. ", "d. " };
            string answerInput;
            string correctAnswer;
            string finishText;
            do
            {
                Console.WriteLine($"Enter question number {quizWriting.QuestionNumber}");
                questionInput = Console.ReadLine();

                quizWriting.WriteQuestion(questionInput);

                for (int i = 0; i < 4; i++)
                {
                    //Console.WriteLine();
                    Console.Write($"{answers[i]}");
                    answerInput = answers[i] + Console.ReadLine();

                    quizWriting.WriteAnswer(answerInput);
                }
                

                bool isCorrectAnswer = false;
                do
                {
                    try
                    {
                        Console.Write("Enter correct answer: ");
                        correctAnswer = Console.ReadLine();
                        quizWriting.WriteCorrectAnswer(correctAnswer[0]);
                        isCorrectAnswer = true;
                    }
                    catch (ArgumentException)
                    {
                        Console.WriteLine("Wrong character, enter again. Ex. a, b, c or d.");
                    }
                } while(!isCorrectAnswer);

                Console.WriteLine("Do you want finish? Enter \"yes\", or just click enter to go next");
                finishText = Console.ReadLine();
                finishText = finishText.ToLower();
                if (finishText.Equals("yes"))
                {
                    isFinished = true;
                }
                Console.Clear();

            } while (!isFinished);
        }
    }
}
