using System;
using System.Collections.Generic;

class ChatBot
{
    static void Main(string[] args)
    {
        Dictionary<string, string> responses = new Dictionary<string, string>
        {
            // Greetings
            {"hi", "Hi there! How can I assist you today?"},
            {"hello", "Hello! How can I help you with cybersecurity?"},
            {"how are you", "I'm just a bot, but I'm always here to help!"},

            // Cybersecurity Tips
            {"password security", "Use a mix of uppercase, lowercase, numbers, and symbols. Avoid common words."},
            {"strong password", "A strong password should be at least 12 characters long and avoid personal details."},
            {"password manager", "Using a password manager helps you generate and store strong, unique passwords for every account."},

            {"phishing", "Don't click on links from unknown sources. Always verify emails before responding."},
            {"email scam", "Be cautious of emails asking for personal information. Verify the sender before clicking links."},
            {"social engineering", "Cybercriminals manipulate people into revealing sensitive information. Always double-check unexpected requests."},

            {"two-factor authentication", "Enable 2FA on all critical accounts for extra security."},
            {"2fa", "Two-factor authentication adds an extra layer of security beyond just a password."},

            {"malware", "Avoid downloading software from untrusted sources. Keep your antivirus updated."},
            {"ransomware", "Ransomware encrypts your files and demands payment. Regularly back up your data to stay protected."},
            {"firewall", "A firewall helps block unauthorized access to your system. Keep it enabled."},

            {"wifi", "Avoid using public WiFi for sensitive transactions. Use a VPN for better security."},
            {"vpn", "A VPN encrypts your internet connection, protecting your data from hackers on public networks."},

            {"cybersecurity", "Cybersecurity is about protecting systems, networks, and data from cyber threats."},
            {"hacking", "Hacking refers to unauthorized access to data or systems. Ethical hackers help improve security."},

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
