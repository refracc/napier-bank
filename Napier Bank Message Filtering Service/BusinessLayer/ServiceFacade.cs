using System;
using System.Collections.Generic;
using DataLayer;

namespace BusinessLayer
{
    public class ServiceFacade
    {
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

        public SignificantIncidentReport ProcessSIR(string sender, string header, string subject, string text)
        {
            try
            {
                SignificantIncidentReport sir = new SignificantIncidentReport(sender, subject, header, text);
                Save(sir, subject);
                return sir;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private bool Save(Message message, string header)
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
