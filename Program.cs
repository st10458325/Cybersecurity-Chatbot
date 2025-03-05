using System;
using System.Collections.Generic;

class ChatBot
{
    static void Main(string[] args)
    {
        Dictionary<string, string> responses = new Dictionary<string, string>
        {
            // Greetings
            {"hi", "Hi there!"},
            {"hello", "Hello! How can I help you?"},
            {"how are you", "I'm doing great, thank you!"},

            // Cybersecurity Tips
            {"password security", "Use a mix of uppercase, lowercase, numbers, and symbols. Avoid common words."},
            {"phishing", "Don't click on links from unknown sources. Always verify emails before responding."},
            {"two-factor authentication", "Enable 2FA on all critical accounts for extra security."}
        };

        Console.WriteLine("Hello! Welcome to your personal Cybersecurity Chatbot.");
        Console.WriteLine("Type 'exit' anytime to leave.");

        while (true)
        {
            Console.Write("\nAsk me anything about Cybersecurity: ");
            string userInput = Console.ReadLine().ToLower();

            if (userInput == "exit")
            {
                Console.WriteLine("Goodbye! Stay safe online.");
                break;
            }

            bool found = false;
            foreach (var key in responses.Keys)
            {
                if (userInput.Contains(key)) // Check if the user's sentence contains a keyword
                {
                    Console.WriteLine(responses[key]);
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                Console.WriteLine("I'm not sure about that. Try asking about password security, phishing, or two-factor authentication.");
            }
        }
    }
}
