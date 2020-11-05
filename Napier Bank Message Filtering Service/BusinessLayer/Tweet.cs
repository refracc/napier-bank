using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLayer;

namespace BusinessLayer
{
    [Serializable]
    public class Tweet : Message
    {
        private Tweet() {}

        private readonly LoadSingleton _ls = LoadSingleton.Instance;

        public Tweet(string sender, string header, string message)
        {
            if (Validate(sender, message))
            {
                Header = header;
                Sender = sender;
                Text = ConvertAbbreviations(message, _ls.GetAbbreviations());
            }
        }

        public bool Validate(string sender, string msg) => (sender.StartsWith("@")) && (msg.Length <= 140);

        public List<string> ExtractMentions(string msg)
        {
            string[] data = msg.Split(' ');

            return data.Where(s => s.StartsWith("@")).ToList();
        }

        public  List<string> ExtractHashtags(string msg)
        {
            string[] msgs = msg.Split(' ');

            return msgs.Where(s => s.StartsWith("#")).ToList();
        }
    }
}
