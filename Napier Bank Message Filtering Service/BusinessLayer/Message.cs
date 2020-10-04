using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace BusinessLayer
{
    public abstract class Message
    {
        public string Header { 
            get { return Header; } 
            set 
            {
                if (value.Length == 10)
                {
                    if (int.TryParse(value.Substring(1), out int n))
                    {
                        value.ToUpper();
                    }
                    else
                    {
                        throw new ArgumentException("The value defined is non-numeric (0-9)");
                    }
                }
                else
                {
                    throw new ArgumentException("The value length is not 10.");
                }
            }
        }

        public string Body;

        protected string ConvertAbbreviations(string msg, Dictionary<string, string> words)
        {
            string[] msgs = msg.Split(' ');
            StringBuilder sb = new StringBuilder();
            
            for(int i = 0; i < msgs.Length; i++)
            {
                string s = msgs[i];
                sb.Append(s + " ");

                if(words.ContainsKey(s))
                {
                    sb.Append(" <" + words[s] + "> ");
                }
            }
            return sb.ToString().Trim();
        }
    }
}
