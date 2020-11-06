using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace BusinessLayer
{
    /// <summary>
    /// This is the class responsible for processing SIRs
    /// </summary>
    public class SignificantIncidentReport : Email
    {
        private string _nature;
        private string _code;

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

        /// <summary>
        /// The report's sort code.
        /// </summary>
        public string Code
        {
            get => _code;
            set
            {
                if (value.Length == 8)
                {
                    _code = value;
                } else throw new ArgumentException("Code is of invalid length!");
            }
        }

        /// <summary>
        /// The nature of the incident.
        /// </summary>
        public string Nature
        {
            get => _nature;
            set
            {
                if (_incidents.Contains(value)) _nature = value.Trim();
                else throw new ArgumentException("No incident detected in email!");
            }
        }

        /// <summary>
        /// The only way to create a valid SIR.
        /// All parameters get passed back to base constructor for validation.
        /// </summary>
        /// <param name="sender">The sender of the report</param>
        /// <param name="subject">The subject (SIR - {DATE})</param>
        /// <param name="header">The header of the report (E + 9 numbers)</param>
        /// <param name="body">The body of the report</param>
        public SignificantIncidentReport(string sender, string subject, string header, string body) : base (header, sender, subject, body)
        {
            string[] data = body.Split(' ');
            Code = data[2].Trim(); // Assume
            Nature = data[6].Trim(); // Assume

            if (subject.StartsWith("SIR"))
            {
                Subject = subject;
            }
            else
            {
                throw new ArgumentException("Subject is invalid.");
            }
        }
    }
}
