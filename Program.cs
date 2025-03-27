using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;

class ChatBot
{
    static void Main(string[] args)
    {
        Dictionary<string, string> responses = new Dictionary<string, string>
        {
            // Greetings
            {"hi", "Hi there! How can I assist you today?"},
            {"hello", "Hello! How can I help you with cybersecurity?"},
            {"what are you", "I'm just a bot, but I'm always here to help!"},

            // Cybersecurity Tips
            {"password", "Use a mix of uppercase, lowercase, numbers, and symbols. Avoid common words." +
                        "A strong password should be at least 12 characters long and avoid personal details." +
                        "Using a password manager helps you generate and store strong, unique passwords for every account."},
            {"phishing", " Don't click on links from unknown sources. Always verify emails before responding."},
            {"email scam", " Be cautious of emails asking for personal information. Verify the sender before clicking links."},
            {"social engineering", " Cybercriminals manipulate people into revealing sensitive information. Always double-check unexpected requests."},

            {"two-factor authentication", " Enable 2FA on all critical accounts for extra security."},
            {"2fa", " Two-factor authentication adds an extra layer of security beyond just a password."},

            {"malware", " Avoid downloading software from untrusted sources. Keep your antivirus updated."},
            {"ransomware", " Ransomware encrypts your files and demands payment. Regularly back up your data to stay protected."},
            {"firewall", "A firewall helps block unauthorized access to your system. Keep it enabled."},

            {"wifi", "Avoid using public WiFi for sensitive transactions. Use a VPN for better security."},
            {"vpn", "A VPN encrypts your internet connection, protecting your data from hackers on public networks."},

            {"cybersecurity", "Cybersecurity is about protecting systems, networks, and data from cyber threats."},
            {"hacking", "Hacking refers to unauthorized access to data or systems. Ethical hackers help improve security."},
        };

        DisplayAsciiArt();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\nWelcome to the Cybersecurity Awareness Bot!");
        Console.ResetColor();
        PlayNotificationSound();
        Console.WriteLine("Type 'exit' anytime to leave.");

        while (true)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("\nAsk me anything about Cybersecurity: ");
            Console.ResetColor();
            string userInput = Console.ReadLine().ToLower();

            if (userInput == "exit")
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Goodbye! Stay safe online.");
                Console.ResetColor();
                break;
            }

            bool found = false;
            foreach (var key in responses.Keys.OrderByDescending(k => k.Length)) 
            {
                if (userInput.Contains(key))
                {
                    Console.WriteLine(responses[key]);
                    found = true;
                    break;
                }
            }


            if (!found)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("I'm not sure about that. Try asking about password security, phishing, or two-factor authentication.");
                Console.ResetColor();
            }
        }
    }

    public static void PlayNotificationSound()
    {
        SoundPlayer player = new SoundPlayer("Recording.wav");
        player.Load();
        player.Play();
    }

    static void DisplayAsciiArt()
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine(@"
                   ____      _          _                 
                  / ___|   _| |__   ___| |_ __ _ ___ _ __ 
                 | |  | | | | '_ \ / _ \ __/ _` / _ \ '__|
                 | |__| |_| | |_) |  __/ || (_| |  __/ |   
                  \____\__, |_.__/ \___|\__\__, |\___|_|   
                       |___/               |___/           
                  Cybersecurity Awareness Bot
                        ");
        Console.ResetColor();
    }
}
