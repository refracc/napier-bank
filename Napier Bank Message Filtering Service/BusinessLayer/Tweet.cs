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

        public Tweet(string sender, string message)
        {
            if (Validate(sender, message))
            {
                Sender = sender;
                Text = message;
            }
        }

        public bool Validate(string sender, string msg) => (sender.StartsWith("@")) && (msg.Length <= 140);

        public List<string> ExtractMentions(string msg)
        {
            string[] data = msg.Split(' ');

            return data.Where(s => s.StartsWith("@")).ToList();
        }
    }
}
