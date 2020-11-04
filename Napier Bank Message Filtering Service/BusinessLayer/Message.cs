﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BusinessLayer
{
    public abstract class Message
    {
        private string _header;
        private string _subject;
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

        public string Text { get; set; }

        public string Sender
        {
            get => _sender;
            set
            {
                if (Header.StartsWith("S") && (value.StartsWith("+") && value.Length > 6))
                {
                    _sender = value;
                } else if (Header.StartsWith("E") || Header.StartsWith("T"))
                {
                    _sender = value;
                }
                else
                {
                    throw new ArgumentException("This is an invalid input!");
                }
            }
        }

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

        protected string QuarantineURL(string msg)
        {
            Regex regex = new Regex(@"(https?:\/\/)?(www\.)?[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b([-a-zA-Z0-9()@:%_\+.~#?&\/\/=]*)");

            foreach (string s in regex.Matches(msg))
            {
                msg = regex.Replace(s, "<URL Quarantined>");
            }

            return msg;
        }

        public List<string> QuarantinedURLs(string msg)
        {
            //Regex regex = new Regex(@"(https?:\/\/)?(www\.)?[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b([-a-zA-Z0-9()@:%_\+.~#?&\/\/=]*)");
            //MatchCollection match = regex.Matches(msg);

            //List<object> urls = new List<object>();

            //foreach (Match m in match)
            //{
            //    urls.Add(m.Value);
            //}

            //return urls;

            List<string> urls = new List<string>();
            string[] data = msg.Split(' ');

            foreach (string s in data)
            {
                if (s.StartsWith("http://") || s.StartsWith("https://"))
                {
                    urls.Add(s);
                }
            }

            return urls;
        }
    }
}
