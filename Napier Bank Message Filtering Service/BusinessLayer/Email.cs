using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace BusinessLayer
{
    [Serializable]
    public class Email : Message
    {
        private Email() { }

        public Email(string header, string sender, string subject, string text)
        {
            if (Validate(sender, subject, text))
            {
                Header = header;
                Sender = sender;
                Subject = subject;
                Text = QuarantineURL(text);
            }
            else
            {
                throw new ArgumentException("The data supplied for the email is insufficient.");
            }
        }

        private bool Validate(string sender, string subject, string text) =>
            (Regex.IsMatch(sender, @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$"))
            && (subject.Length <= 20)
            && (text.Length <= 1028);
    }
}
