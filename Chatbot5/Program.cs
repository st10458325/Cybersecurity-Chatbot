using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;

class ChatBot
{
    private Dictionary<string, string> persistentMemory;
    private Dictionary<string, string> sessionMemory;
    private Dictionary<string, List<string>> responses;
    private List<string> sentimentInputs;
    private string userName;

    private readonly string[] positiveWords = { "good", "great", "happy", "excited", "relieved" };
    private readonly string[] negativeWords = { "sad", "angry", "stressed", "upset", "overwhelmed", "worried", "frustrated" };
    private readonly string[] sentimentTriggers = { "worried", "curious", "frustrated", "sad", "angry", "stressed", "upset", "overwhelmed", "good", "great", "happy", "excited", "relieved" };
    private const string memoryTrigger = "remember";

    private string MemoryFile => $"{userName}.txt";

    public ChatBot()
    {
        sessionMemory = new Dictionary<string, string>();
        sentimentInputs = new List<string>();

        responses = new Dictionary<string, List<string>>
        {
            {"hi", new List<string>{"Hi there! How can I assist you today?", "Hello! Need help with cybersecurity?"} },
            {"hello", new List<string>{"Hello! How can I help you?", "Hi! How's your day?"} },
            {"what are you", new List<string>{"I'm just a bot, but I'm here to help!", "I'm a cybersecurity chatbot!"} },
            {"password", new List<string>{"Use a mix of uppercase, lowercase, numbers, and symbols.", "A strong password should be unique and hard to guess."} },
            {"phishing", new List<string>{"Don't click on links from unknown sources.", "Phishing attacks often come from emails pretending to be legitimate."} },
            {"email", new List<string>{"Be cautious of emails asking for personal info.", "Verify the sender before clicking links."} },
            {"social", new List<string>{"Cybercriminals manipulate people into revealing sensitive information.", "Always double-check unexpected requests."} },
            {"two-factor", new List<string>{"Enable 2FA for extra security.", "2FA makes it harder for hackers to access your accounts."} },
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
        DisplayAsciiArt();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\nWelcome to the Cybersecurity Awareness Bot!");
        Console.ResetColor();
        PlayNotificationSound();

        Console.Write("Are you a new user? (yes/no): ");
        var answer = Console.ReadLine().Trim().ToLower();

        Console.Write("Enter your name: ");
        userName = Console.ReadLine().Trim();
        if (string.IsNullOrWhiteSpace(userName)) userName = "User";

        if (answer == "yes" || answer == "y")
        {
            persistentMemory = new Dictionary<string, string> { { "name", userName } };
            SavePersistentMemory();
            Console.WriteLine($"Nice to meet you, {userName}! Your data will be saved.");
        }
        else
        {
            if (File.Exists(MemoryFile))
            {
                persistentMemory = LoadPersistentMemory();
                Console.WriteLine($"Welcome back, {persistentMemory["name"]}!");
            }
            else
            {
                persistentMemory = new Dictionary<string, string> { { "name", userName } };
                SavePersistentMemory();
                Console.WriteLine($"No existing data found. Created new profile for {userName}.");
            }
        }

        Console.WriteLine("Type 'exit' anytime to leave.");

        while (true)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"\n{userName}: ");
            Console.ResetColor();

            string userInput = Console.ReadLine().Trim();
            if (string.IsNullOrWhiteSpace(userInput))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Bot: Please enter a valid question.");
                Console.ResetColor();
                continue;
            }

            if (userInput.Equals("exit", StringComparison.OrdinalIgnoreCase))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Bot: Goodbye! Stay safe online.");
                Console.ResetColor();
                break;
            }

            sessionMemory["last_topic"] = userInput;
            File.AppendAllText(MemoryFile, $"Input|{userInput}{Environment.NewLine}");
            RespondToUser(userInput.ToLower());
        }
    }

    private void RespondToUser(string userInput)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("Bot: ");

        if (userInput.Contains(memoryTrigger))
        {
            var sb = new StringBuilder();
            sb.AppendLine("Here's what I remember:");
            foreach (var kv in persistentMemory)
                sb.AppendLine($"- {kv.Key}: {kv.Value}");
            foreach (var kv in sessionMemory)
                sb.AppendLine($"- {kv.Key}: {kv.Value}");
            foreach (var input in sentimentInputs)
                sb.AppendLine($"- Feeling: {input}");
            DisplayTypingEffect(sb.ToString());
            Console.ResetColor();
            return;
        }

        bool found = false;
        foreach (var key in responses.Keys.OrderByDescending(k => k.Length))
        {
            if (userInput.Contains(key))
            {
                DisplayTypingEffect(GetRandomResponse(key));
                found = true;
                break;
            }
        }
        

        bool sentimentFound = false;    
        if (sentimentTriggers.Any(t => userInput.Contains(t)))
        {
            string sentiment = DetectSentiment(userInput);
            sentimentInputs.Add(userInput);
            DisplayTypingEffect(GetEmpathyResponse(sentiment));
            sentimentFound = true;
        }

        if (!found && !sentimentFound)
            DisplayTypingEffect("I'm not sure about that. Try asking about password security, phishing, or two-factor authentication.");
        Console.ResetColor();
    }

    private string DetectSentiment(string input)
    {
        if (negativeWords.Any(w => input.Contains(w))) return "negative";
        if (positiveWords.Any(w => input.Contains(w))) return "positive";
        return "neutral";
    }

    private string GetEmpathyResponse(string sentiment)
    {
        switch (sentiment)
        {
            case "positive":
                return "I'm glad to hear that. How can I assist you further?";
            case "negative":
                return "I'm sorry you're feeling that way. Would you like to talk about it?";
            default:
                return "Thanks for sharing. What else would you like to know?";
        }
    }

    private string GetRandomResponse(string key)
    {
        try
        {
            var rnd = new Random();
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

    private Dictionary<string, string> LoadPersistentMemory()
    {
        var mem = new Dictionary<string, string>();
        if (File.Exists(MemoryFile))
        {
            foreach (var line in File.ReadAllLines(MemoryFile))
            {
                var parts = line.Split('|');
                if (parts.Length == 2 && !parts[0].Equals("Input"))
                    mem[parts[0]] = parts[1];
            }
        }
        return mem;
    }

    private void SavePersistentMemory()
    {
        var sb = new StringBuilder();
        foreach (var kvp in persistentMemory)
            sb.AppendLine($"{kvp.Key}|{kvp.Value}");
        File.WriteAllText(MemoryFile, sb.ToString());
    }

    static void Main(string[] args)
    {
        ChatBot bot = new ChatBot();
        bot.Start();
    }
}
