using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizConsoleApp
{
    public struct QuestionAndAnswer
    {
        public StringBuilder Question { get; set; }
        public char Answer { get; set;}

        public QuestionAndAnswer()
        {
            Question = new StringBuilder();
            Answer = new char();
        }
    }
}
