using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizConsoleApp
{
    public class QuestionAndAnswer
    {
        public StringBuilder Content { get; set; }
        public char CorrectAnswer { get; set;}

        public QuestionAndAnswer()
        {
            Content = new StringBuilder();
        }
    }
}
