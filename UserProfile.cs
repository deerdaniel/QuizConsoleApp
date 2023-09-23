using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace QuizConsoleApp
{
    [Serializable]
    public class UserProfile
    {
        private char _userAnswer;
        public char UserAnswer 
        {
            set { _userAnswer = Char.ToLower(value); }
        }
        private char _correctAnswer;
        public char CorrectAnswer 
        {
            set { _correctAnswer = Char.ToLower(value); } 
        }
        [JsonInclude]
        public int Points { get; private set; }
        [JsonInclude]
        public string Name { get; private set; }
        [JsonIgnore]
        public bool IsCorrectAnswer { get; private set; }
        [JsonIgnore]
        public int AllPossiblePoints { get; set; }
        public UserProfile(string name)
        {
            if (String.IsNullOrEmpty(name)) throw new ArgumentNullException("name");
            Name = name;
        }
        

        public void CheckAnswer()
        {
            if (_userAnswer.Equals(_correctAnswer))
            {
                _addPoint();
                IsCorrectAnswer = true;
            }
            else
            {
                IsCorrectAnswer = false;
            }
        }
        public bool CheckCharCorrection()
        {
            return CheckCharCorrection(_userAnswer);
        }
        public static bool CheckCharCorrection(char answer)
        {
            if (answer.Equals('a') || answer.Equals('b') || answer.Equals('c') || answer.Equals('d'))
            {
                return true;
            }
            return false;
        }
        private void _addPoint()
        {
            Points++;
        }
        public override string ToString()
        {
            return Name + ": " + Points;
        }
    }
}
