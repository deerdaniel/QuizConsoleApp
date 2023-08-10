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
                    Console.Write("Enter path of quiz: ");
                    string path = Console.ReadLine();
                    Console.Clear();
                    try
                    {
                        var quiz = new QuizLoadFromFile(@"" + path);

                        quiz.LoadQuestion();

                        ConsoleKeyInfo answer;
                        char correctAnswer;
                        int i = 0;

                        //Points counter
                        while (i < quiz.QuestionsAndAnswers.Count)
                        {
                            Console.WriteLine(quiz.QuestionsAndAnswers[i].Question);
                            answer = Console.ReadKey(true);
                            correctAnswer = quiz.QuestionsAndAnswers[i].Answer;

                            if (!quiz.CheckIfCharCorrect(answer.KeyChar))
                            {
                                Console.Clear();
                                UserInterface.WriteWrongCharacter();
                                continue;
                            }

                            quiz.CheckAnswer(answer.KeyChar, correctAnswer);
                            Console.Clear();
                            i++;
                        }

                        UserInterface.WritePoints(quiz);
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

                    break;
            }
        }
    }
}
