using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace BusinessLayer
{
    public class SignificantIncidentReport : Email
    {
        private string _nature;

        private readonly List<string> _incidents = new List<string>
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

        public string Code { get; internal set; }

        public string Nature
        {
            get => _nature;
            set
            {
                if (_incidents.Contains(value)) _nature = value.Trim();
                else throw new ArgumentException("No incident detected in email!");
            }
        }

        public SignificantIncidentReport(string sender, string subject, string header, string body) : base (header, sender, subject, body)
        {
            string[] data = body.Split(' ');
            Code = data[2].Trim();
            Nature = data[6].Trim();
        }
    }
}
