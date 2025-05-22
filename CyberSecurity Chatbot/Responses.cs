using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using CyberSecurity_Chatbot;





namespace CybersecurityChatbot
    {
        public class Responses
        {
            private readonly UserInterface _ui;
            private readonly MemoryService _memory;
            private readonly SentimentAnalyzer _sentimentAnalyzer;
            private readonly Dictionary<string, Action> _keywordHandlers;

            public Responses(UserInterface ui, MemoryService memory, SentimentAnalyzer sentimentAnalyzer)
            {
                _ui = ui;
                _memory = memory;
                _sentimentAnalyzer = sentimentAnalyzer;
                _keywordHandlers = InitializeKeywordHandlers();
            }

            private Dictionary<string, Action> InitializeKeywordHandlers()
            {
                return new Dictionary<string, Action>(StringComparer.OrdinalIgnoreCase)
                {

                    ["password"] = ShowPasswordTips,
                    ["scam"] = ShowScamInfo,
                    ["privacy"] = ShowPrivacyInfo,
                    ["phishing"] = ShowPhishingInfo,
                    ["authentication"] = ShowTwoFactorAuthInfo,
                    ["2fa"] = ShowTwoFactorAuthInfo,
                    ["virus"] = ShowMalwareInfo,
                    ["malware"] = ShowMalwareInfo,
                    ["hack"] = ShowHackingPreventionInfo,
                    ["social media"] = ShowSocialMediaSafetyInfo,
                    ["wifi"] = ShowPublicWifiSafetyInfo,
                    ["public wifi"] = ShowPublicWifiSafetyInfo,
                   
                };
            }

            public void HandleFreeTextInput(string input)
            {
                var sentiment = _sentimentAnalyzer.AnalyzeSentiment(input);

                // Check for keywords
                var keyword = _keywordHandlers.Keys.FirstOrDefault(k => input.IndexOf(k, StringComparison.OrdinalIgnoreCase) >= 0);

                if (keyword != null)
                {
                    // Remember interest in this topic if expressed
                    if (input.ToLower().Contains("interested") || input.ToLower().Contains("curious"))
                    {
                        _memory.RememberInterest(keyword, input);
                        _ui.TypingEffect($"I'll remember you're interested in {keyword}. ", ConsoleColor.Green);
                        Thread.Sleep(500);
                    }

                    // Check if we should reference remembered interests
                    var rememberedTopics = _memory.GetRememberedTopics();
                    if (rememberedTopics.Any())
                    {
                        _ui.TypingEffect($"As someone interested in {string.Join(" and ", rememberedTopics)}, ", ConsoleColor.Cyan);
                        Thread.Sleep(300);
                    }

                    // Add sentiment-aware introduction
                    if (sentiment != "neutral")
                    {
                        _ui.TypingEffect(_sentimentAnalyzer.GetResponseForSentiment(sentiment, keyword), ConsoleColor.Green);
                        Thread.Sleep(500);
                    }

                    // Show the actual response
                    _keywordHandlers[keyword].Invoke();
                }
                else
                {
                    _ui.TypingEffect("I'm not sure I understand. Could you try rephrasing or ask about:", ConsoleColor.Yellow);
                    _ui.TypingEffect("- Passwords", ConsoleColor.Yellow);
                    _ui.TypingEffect("- Scams", ConsoleColor.Yellow);
                    _ui.TypingEffect("- Privacy", ConsoleColor.Yellow);
                    _ui.TypingEffect("- Phishing", ConsoleColor.Yellow);
                    _ui.TypingEffect("- Two-factor authentication", ConsoleColor.Yellow);
                }
            }

            public void ShowPhishingInfo()
            {
                _ui.TypingEffect("Phishing is a cyber attack using disguised emails:", ConsoleColor.Green);
                Thread.Sleep(300);
                _ui.TypingEffect("- Targets: Banks, companies, social media", ConsoleColor.Green);
                _ui.TypingEffect("- Goals: Steal credentials, install malware", ConsoleColor.Green);
                _ui.TypingEffect("- Common signs: Urgent language, mismatched sender addresses", ConsoleColor.Green);
                Thread.Sleep(300);
                _ui.TypingEffect("Protection tips:", ConsoleColor.Cyan);
                _ui.TypingEffect("- Verify sender email addresses carefully", ConsoleColor.Green);
                _ui.TypingEffect("- Hover over links before clicking", ConsoleColor.Green);
                _ui.TypingEffect("- Never enter credentials from email links", ConsoleColor.Green);
                _ui.TypingEffect("- Use anti-phishing browser extensions", ConsoleColor.Green);
            }

            public void ShowPasswordTips()
            {
                _ui.TypingEffect("Strong password guidelines:", ConsoleColor.Green);
                Thread.Sleep(300);
                _ui.TypingEffect("- Minimum 12 characters (longer is better)", ConsoleColor.Green);
                _ui.TypingEffect("- Mix uppercase, lowercase, numbers, symbols", ConsoleColor.Green);
                _ui.TypingEffect("- Avoid dictionary words and personal info", ConsoleColor.Green);
                _ui.TypingEffect("- Use passphrases: 'PurpleTurtle$JumpsHigh!'", ConsoleColor.Green);
                Thread.Sleep(300);
                _ui.TypingEffect("Management tips:", ConsoleColor.Cyan);
                _ui.TypingEffect("- Use a password manager (Bitwarden, LastPass)", ConsoleColor.Green);
                _ui.TypingEffect("- Never reuse passwords across sites", ConsoleColor.Green);
                _ui.TypingEffect("- Change passwords after breaches", ConsoleColor.Green);
                _ui.TypingEffect("- Enable 2FA wherever possible", ConsoleColor.Green);
            }

            public void ShowScamInfo()
            {
                _ui.TypingEffect("Common online scams to watch for:", ConsoleColor.Green);
                Thread.Sleep(300);
                _ui.TypingEffect("- Tech support scams (fake virus warnings)", ConsoleColor.Green);
                _ui.TypingEffect("- Romance scams (fake relationships)", ConsoleColor.Green);
                _ui.TypingEffect("- Investment scams (get-rich-quick schemes)", ConsoleColor.Green);
                _ui.TypingEffect("- Impersonation scams (fake officials)", ConsoleColor.Green);
                Thread.Sleep(300);
                _ui.TypingEffect("Protection tips:", ConsoleColor.Cyan);
                _ui.TypingEffect("- Never send money to strangers", ConsoleColor.Green);
                _ui.TypingEffect("- Verify identities through official channels", ConsoleColor.Green);
                _ui.TypingEffect("- Be skeptical of too-good-to-be-true offers", ConsoleColor.Green);
                _ui.TypingEffect("- Report scams to relevant authorities", ConsoleColor.Green);
            }

            public void ShowPrivacyInfo()
            {
                _ui.TypingEffect("Protecting your privacy online:", ConsoleColor.Green);
                Thread.Sleep(300);
                _ui.TypingEffect("- Review privacy settings on all accounts", ConsoleColor.Green);
                _ui.TypingEffect("- Limit personal info shared on social media", ConsoleColor.Green);
                _ui.TypingEffect("- Use privacy-focused browsers (Brave, Firefox)", ConsoleColor.Green);
                _ui.TypingEffect("- Consider using a VPN for browsing", ConsoleColor.Green);
                Thread.Sleep(300);
                _ui.TypingEffect("Advanced privacy measures:", ConsoleColor.Cyan);
                _ui.TypingEffect("- Use encrypted messaging (Signal, WhatsApp)", ConsoleColor.Green);
                _ui.TypingEffect("- Enable privacy features in your OS", ConsoleColor.Green);
                _ui.TypingEffect("- Regularly clear cookies and cache", ConsoleColor.Green);
                _ui.TypingEffect("- Use alias emails for signups", ConsoleColor.Green);
            }

            public void ShowTwoFactorAuthInfo()
            {
                _ui.TypingEffect("Two-Factor Authentication (2FA) adds critical security:", ConsoleColor.Green);
                Thread.Sleep(300);
                _ui.TypingEffect("- Types: SMS, authenticator apps, security keys", ConsoleColor.Green);
                _ui.TypingEffect("- Recommended: Authenticator apps (Google Auth, Authy)", ConsoleColor.Green);
                _ui.TypingEffect("- Most secure: Hardware security keys (YubiKey)", ConsoleColor.Green);
                Thread.Sleep(300);
                _ui.TypingEffect("Setup recommendations:", ConsoleColor.Cyan);
                _ui.TypingEffect("- Enable on email, banking, social media first", ConsoleColor.Green);
                _ui.TypingEffect("- Save backup codes securely", ConsoleColor.Green);
                _ui.TypingEffect("- Consider a password manager with 2FA", ConsoleColor.Green);
                _ui.TypingEffect("- Review active sessions periodically", ConsoleColor.Green);
            }

            public void ShowMalwareInfo()
            {
                _ui.TypingEffect("Malware includes viruses, ransomware, and spyware:", ConsoleColor.Green);
                Thread.Sleep(300);
                _ui.TypingEffect("- Infection vectors: Email attachments, fake downloads", ConsoleColor.Green);
                _ui.TypingEffect("- Signs: Slow performance, popups, strange behavior", ConsoleColor.Green);
                Thread.Sleep(300);
                _ui.TypingEffect("Protection tips:", ConsoleColor.Cyan);
                _ui.TypingEffect("- Install reputable antivirus software", ConsoleColor.Green);
                _ui.TypingEffect("- Keep all software updated", ConsoleColor.Green);
                _ui.TypingEffect("- Don't disable security features", ConsoleColor.Green);
                _ui.TypingEffect("- Backup important files regularly", ConsoleColor.Green);
            }

            public void ShowHackingPreventionInfo()
            {
                _ui.TypingEffect("Preventing unauthorized access:", ConsoleColor.Green);
                Thread.Sleep(300);
                _ui.TypingEffect("- Use strong, unique passwords everywhere", ConsoleColor.Green);
                _ui.TypingEffect("- Enable login notifications where available", ConsoleColor.Green);
                _ui.TypingEffect("- Monitor for unusual account activity", ConsoleColor.Green);
                Thread.Sleep(300);
                _ui.TypingEffect("Advanced protection:", ConsoleColor.Cyan);
                _ui.TypingEffect("- Consider a password manager", ConsoleColor.Green);
                _ui.TypingEffect("- Use a hardware security key for critical accounts", ConsoleColor.Green);
                _ui.TypingEffect("- Enable biometric authentication where possible", ConsoleColor.Green);
                _ui.TypingEffect("- Regularly review connected apps/services", ConsoleColor.Green);
            }

            public void ShowSocialMediaSafetyInfo()
            {
                _ui.TypingEffect("Staying safe on social media:", ConsoleColor.Green);
                Thread.Sleep(300);
                _ui.TypingEffect("- Limit personal info in profiles", ConsoleColor.Green);
                _ui.TypingEffect("- Be cautious with friend requests from strangers", ConsoleColor.Green);
                _ui.TypingEffect("- Review privacy settings regularly", ConsoleColor.Green);
                Thread.Sleep(300);
                _ui.TypingEffect("Additional precautions:", ConsoleColor.Cyan);
                _ui.TypingEffect("- Disable location tagging", ConsoleColor.Green);
                _ui.TypingEffect("- Be wary of quizzes/data collection", ConsoleColor.Green);
                _ui.TypingEffect("- Use different passwords for each platform", ConsoleColor.Green);
                _ui.TypingEffect("- Enable login approvals", ConsoleColor.Green);
            }

            public void ShowPublicWifiSafetyInfo()
            {
                _ui.TypingEffect("Risks of public WiFi networks:", ConsoleColor.Green);
                Thread.Sleep(300);
                _ui.TypingEffect("- Man-in-the-middle attacks", ConsoleColor.Green);
                _ui.TypingEffect("- Fake hotspots with similar names", ConsoleColor.Green);
                _ui.TypingEffect("- Unencrypted data transmission", ConsoleColor.Green);
                Thread.Sleep(300);
                _ui.TypingEffect("Safety measures:", ConsoleColor.Cyan);
                _ui.TypingEffect("- Use a reputable VPN service", ConsoleColor.Green);
                _ui.TypingEffect("- Avoid accessing sensitive accounts", ConsoleColor.Green);
                _ui.TypingEffect("- Verify network names with staff", ConsoleColor.Green);
                _ui.TypingEffect("- Enable firewall on your device", ConsoleColor.Green);
            }

            public void ShowTaskManagementHelp()
            {
                _ui.TypingEffect("You can manage cybersecurity tasks with commands like:", ConsoleColor.Green);
                _ui.TypingEffect("- 'Add task [title]' to create a new task", ConsoleColor.Green);
                _ui.TypingEffect("- 'List tasks' to view your tasks", ConsoleColor.Green);
                _ui.TypingEffect("- 'Complete task [number]' to mark a task done", ConsoleColor.Green);
                _ui.TypingEffect("- 'Delete task [number]' to remove a task", ConsoleColor.Green);
                _ui.TypingEffect("\nExample task ideas:", ConsoleColor.Cyan);
                _ui.TypingEffect("- 'Add task Enable 2FA on email account'", ConsoleColor.White);
                _ui.TypingEffect("- 'Add task Review Facebook privacy settings'", ConsoleColor.White);
                _ui.TypingEffect("- 'Add task Update all device passwords'", ConsoleColor.White);
            }
      
    }
    }
