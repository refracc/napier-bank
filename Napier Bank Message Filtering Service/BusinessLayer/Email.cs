using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace BusinessLayer
{
    /// <summary>
    /// This class is to represent the Email message type.
    /// </summary>
    [Serializable]
    public class Email : Message
    {
        private Email() { }
        private string _subject;

        /// <summary>
        /// The subject of the email
        /// </summary>
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

        /// <summary>
        /// The constructor for the Email class.
        /// This is the only way to create a valid email.
        /// </summary>
        /// <param name="header">The header. S, E or T followed by 9 numbers.</param>
        /// <param name="sender">The sender. In the form of a valid email address.</param>
        /// <param name="subject">The subject, must be less than 20 characters.</param>
        /// <param name="text">The body of the email. Must 1024 characters or less.</param>
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

        /// <summary>
        /// This function validates the email data is actually.. well..... valid.
        /// </summary>
        /// <param name="sender">The email sender</param>
        /// <param name="subject">The email subject</param>
        /// <param name="text">The email text</param>
        /// <returns></returns>
        public bool Validate(string sender, string subject, string text) =>
            (Regex.IsMatch(sender, @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$")) // regex for email message
            && (subject.Length <= 20)
            && (text.Length <= 1028);
    }
}
