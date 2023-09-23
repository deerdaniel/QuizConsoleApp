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
        public List<QuestionAndAnswer> QuestionAndAnswers { get; private set; }
        private readonly List<UserProfile> UserProfiles;
        private int questionNumber;
        public FileInfo[] Files { get; private set; }
        public QuizLoadFromFile(string path, List<UserProfile> profiles) :
            base (path)
        {
            if (!File.Exists(path))
                throw new InvalidOperationException("File doesn't exist.");

            _content = File.ReadAllLines(_path);
            QuestionAndAnswers = new List<QuestionAndAnswer>();
            UserProfiles = profiles;        
        }
        public QuizLoadFromFile()
        {
            string binPath = QuizWriteInFile.BinPath;
            var directory = new DirectoryInfo(binPath);
            Files = directory.GetFiles("*.txt");
        }
        public void StartQuiz()
        {
            try
            {               
                Console.Clear();
                UserInterface.WriteAskNameProfile();
                string fileName = Path.GetFileName(_path);

                var name = Console.ReadLine();
                var user = new UserProfile(name);

                GetQuestionsAndAnswer();
                _showQuiz(user);

            }
            catch (ArgumentNullException)
            {
                UserInterface.WriteError("Empty path! Enter correct path");
            }
            catch (InvalidOperationException)
            {
                UserInterface.WriteError("File doesn't exist or file doesn't have the correct pattern.\nEnter correct path or make you sure that file has the correct pattern");
            }
            catch (IndexOutOfRangeException)
            {
                UserInterface.WriteError("File doesn't have correct template. Quiz probably doesn't inlcude a correct answer.");
            }
        }
        private void _showQuiz(UserProfile user)
        {
            _setAllPossiblePointsToUserProfile(user);
            for (int i = 0; i < QuestionAndAnswers.Count; i++)
            {
                _showQuestion(QuestionAndAnswers[i]);
                _setAnswerToUserProfile(user, QuestionAndAnswers[i]);
                user.CheckAnswer();
                UserInterface.WriteCurrentPoints(user);
            }
            UserInterface.WriteFinalPoints(user);
            _addUserProfileToList(user);
            _serializeProfiles();
        }
        private void _showQuestion(QuestionAndAnswer qan)
        {
            Console.WriteLine(qan.Content);
        }
        public void GetQuestionsAndAnswer()
        {            
            bool isFileNull = true;

            for (int line = 0; line < _content.Length; line++)
            {
                if (_checkQuestionPattern(_content[line]))
                {
                    line++; //if we found this tag, then skip a line to not write the tag
                    QuestionAndAnswer questionAndAnswer = new();
                    _addQuestionTagToStringBuilder(questionAndAnswer);

                    while (line < _content.Length)
                    {
                        _addQuestionAndAnswerToStringBuilder(questionAndAnswer, _content[line]);
                        if (_checkEndPattern(_content[line]))
                        {
                            line++; //next line to correct answer
                            if (line >= _content.Length)
                            {
                                throw new IndexOutOfRangeException();
                            }
                            char correctAnswer = _getCorrectAnswer(_content[line]);
                            _addCorrectAnswer(questionAndAnswer, correctAnswer);
                            QuestionAndAnswers.Add(questionAndAnswer);

                            isFileNull = false;
                            questionNumber++;
                            break;
                        }
                        line++;
                    }
                }
            }
            if (isFileNull)
            {
                throw new InvalidOperationException("Not found any question or bad pattern file.");
            }
            
        }

        private bool _checkQuestionPattern(string word)
        {
            string pattern = string.Format("^\\[QUESTION\\]");
            return _checkPattern(word, pattern);
        }
        private bool _checkEndPattern(string word)
        {
            string pattern = @"^.+;$";
            return _checkPattern(word, pattern);
        }
        private bool _checkPattern(string word, string pattern)
        {
            if (Regex.IsMatch(word, pattern)) return true;
            return false;
        }
        private void _addQuestionTagToStringBuilder(QuestionAndAnswer qan)
        {
            qan.Content.Append(string.Format("[Question {0}]", questionNumber + 1));
        }
        private void _addQuestionAndAnswerToStringBuilder(QuestionAndAnswer qan, string content)
        {
            qan.Content.Append("\n")
                       .Append(content);
        }
        private char _getCorrectAnswer(string content)
        {
            return content[0];
        }
        private void _addCorrectAnswer(QuestionAndAnswer qan, char correctAnswer)
        {
            qan.CorrectAnswer = correctAnswer;
        }
        private void _setInvalidMessage(QuestionAndAnswer qan)
        {
            string invalidMessage = "Not found any question or bad pattern file.";
            qan.Content.Append(invalidMessage);
        }    
        private void _setAllPossiblePointsToUserProfile(UserProfile user)
        {
            user.AllPossiblePoints = QuestionAndAnswers.Count;
        }
        private void _setAnswerToUserProfile(UserProfile user, QuestionAndAnswer qan)
        {
            do
            {
                user.UserAnswer = _getAnswer();
            } while (!user.CheckCharCorrection());
            
            user.CorrectAnswer = qan.CorrectAnswer;
        }
        private static char _getAnswer()
        {
            return Console.ReadKey(true).KeyChar;
        }
        private void _addUserProfileToList(UserProfile userProfile)
        { 
            UserProfiles.Add(userProfile);
        }
        private void _serializeProfiles()
        {
            ProfileSerialization.SerializeProfile(UserProfiles);
        }
    }
}
