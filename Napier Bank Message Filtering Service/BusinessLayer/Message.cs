using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BusinessLayer
{
    [Serializable]
    public abstract class Message
    {
        private string _header;
        private string _subject;
        private string _text;
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
                    throw new ArgumentException("The value specified is not acceptable! (S, E or T followed by 0-9)");
                }
            }
        }

        public string Subject
        {
            get => _subject;
            set
            {
                if (value.Length <= 20 && value.Length > 0)
                    _subject = value;
                else throw new ArgumentException("Subject length must be in range 1 - 20 [inclusive]!");
            }
        }

        public string Text
        {
            get => _text;
            set => _text = value;
        }

        public string Sender { get; protected set; }

        protected string ConvertAbbreviations(string msg, Dictionary<string, string> words)
        {
            string[] msgs = msg.Split(' ');
            StringBuilder sb = new StringBuilder();

            foreach (var s in msgs)
            {
                sb.Append(s + " ");

                if (words.ContainsKey(s))
                {
                    sb.Append(" <" + words[s] + "> ");
                }
            }
            return sb.ToString().Trim();
        }

        protected List<string> ExtractAbbreviations(string msg, Dictionary<string, string> words)
        {
            string[] msgs = msg.Split(' ');

            return msgs.Where(words.ContainsKey).ToList();
        }

        protected string QuarantineURL(string msg)
        {
            Regex regex = new Regex(@"(https?:\/\/)?(www\.)?[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b([-a-zA-Z0-9()@:%_\+.~#?&\/\/=]*)");

            foreach (string s in regex.Matches(msg))
            {
                msg = msg.Replace(s, "<URL Quarantined>");
            }

            return msg;
        }

        protected List<string> GetHashTags(string msg)
        {
            string[] msgs = msg.Split(' ');

            List<string> hashes = new List<string>();

            foreach (var s in msgs)
            {
                if (s.StartsWith("#")) hashes.Add(s);
            }

            return hashes;
        }
    }
}
