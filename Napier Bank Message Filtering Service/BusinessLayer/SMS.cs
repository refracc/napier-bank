using DataLayer;
using System;
using System.Collections.Generic;

namespace BusinessLayer
{
    /// <summary>
    /// This class is responsible for handling SMS messages being passed through the system.
    /// </summary>
    [Serializable]
    public class SMS : Message
    {
        private static readonly LoadSingleton _ls = LoadSingleton.Instance;

        private readonly Dictionary<string, string> _words = _ls.GetAbbreviations();

        private SMS() { }

        /// <summary>
        /// This is the only valid way to create an SMS.
        /// </summary>
        /// <param name="number">The number (sender) of the message</param>
        /// <param name="header">The header of the message (S + 9 numbers)</param>
        /// <param name="body">The body of the SMS</param>
        public SMS(string number, string header, string body)
        {
            Header = header;
            Sender = number;

            CheckTextValid(body);
        }

        /// <summary>
        /// Check the information in the message is valid.
        /// </summary>
        /// <param name="text">The text to validate.</param>
        private void CheckTextValid(string text)
        {
            if (text.Length <= 140)
                Text = ConvertAbbreviations(text, _words); // Wait to validate length first, then convert abbreviations.
            else throw new ArgumentException("This text is not valid, length must be 1 - 140 [inclusive] characters!");
        }

    }
}
