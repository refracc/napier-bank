using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLayer;

namespace BusinessLayer
{
    /// <summary>
    /// This is the class responsible for handling Tweets.
    /// </summary>
    [Serializable]
    public class Tweet : Message
    {
        private Tweet() {}

        private readonly LoadSingleton _ls = LoadSingleton.Instance;

        /// <summary>
        /// The only valid way of creating tweets is through this constructor.
        /// </summary>
        /// <param name="sender">The handle of the user (@[name])</param>
        /// <param name="header">The header of the tweet (T + 9 numbers)</param>
        /// <param name="message">The body of the message</param>
        public Tweet(string sender, string header, string message)
        {
            if (Validate(sender, message))
            {
                Header = header;
                Sender = sender;
                Text = ConvertAbbreviations(message, _ls.GetAbbreviations());
            }
            else
            {
                throw new ArgumentException("This tweet contains invalid data!");
            }
        }

        /// <summary>
        /// The method used for validating tweets
        /// </summary>
        /// <param name="sender">The handle having an @</param>
        /// <param name="msg">The message being less than 140 chars</param>
        /// <returns>True if valid, false otherwise.</returns>
        public bool Validate(string sender, string msg) => (sender.StartsWith("@")) && (msg.Length <= 140);

        /// <summary>
        /// The method used for returning a list of mentioned users from the tweet.
        /// </summary>
        /// <param name="msg">The message to check.</param>
        /// <returns>A list of mentioned users.</returns>
        public List<string> ExtractMentions(string msg)
        {
            string[] data = msg.Split(' ');

            return data.Where(s => s.StartsWith("@")).ToList(); // return list using LINQ expression
        }

        /// <summary>
        /// The method used for returning a list of hashtags used in the message.
        /// </summary>
        /// <param name="msg">The message to check</param>
        /// <returns>A list of all words starting with #.</returns>
        public List<string> ExtractHashtags(string msg)
        {
            string[] msgs = msg.Split(' ');

            return msgs.Where(s => s.StartsWith("#")).ToList(); // return list using LINQ expression
        }
    }
}
