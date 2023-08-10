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
        //Return false when there is no overwrite. If a file is overwrite, the method asks if user want to overwrite or not.
        public bool IsFileCreated()
        {
            if (!File.Exists(_path))
            {
                ConsoleKeyInfo option;
                UserInterface.WriteAskOverwrite();
                option = Console.ReadKey(true);

                switch (option.KeyChar)
                {
                    case '1':
                        CreateFile();
                        break;

                    case '2':
                        
                        break;
                }
            }
            CreateFile();
            return true;
        }
        private void CreateFile()
        {
            File.Create(_path);
        }
    }
}
