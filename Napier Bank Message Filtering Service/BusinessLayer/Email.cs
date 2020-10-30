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

        public Email(string sender, string subject, string text)
        {
            if (Validate(sender, subject, text))
            {
                Sender = sender;
                Subject = subject;
                Text = text;
            }
        }

        private bool Validate(string sender, string subject, string text) =>
            (Regex.IsMatch(sender, @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$"))
            && (subject.Length <= 20)
            && (text.Length <= 1028);
    }
}
