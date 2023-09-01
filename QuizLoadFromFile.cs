using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace QuizConsoleApp
{
    public class QuizLoadFromFile : Quiz
    {
        public int Points { get; private set; }
        public List<QuestionAndAnswer> QuestionsAndAnswers { get; private set; }
        public QuizLoadFromFile(string path) :
            base (path)
        {
            if (!File.Exists(path))
                throw new InvalidOperationException("File doesn't exist.");

            _content = File.ReadAllLines(_path);
            QuestionsAndAnswers = new List<QuestionAndAnswer>();
        }

        public void LoadQuestion()
        {

            string invalidMessage = "Not found any question or bad pattern file.";
            string endPattern = @"^.+;$";
            string questionPattern = string.Format("^\\[QUESTION\\]");
            int questionNumber = 1;
            bool isFileNull = true;

            for (int line = 0; line < _content.Length; line++)
            {

                if (Regex.IsMatch(_content[line], questionPattern))
                {
                    QuestionAndAnswer questionAnswer = new();
                    questionAnswer.Question.Append(string.Format("[Question {0}]", questionNumber));
                    line++;
                    while (line < _content.Length)
                    {
                        questionAnswer.Question.Append("\n");
                        questionAnswer.Question.Append(_content[line]);


                        if (Regex.IsMatch(_content[line], endPattern))
                        {
                            isFileNull = false;
                            questionAnswer.Answer = _content[line + 1][0];
                            QuestionsAndAnswers.Add(questionAnswer);
                            questionNumber++;
                            break;
                        }
                        line++;
                        //questionAnswer.Question.Clear();
                    }
                }
            }
            if (isFileNull)
            {
                QuestionAndAnswer questionAnswer = new();
                questionAnswer.Question.Append(invalidMessage);
                QuestionsAndAnswers.Add(questionAnswer);
            }

        }
        
        public bool CheckIfCharCorrect(char answer)
        {
            answer = Char.ToLower(answer);
            if (answer.Equals('a') || answer.Equals('b') || answer.Equals('c') || answer.Equals('d'))
            {
                return true;
            }
            return false;
        }

        public void CheckAnswer(char answer, char correctAnswer)
        {
            answer = Char.ToLower(answer);
            if (answer.Equals(correctAnswer))
            {
                Points++;
            }
        }
    }
}
