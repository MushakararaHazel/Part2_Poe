using System;
using System.Threading;

namespace CybersecurityChatbot
{
    public class UserInterface
    {
        // Color scheme for consistent UI
        private const ConsoleColor TitleColor = ConsoleColor.Cyan;
        private const ConsoleColor BotColor = ConsoleColor.Green;
        private const ConsoleColor UserColor = ConsoleColor.Yellow;
        private const ConsoleColor WarningColor = ConsoleColor.Red;
        private const ConsoleColor InfoColor = ConsoleColor.Blue;
        private const ConsoleColor TaskColor = ConsoleColor.Magenta;
        private const ConsoleColor PromptColor = ConsoleColor.DarkYellow;

        public void DisplayHeader()
        {
            Console.ForegroundColor = TitleColor;
            Console.WriteLine(@"  ____      _                                        _ _         
 / ___|   _| |__   ___ _ __ ___  ___  ___ _   _ _ __(_) |_ _   _ 
| |  | | | | '_ \ / _ \ '__/ __|/ _ \/ __| | | | '__| | __| | | |
| |__| |_| | |_) |  __/ |  \__ \  __/ (__| |_| | |  | | |_| |_| |
 \____\__, |_.__/ \___|_|  |___/\___|\___|\__,_|_|  |_|\__|\__, |
  ____|___/        _   _           _                       |___/ 
 / ___| |__   __ _| |_| |__   ___ | |_                           
| |   | '_ \ / _` | __| '_ \ / _ \| __|                          
| |___| | | | (_| | |_| |_) | (_) | |_                           
 \____|_| |_|\__,_|\__|_.__/ \___/ \__|                             ");

            Console.ResetColor();
            Console.WriteLine(new string('=', Console.WindowWidth - 1));

        }

        public string GetUserName()
        {
            TypingEffect("Hello! Before we begin, may I have your name?", BotColor);

            string name;
            while (true)
            {
                Console.ForegroundColor = UserColor;
                Console.Write(">> Your name: ");
                Console.ResetColor();
                name = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(name))
                    return name.Trim();

                Console.ForegroundColor = WarningColor;
                Console.WriteLine("Please enter a valid name.");
                Console.ResetColor();
            }
        }

        public void TypingEffect(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(20); // Adjust typing speed (milliseconds)
            }
            Console.WriteLine();
            Console.ResetColor();
        }

        public void DisplayFarewell(string userName)
        {
            Console.WriteLine();
            TypingEffect($"Thank you for using the Cybersecurity Assistant, {userName}!", BotColor);
            TypingEffect("Remember: Staying safe online is an ongoing process. Stay vigilant!", BotColor);
        }

        public void ShowTaskListHeader()
        {
            Console.ForegroundColor = TaskColor;
            Console.WriteLine("\nYour Cybersecurity Tasks:");
            Console.WriteLine(new string('-', 30));
            Console.ResetColor();
        }

        

        public void ShowCommandPrompt()
        {
            Console.ForegroundColor = PromptColor;
            Console.Write("\n>> ");
            Console.ResetColor();
        }

        public void ShowHelpInformation()
        {
            Console.ForegroundColor = InfoColor;
            Console.WriteLine("\nAvailable Commands:");
            Console.WriteLine(new string('-', 30));

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("General Questions:");
            Console.WriteLine("- Ask about: passwords, phishing, scams, privacy");
            Console.WriteLine("- Example: \"How do I create strong passwords?\"");

           

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\nOther Commands:");
            Console.WriteLine("- help (show this information)");
            Console.WriteLine("- exit (end conversation)");

            Console.ResetColor();
        }


        public void ShowInputError(string message)
        {
            Console.ForegroundColor = WarningColor;
            Console.WriteLine(message);
            Console.ResetColor();
        }
        public void ShowSuccessMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"✓ {message}");
            Console.ResetColor();
        }


    }
}