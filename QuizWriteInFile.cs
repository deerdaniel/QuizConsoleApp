using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;

namespace QuizConsoleApp
{
    public class QuizWriteInFile
    {
        public static readonly string BinPath = Directory.GetCurrentDirectory() + @"\Quiz";
        public readonly string FilePath;
        public int QuestionNumber { get; private set; }
        public QuizWriteInFile(string nameFile)
        {
            QuestionNumber = 1;
            FilePath = System.IO.Path.Combine(BinPath, nameFile + ".txt");
        }
        /// <summary>
        /// This method create file and return true when param is true; Return false when param is false 
        /// </summary>
        public void CreateFile()
        {
            string finalPath = FilePath;
            File.Create(finalPath).Dispose();
        }

        public void WriteQuestion(string question)
        {
            string writeIn = $"[QUESTION]\n" + question;
            ExistsFile();
            _writeInFile(writeIn);
            QuestionNumber++;
            
        }
        public void WriteAnswer(string answer)
        {
            ExistsFile();
            if (answer[0].Equals('d'))
            {
                answer += ";";
            }
            _writeInFile(answer);
        }
        public void WriteCorrectAnswer(char correctAnswer)
        {
            if (!UserProfile.CheckCharCorrection(correctAnswer))
            {
                throw new ArgumentException("Only correct char is: a, b, c or d.", correctAnswer.ToString());
            }
            ExistsFile();
            _writeInFile(correctAnswer.ToString());
        }
        private void _writeInFile(string message)
        {
            using (var writer = new StreamWriter(FilePath, true))
            {
                writer.WriteLine(message);
            }
        }
        private void ExistsFile()
        {
            if (!File.Exists(FilePath))
            {
                throw new InvalidOperationException("A file wasn't created");
            }
        }

    }
}
