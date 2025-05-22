
using System;
using CybersecurityChatbot;

namespace CyberSecurity_Chatbot
{
  class Program
    {
        static void Main(string[] args)
        {
            var audioManager = new Audio();
            var userInterface = new UserInterface();
            var memoryService = new MemoryService();
            var sentimentAnalyzer = new SentimentAnalyzer();
            

            var responses = new Responses(userInterface, memoryService, sentimentAnalyzer);
            var chatbot = new ChatbotEngine(audioManager, userInterface, responses );
            // Start the chatbot
            chatbot.Run();
        }
    }
}
