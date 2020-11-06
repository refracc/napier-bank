using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BusinessLayer
{
    /// <summary>
    /// This class is about the abstract Message type.
    /// </summary>
    public abstract class Message
    {
        private string _header;
        private string _sender;

        /// <summary>
        /// The message header value.
        /// Must be S, E or T followed by 9 numbers.
        /// </summary>
        public string Header
        {
            get => _header;
            set
            {
                if ((value.Length == 10) && int.TryParse(value.Substring(1), out _)) // Remove first character and check the last 9 characters are numbers, then discard them with <code>_</code
                {
                    _header = value.ToUpper(); // Just make it S,E,T instead of s,e,t
                }
                else
                {
                    throw new ArgumentException("The value specified is not acceptable! (S, E or T followed by 0-9 9 times)");
                }
            }
        }

        /// <summary>
        /// The text of the message.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// The sender of the message.
        /// This is either:
        ///     - Phone number
        ///     - Email address
        ///     - Twitter user
        /// </summary>
        public string Sender
        {
            get => _sender;
            set
            {
                 if (Header.StartsWith("E") || Header.StartsWith("T"))
                 {
                    _sender = value;
                 } else if (Header.StartsWith("S") && (value.StartsWith("+") && value.Length > 6)) //Shortest phone number is 7 digits long
                 {
                    _sender = value;
                 }
                 else
                 {
                    throw new ArgumentException("This is an invalid input!");
                 }
            }
        }

        /// <summary>
        /// This method adds all abbreviations from a message.
        /// </summary>
        /// <param name="msg">The message to insert abbreviations.</param>
        /// <param name="words">The dictionary of words to select from.</param>
        /// <returns>A modified message with the necessary insertions.</returns>
        public string ConvertAbbreviations(string msg, Dictionary<string, string> words)
        {
            string[] msgs = msg.Split(' ');
            StringBuilder sb = new StringBuilder();

            foreach (var s in msgs)
            {
                sb.Append(s + " ");

                if (words.ContainsKey(s))
                {
                    sb.Append("<" + words[s] + "> "); // If the word is in the dictionary, replace it
                }
            }
            return sb.ToString().Trim();
        }

        /// <summary>
        /// This is the method for quarantining URLs.
        /// </summary>
        /// <param name="msg">The message to be modified and cleaned.</param>
        /// <returns>A modified and cleaned message with necessary edits made.</returns>
        public string QuarantineURL(string msg)
        {
            const string regex = @"(https?:\/\/)?(www\.)?[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b([-a-zA-Z0-9()@:%_\+.~#?&\/\/=]*)"; // URL regex

            return Regex.Replace(msg, regex, "<URL Quarantined>");
        }

        /// <summary>
        /// This method obtains a list of urls which have been detected by the filter.
        /// </summary>
        /// <param name="msg">The message being modified and to be cleaned.</param>
        /// <returns>A modified and cleaned message with necessary edits made.</returns>
        public List<string> QuarantinedURLs(string msg)
        {
            Regex regex = new Regex(@"(https?:\/\/)?(www\.)?[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b([-a-zA-Z0-9()@:%_\+.~#?&\/\/=]*)"); // URL regex
            MatchCollection match = regex.Matches(msg);

            return (from Match m in match select m.Value).ToList(); // return list using LINQ expression
        }
    }
}
