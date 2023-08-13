using QuizConsoleApp;
public class Program
{
    private static void Main()
    {
        while (true)
        {
            ConsoleKeyInfo option;

            UserInterface.WriteOptions();
            option = Console.ReadKey(true);


            switch (option.KeyChar)
            {
                case '1':
                    
                    try
                    {
                        var quizLoading = (QuizLoadFromFile)_creatingObject();
                        quizLoading.LoadQuestion();

                        ConsoleKeyInfo answer;
                        char correctAnswer;
                        int i = 0;

                        //Points counter
                        while (i < quizLoading.QuestionsAndAnswers.Count)
                        {
                            Console.WriteLine(quizLoading.QuestionsAndAnswers[i].Question);
                            answer = Console.ReadKey(true);
                            correctAnswer = quizLoading.QuestionsAndAnswers[i].Answer;

                            if (!quizLoading.CheckIfCharCorrect(answer.KeyChar))
                            {
                                Console.Clear();
                                UserInterface.WriteWrongCharacter();
                                continue;
                            }

                            quizLoading.CheckAnswer(answer.KeyChar, correctAnswer);
                            Console.Clear();
                            i++;
                        }

                        UserInterface.WritePoints(quizLoading);
                    }
                    catch(ArgumentNullException)
                    {
                        UserInterface.WriteError("Empty path! Enter correct path");
                    }
                    catch (InvalidOperationException)
                    {
                        UserInterface.WriteError("File doesn't exist. Enter correct path");
                    }
                    break;

                case '2':
                    var quizWriting = (QuizWriteInFile)_creatingObject();

                    if (quizWriting.IsFileExist())
                    {
                        char userOption = '0';
                        UserInterface.WriteAskOverwrite();
                        while ( !(userOption.Equals('1') || userOption.Equals('2')) )
                        {
                            userOption = Console.ReadKey().KeyChar;
                        }
                        bool userOptionConvertToBool = userOption.Equals('1') ? true : false;
                        
                        quizWriting.CreateFile(userOptionConvertToBool);
                    }
                    break;
            }
        }
    }
    private static Quiz _creatingObject()
    {
        UserInterface.WriteEnterAsk();
        string path = Console.ReadLine();
        var quiz = new Quiz(@"" + path);
        Console.Clear();
        return quiz;
    }
}
