using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.IO;
using System.Threading;

class ChatBot
{
    private Dictionary<string, List<string>> responses;
    private string userName;

    public ChatBot()
    {
        responses = new Dictionary<string, List<string>>
        {
            {"hi", new List<string>{"Hi there! How can I assist you today?", "Hello! Need help with cybersecurity?"} },
            {"hello", new List<string>{"Hello! How can I help you?", "Hi! How's your day?"} },
            {"what are you", new List<string>{"I'm just a bot, but I'm here to help!", "I'm a cybersecurity chatbot!"} },
            {"password", new List<string>{"Use a mix of uppercase, lowercase, numbers, and symbols.", "A strong password should be unique and hard to guess."} },
            {"phishing", new List<string>{"Don't click on links from unknown sources.", "Phishing attacks often come from emails pretending to be legitimate."} },
            {"email", new List<string>{"Be cautious of emails asking for personal info.", "Verify the sender before clicking links."} },
            {"social", new List<string>{"Cybercriminals manipulate people into revealing sensitive information.", "Always double-check unexpected requests."} },
            {"twofactor","authentication", new List<string>{"Enable 2FA for extra security.", "2FA makes it harder for hackers to access your accounts."} },
            {"vpn", new List<string>{"A VPN encrypts your internet connection.", "Use a VPN on public Wi-Fi for added security."} },
            {"malware", new List<string>{"Avoid downloading software from untrusted sources.", "Keep your antivirus updated."} },
            {"firewall", new List<string>{"A firewall helps block unauthorized access.", "Enable your firewall for better protection."} },
            {"encryption", new List<string>{"Encryption protects your data.", "Encrypted data is unreadable without the correct key."} },
            {"ddos", new List<string>{"DDoS attacks flood systems with traffic.", "Mitigate DDoS attacks using rate-limiting and firewalls."} },
            {"cybersecurity", new List<string>{"Cybersecurity protects data and systems.", "Always stay informed about security risks."} },
            {"ransomware", new List<string>{"Ransomware encrypts your files and demands payment.", "Backup your data regularly to mitigate ransomware attacks."} },
            {"breach", new List<string>{"A data breach exposes sensitive information.", "Use strong, unique passwords to protect your accounts."} },
            {"spyware", new List<string>{"Spyware secretly monitors your activities.", "Use anti-spyware tools to detect and remove spyware."} },
            {"trojan", new List<string>{"A trojan disguises itself as legitimate software.", "Avoid downloading software from untrusted sources."} },
            {"worm", new List<string>{"A worm spreads across networks without user intervention.", "Keep your software updated to prevent worm infections."} }
        };
    }

    public void Start()
    {
        try
        {
            DisplayAsciiArt();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nWelcome to the Cybersecurity Awareness Bot!");
            Console.ResetColor();
            PlayNotificationSound();
            Console.Write("Enter your name: ");
            userName = Console.ReadLine().Trim();
            if (string.IsNullOrWhiteSpace(userName)) userName = "User";
            Console.WriteLine("Type 'exit' anytime to leave.");

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write($"\n{userName}: ");
                Console.ResetColor();
                string userInput = Console.ReadLine().Trim().ToLower();

                if (string.IsNullOrWhiteSpace(userInput))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Bot: Please enter a valid question.");
                    Console.ResetColor();
                    continue;
                }

                if (userInput == "exit")
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Bot: Goodbye! Stay safe online.");
                    Console.ResetColor();
                    break;
                }

                RespondToUser(userInput);
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("An error occurred: " + ex.Message);
            Console.ResetColor();
        }
    }

    private void RespondToUser(string userInput)
    {
        try
        {
            bool found = false;
            foreach (var key in responses.Keys.OrderByDescending(k => k.Length))
            {
                if (userInput.Contains(key))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("Bot: ");
                    DisplayTypingEffect(GetRandomResponse(key));
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Bot: I'm not sure about that. Try asking about password security, phishing, or two-factor authentication.");
                Console.ResetColor();
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Bot: An error occurred while processing your request: " + ex.Message);
            Console.ResetColor();
        }
    }

    private string GetRandomResponse(string key)
    {
        try
        {
            Random rnd = new Random();
            return responses[key][rnd.Next(responses[key].Count)];
        }
        catch
        {
            return "Sorry, something went wrong while fetching the response.";
        }
    }

    private void PlayNotificationSound()
    {
        try
        {
            SoundPlayer player = new SoundPlayer("Recording.wav");
            player.Load();
            player.Play();
        }
        catch
        {
            Console.WriteLine("(Sound effect unavailable.)");
        }
    }

    private void DisplayAsciiArt()
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

    private void DisplayTypingEffect(string message)
    {
        foreach (char c in message)
        {
            Console.Write(c);
            Thread.Sleep(30);
        }
        Console.WriteLine();
    }

    static void Main(string[] args)
    {
        ChatBot bot = new ChatBot();
        bot.Start();
    }
}