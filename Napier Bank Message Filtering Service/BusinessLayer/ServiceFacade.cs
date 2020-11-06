using System;
using System.Collections.Generic;
using DataLayer;

namespace BusinessLayer
{
    /// <summary>
    /// This class acts as an API, hiding implementation and keeping the main class clean.
    /// </summary>
    public class ServiceFacade
    {
        /// <summary>
        /// This method processes the SMS (text) messages
        /// which get passed through the system.
        /// </summary>
        /// <param name="number">The sender</param>
        /// <param name="header">The header of the message</param>
        /// <param name="body">The body (text) of the message</param>
        /// <returns>An instance of the SMS class to be used.</returns>
        public SMS ProcessSMS(string number, string header, string body)
        {
            try
            {
                SMS sms = new SMS(number, header, body);
                Save(sms, header);
                return sms;
            }
            catch (ArgumentException e)
            {
                throw new ArgumentException(e.Message);
            }
        }

        /// <summary>
        /// Processing the Email messages
        /// which get passed through the system.
        /// </summary>
        /// <param name="sender">The sender (email address)</param>
        /// <param name="header">The header</param>
        /// <param name="subject">The subject of the email</param>
        /// <param name="body">The text of the email</param>
        /// <returns>An instance of the email class to be referred to.</returns>
        public Email ProcessEmail(string sender, string header, string subject, string body)
        {
            try
            {
                Email email = new Email(header, sender, subject, body);
                Save(email, header);
                return email;
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }
        }

        /// <summary>
        /// This method processes tweets
        /// passing through the system.
        /// </summary>
        /// <param name="sender">The handle of the user (@[name])</param>
        /// <param name="header">The header of the message</param>
        /// <param name="body">The actual text in the tweet (old system - 140 chars)</param>
        /// <returns>An instance of the Tweet class to be referred to.</returns>
        public Tweet ProcessTweet(string sender, string header, string body)
        {
            try
            {
                Tweet tweet = new Tweet(sender, header, body);
                Save(tweet, header);
                return tweet;
            }
            catch (ArgumentException e)
            {
                throw new ArgumentException(e.Message);
            }
        }

        /// <summary>
        /// This method processes the SIRs that pass through the system.
        /// Although similar to emails, it's easier to have their own class.
        /// </summary>
        /// <param name="sender">The sender of the report</param>
        /// <param name="header">The header of the report</param>
        /// <param name="subject">The report subject</param>
        /// <param name="text">The body of the email, where the information about the report is contained.</param>
        /// <returns>An instance of the SignificantIncidentReport class to be referred to.</returns>
        public SignificantIncidentReport ProcessSIR(string sender, string header, string subject, string text)
        {
            try
            {
                SignificantIncidentReport sir = new SignificantIncidentReport(sender, subject, header, text);
                Save(sir, header);
                return sir;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// This method is for saving the messages into a file.
        /// </summary>
        /// <param name="message">The message to be saved</param>
        /// <param name="header">The header - which will be used as the file name.json</param>
        /// <returns>True - the file can save. False if something unexpected happens.</returns>
        public bool Save(Message message, string header)
        {
            try
            {
                SaveSingleton ss = SaveSingleton.Instance;
                ss.WriteData(message, header);
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
    }
}
