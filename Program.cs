using QuizConsoleApp;
public class Program
{
    private static void Main()
    {
        while (true)
        {
            ConsoleKeyInfo option;
            string path;
            UserInterface.WriteOptions();
            option = Console.ReadKey(true);


            switch (option.KeyChar)
            {
                case '1':
                    
                    try
                    {
                        UserInterface.WriteEnterAsk();
                        path = Console.ReadLine();
                        var quizLoading = new QuizLoadFromFile(@"" + path);
                        Console.Clear();

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
                    UserInterface.WriteEnterAsk();
                    path = Console.ReadLine();
                    var quizWriting = new QuizWriteInFile(@"" + path);
                    Console.Clear();

                    Console.WriteLine("Enter a file name");
                    string fileName = Console.ReadLine();

                    if (File.Exists(quizWriting._path))
                    {
                        char userOption = '0';
                        UserInterface.WriteAskOverwrite();
                        while ( !(userOption.Equals('1') || userOption.Equals('2')) )
                        {
                            userOption = Console.ReadKey().KeyChar;
                        }
                        bool userOptionConvertToBool = userOption.Equals('1') ? true : false;
                        
                        quizWriting.CreateFile(userOptionConvertToBool, fileName);
                    }
                    else
                    {
                        quizWriting.CreateFile(fileName);
                    }
                    break;
            }
        }
    }
}
