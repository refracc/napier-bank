using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLayer
{
    [Serializable]
    public class Tweet : Message
    {
        private Tweet() {}

        public Tweet(string sender, string header, string message)
        {
            if (Validate(sender, message))
            {
                Sender = sender;
                Text = message;
                Header = header;
            }
        }

        public bool Validate(string sender, string msg) => (sender.StartsWith("@")) && (msg.Length <= 140);

        public List<string> ExtractMentions(string msg)
        {
            string[] data = msg.Split(' ');

            return data.Where(s => s.StartsWith("@")).ToList();
        }

        protected List<string> ExtractHashtags(string msg)
        {
            string[] msgs = msg.Split(' ');

            return msgs.Where(s => s.StartsWith("#")).ToList();
        }
    }
}
