using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace QuizConsoleApp
{
    public class QuizLogic
    {
        private readonly string _path;
        private string[] _content;
        private int _nextLinesQuestionNumber;
        public QuizLogic(string path)
        {
            if (string.IsNullOrWhiteSpace(path)) 
                throw new ArgumentNullException("path");
            if (!File.Exists(path))
                throw new InvalidOperationException("File doesn't exist.");

            _path = path;
            _content = File.ReadAllLines(_path);
        }

        public StringBuilder GetQuestionFromFile(int number = 1)
        {
            StringBuilder question = new();
            string notFoundReturn = "Not found";
            string endPattern = @"^.+;$";
            string questionPattern = string.Format("\\[QUESTION {0}\\]", number);

            for (int i = 0 + _nextLinesQuestionNumber; i < _content.Length; i++)
            {
                if (Regex.IsMatch(_content[i], questionPattern))
                {
                    while (i < _content.Length)
                    {
                        question.Append(_content[i]);
                        question.Append("\n");
                        if (Regex.IsMatch(_content[i], endPattern))
                        {
                            _nextLinesQuestionNumber += i + 1;
                            return question;
                        }
                        i++;
                    }
                    //when ";" not found in file then clear StringBuilder and return not found question 
                    question.Clear();
                }
            }

            question.Append(notFoundReturn);
            return question;
        }

        private void GetAnswersFromFile()
        {
            string pattern;
        }
    }
}
