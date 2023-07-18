using QuizConsoleApp;
internal class Program
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

                    var quiz = new QuizLogic(@"" + path);
                    Console.WriteLine(quiz.GetQuestionFromFile(1));
                    break;
            }
            Console.ReadLine();
        }
    }
}
