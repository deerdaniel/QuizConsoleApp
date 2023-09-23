using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizConsoleApp
{
    public class App
    {
        public void Start()
        {
            while (true)
            {
                ConsoleKeyInfo option;
                UserInterface.WriteOptions();
                var menu = new Menu();
                List<UserProfile> profiles = ProfileDeserialization.TryDeserializeProfileFile();
                               
                option = Console.ReadKey(true);

                menu.Options(option.KeyChar, profiles);
            }
        }

        
    }
}
