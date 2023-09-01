using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;

namespace QuizConsoleApp
{
    public class QuizWriteInFile : Quiz
    {
        private bool _isFileCreated;
        public QuizWriteInFile(string path) :
            base(path)
        {
            
        }
        /// <summary>
        /// This method create file and return true when param is true; Return false when param is false 
        /// </summary>
        public bool CreateFile(bool condition, string fileName = "Quiz")
        {
            if (condition)
            {
                CreateFile(fileName);
                return true;
            }
            else
            {
                return false;
            }
        }
        public void CreateFile(string fileName = "Quiz")
        {
            if (_isFileCreated)
            {
                throw new InvalidOperationException("A file was created");
            }
            string finalPath = _path + $"{fileName}.txt";
            File.Create(finalPath);
            _isFileCreated = true;
        }

        public void WriteQuestion(string question)
        {
            if (!_isFileCreated)
            {
                throw new InvalidOperationException("A file wasn't created");
            }
            using (var writer = new StreamWriter(_path))
            {
                writer.WriteLine();
            }
            
        }
    }
}
