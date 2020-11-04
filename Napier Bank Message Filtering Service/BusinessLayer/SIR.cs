using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace BusinessLayer
{
    public class SignificantIncidentReport : Email
    {
        public SignificantIncidentReport(string sender, string subject, string header, string body) : base (header, sender, subject, body)
        {
            string[] data = body.Split(' ');
            string code = data[1].Trim();

        }

        public string CheckIncidents(string message, string sortCode)
        {
            List<string> incidents = new List<string>
            {
                "Theft",
                "Staff-Attack",
                "ATM-Theft",
                "Raid",
                "Customer-Attack",
                "Staff-Abuse",
                "Bomb-Threat",
                "Terrorism",
                "Suspicious-Incident",
                "Intelligence",
                "Cash-Loss"
            };

            string[] data = message.Split(' ');
            StringBuilder sb = new StringBuilder();
            sb.Append("Sort Code: " + sortCode + "\n");

            foreach (string s in data)
            {
                if (incidents.Contains(s))
                {
                    sb.Append("Incident: " + s);
                } 
            }

            return sb.ToString();
        }
    }
}
