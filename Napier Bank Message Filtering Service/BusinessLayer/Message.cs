using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BusinessLayer
{
    public abstract class Message
    {
        private string _header;
        private string _sender;

        public string Header
        {
            get => _header;
            set
            {
                if ((value.Length == 10) && int.TryParse(value.Substring(1), out _))
                {
                    _header = value.ToUpper();
                }
                else
                {
                    throw new ArgumentException("The value specified is not acceptable! (S, E or T followed by 0-9 9 times)");
                }
            }
        }

        public string Text { get; set; }

        public string Sender
        {
            get => _sender;
            set
            {
                 if (Header.StartsWith("E") || Header.StartsWith("T"))
                 {
                    _sender = value;
                 } else if (Header.StartsWith("S") && (value.StartsWith("+") && value.Length > 6)) 
                 {
                    _sender = value;
                 }
                 else
                 {
                    throw new ArgumentException("This is an invalid input!");
                 }
            }
        }

        public string ConvertAbbreviations(string msg, Dictionary<string, string> words)
        {
            string[] msgs = msg.Split(' ');
            StringBuilder sb = new StringBuilder();

            foreach (var s in msgs)
            {
                sb.Append(s + " ");

                if (words.ContainsKey(s))
                {
                    sb.Append("<" + words[s] + "> ");
                }
            }
            return sb.ToString().Trim();
        }

        public string QuarantineURL(string msg)
        {
            string regex = @"(https?:\/\/)?(www\.)?[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b([-a-zA-Z0-9()@:%_\+.~#?&\/\/=]*)";

            return Regex.Replace(msg, regex, "<URL Quarantined>");
        }

        public List<string> QuarantinedURLs(string msg)
        {
            Regex regex = new Regex(@"(https?:\/\/)?(www\.)?[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b([-a-zA-Z0-9()@:%_\+.~#?&\/\/=]*)");
            MatchCollection match = regex.Matches(msg);

            return (from Match m in match select m.Value).ToList();
        }
    }
}
