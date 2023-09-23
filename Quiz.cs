using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace QuizConsoleApp
{
    public class Quiz
    {
        public readonly string _path;
        protected string[] _content;
        public Quiz(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentNullException("path");
            _path = path;
        }
        public Quiz() { }  
    }
}
