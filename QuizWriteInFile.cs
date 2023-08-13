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
        public QuizWriteInFile(string path) :
            base(path)
        {
            
        }
        /// <summary>
        /// This method create file and return true when param is true; Return false when param is false 
        /// </summary>
        public bool CreateFile(bool condition)
        {
            if (condition)
            {
                CreateFile();
                return true;
            }
            else
            {
                return false;
            }
        }
        public void CreateFile()
        {
            File.Create(_path);
        }
    }
}
