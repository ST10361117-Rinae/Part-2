using System;
using System.Collections.Generic;
using System.Media;

class CyberSecurityChatBot
{
    // Memory to store user preferences and details
    static string userName = "User";
    static string favoriteTopic = "";

    // Random generator for varied responses
    static Random rand = new Random();

    static void Main()
    {
        PlayVoiceGreeting();
        DisplayAsciiLogo();
        StartChat();
    }

    static void PlayVoiceGreeting()
    {
        try
        {
            using (SoundPlayer player = new SoundPlayer(@"C:\voicebot\WhatsApp Audio 2025-04-01 at 17.55.28.wav"))
            {
                player.PlaySync();
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Error playing greeting: " + ex.Message);
            Console.ResetColor();
        }
    }

    static void DisplayAsciiLogo()
    {
        Console.ForegroundColor = ConsoleColor.DarkBlue;
        Console.WriteLine(@"
 _        __                         _                
|_) _    /   _    _|_ o  _     _    / \__  |  o __  _ 
|_)(/_   \__(_||_| |_ | (_)|_|_>    \_/| | |  | | |(/_                                                                                                                                       
     Cybersecurity Awareness Chatbot 
        ");
        Console.ResetColor();
    }

    static void StartChat()
    {
        Console.Write("\nEnter your name: ");
        userName = Console.ReadLine()?.Trim();
        if (string.IsNullOrEmpty(userName)) userName = "User";

        Console.ForegroundColor = ConsoleColor.DarkBlue;
        Console.WriteLine($"\nHello {userName}! I'm here to help you stay safe online.\n");
        Console.ResetColor();

        while (true)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("\nAsk me a cybersecurity question: ");
            Console.ResetColor();

            string userInput = Console.ReadLine()?.Trim().ToLower();

            if (string.IsNullOrEmpty(userInput))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Oops! You didn't say anything. Try again.");
                Console.ResetColor();
                continue;
            }

            if (userInput == "exit" || userInput == "quit")
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Goodbye! Stay safe online.");
                Console.ResetColor();
                break;
            }

            RespondToUser(userInput);
        }
    }

    static void RespondToUser(string input)
    {
        Console.ForegroundColor = ConsoleColor.Magenta;

        // Sentiment detection
        if (input.Contains("worried") || input.Contains("scared") || input.Contains("nervous"))
        {
            Console.WriteLine("It's okay to feel that way. Although cybersecurity can seem overwhelming, I'm here to walk you through each step.");
            return;
        }
        else if (input.Contains("curious") || input.Contains("interested"))
        {
            Console.WriteLine("Curiosity is a great start! Let’s dive into cybersecurity together.");
            return;
        }
        else if (input.Contains("frustrated") || input.Contains("angry"))
        {
            Console.WriteLine("I understand this can be frustrating. Let me make things understandable for you.");
            return;
        }

        // Recognised topic keywords
        Dictionary<string, List<string>> keywordResponses = new Dictionary<string, List<string>>
        {
            ["password"] = new List<string>
            {
                "Make sure to use strong, unique password for each account.",
                "When realistic turn on two-factor authentication.",
                "Avoid using personal details in your password."
            },
            ["phishing"] = new List<string>
            {
                "Be cautious of emails asking personal information.",
                "Before clicking on links, always confirm the sender's email address.",
                "Scammers often disguise as trusted organisations."
            },
            ["privacy"] = new List<string>
            {
                "Great! I'll remember that you're interested in privacy.It's a crucial part of staying safe online.",
                "As someone interested in privacy, you might want to review the security settings on your account.",
                
            },
            ["scam"] = new List<string>
            {
                "If it sounds too good to be true, it probably is.",
                "Legitimate companies won't ask for personal information via email.",
                "Report scam attempts to relevant authorities immediately."
            }
        };

        // Remember favorite topic
        foreach (var keyword in keywordResponses.Keys)
        {
            if (input.Contains(keyword))
            {
                favoriteTopic = keyword;
                var responses = keywordResponses[keyword];
                Console.WriteLine(responses[rand.Next(responses.Count)]);

                Console.ResetColor();
                return;
            }
        }

        // Check if user is asking about chatbot
        if (input.Contains("how are you"))
        {
            Console.WriteLine("I'm operating at optimal efficiency! Thanks for asking.");
        }
        else if (input.Contains("your purpose"))
        {
            Console.WriteLine("I exist to help you understand cybersecurity and stay safe online.");
        }
        else if (input.Contains("what can i ask") || input.Contains("ask you about"))
        {
            Console.WriteLine("You can ask me about password safety, scams, privacy, phishing, or safe browsing.");
        }
        else if (input.Contains("interested in"))
        {
            int idx = input.IndexOf("interested in") + 13;
            string interest = input.Substring(idx).Trim();
            favoriteTopic = interest;
            Console.WriteLine($"Great! As someone interested in {favoriteTopic}, I’ll share more tips about it going forward.");
        }
        else if (!string.IsNullOrEmpty(favoriteTopic) && input.Contains("more"))
        {
            if (keywordResponses.ContainsKey(favoriteTopic))
            {
                var responses = keywordResponses[favoriteTopic];
                Console.WriteLine("Here’s another tip:");
                Console.WriteLine(responses[rand.Next(responses.Count)]);
            }
            else
            {
                Console.WriteLine("Tell me more about what you're interested in so I can assist better.");
            }
        }
        else
        {
            Console.WriteLine("I'm not sure I understand. Could you rephrase or ask about something like passwords, scams, or phishing?");
        }

        Console.ResetColor();
    }
}
