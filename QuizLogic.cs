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

            for (int questionTag = 0 + _nextLinesQuestionNumber; questionTag < _content.Length; questionTag++)
            {
                if (Regex.IsMatch(_content[questionTag], questionPattern))
                {
                    int semicolonTag = questionTag;
                    while (semicolonTag < _content.Length)
                    {
                        question.Append(_content[semicolonTag]);
                        question.Append("\n");
                      
                        if (Regex.IsMatch(_content[semicolonTag], endPattern))
                        {
                            _nextLinesQuestionNumber += semicolonTag + 1;
                            return question;
                        }
                        else if (Regex.IsMatch(_content[semicolonTag + 1], "\\[QUESTION [0-99]\\]") && Regex.IsMatch(_content[questionTag], questionPattern))
                        {
                            question.Clear();
                            question.Append("ERROR: Check if your file have \";\"  ");
                            return question;
                        }
                        semicolonTag++;
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
