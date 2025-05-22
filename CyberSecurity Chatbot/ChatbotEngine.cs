using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using System;
using System.Globalization;
using System.Linq;
using CyberSecurity_Chatbot;

namespace CybersecurityChatbot
{
    public class ChatbotEngine
    {
        private readonly Audio _audio;
        private readonly UserInterface _ui;
        private readonly Responses _responses;
       
    
        private bool _firstRun = true;

        public ChatbotEngine(Audio audio, UserInterface ui, Responses responses)
        {
            _audio = audio;
            _ui = ui;
            _responses = responses;
            
        }

        public void Run()
        {
            InitializeConsole();
            PlayWelcomeSequence();
            string userName = GetUserName();
         
            MainInteractionLoop(userName);
            ShowFarewell(userName);
        }

        private void InitializeConsole()
        {
            Console.Title = "Cybersecurity Awareness Assistant";
            Console.Clear();
        }

        private void PlayWelcomeSequence()
        {
            if (_firstRun)
            {
                _audio.PlayWelcomeSound();
                _ui.TypingEffect("Initializing Cybersecurity Assistant...", ConsoleColor.Blue);
                Thread.Sleep(1500);
                _firstRun = false;
            }
            _ui.DisplayHeader();
        }

        private string GetUserName()
        {
            string userName;
            do
            {
                userName = _ui.GetUserName();
            } while (string.IsNullOrWhiteSpace(userName));

            return userName;
        }

       

        private void MainInteractionLoop(string userName)
        {
            _ui.TypingEffect($"Hello {userName}! I'm your Cybersecurity Assistant.", ConsoleColor.Cyan);
            _ui.TypingEffect("You can ask me about cybersecurity topics or.", ConsoleColor.Cyan);
            _ui.TypingEffect("Type 'help' for available commands, or 'exit' to quit.", ConsoleColor.Cyan);

            bool continueChat = true;
            while (continueChat)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("\n>> ");
                Console.ResetColor();

                string input = Console.ReadLine()?.Trim();

                if (string.IsNullOrWhiteSpace(input))
                {
                    _ui.TypingEffect("Please enter your question or command.", ConsoleColor.Yellow);
                    continue;
                }

                if (input.Equals("exit", StringComparison.OrdinalIgnoreCase))
                {
                    continueChat = false;
                }
                else if (input.Equals("help", StringComparison.OrdinalIgnoreCase))
                {
                    ShowHelp();
                }
                else
                {
                    ProcessNaturalInput(input);
                }

            }
        }

        private void ShowHelp()
        {
            _ui.TypingEffect("\nAvailable commands:", ConsoleColor.Green);
            _ui.TypingEffect("------------------", ConsoleColor.Green);
            _ui.TypingEffect("- Ask about: passwords, phishing, scams, privacy, 2FA, malware", ConsoleColor.White);
           
            _ui.TypingEffect("- exit                  : End the conversation", ConsoleColor.White);
            _ui.TypingEffect("\nExample questions:", ConsoleColor.Cyan);
            _ui.TypingEffect("- \"How can I create strong passwords?\"", ConsoleColor.White);
            _ui.TypingEffect("- \"Tell me about phishing scams\"", ConsoleColor.White);
            _ui.TypingEffect("- \"I'm worried about malware\"", ConsoleColor.White);
        }

        private void ProcessNaturalInput(string input)
        {
            _responses.HandleFreeTextInput(input);
        }
        private void ShowFarewell(string userName)
        {
            _ui.TypingEffect($"\nThank you for using the Cybersecurity Assistant, {userName}!", ConsoleColor.Cyan);
            _ui.TypingEffect("Remember to stay safe online!", ConsoleColor.Cyan);
            _ui.TypingEffect("\nPress any key to exit...", ConsoleColor.Gray);
            Console.ReadKey();
        }
    }
}