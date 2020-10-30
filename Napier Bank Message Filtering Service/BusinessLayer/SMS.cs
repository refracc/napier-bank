using DataLayer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;

namespace BusinessLayer
{
    public class SMS : Message
    {
        private static readonly LoadSingleton _ls = LoadSingleton.Instance;

        private readonly Dictionary<string, string> _words = _ls.GetAbbreviations();

        private string _number;

        public string Number
        {
            get => _number;
            set
            {
                if (value.StartsWith("+") && value.Length > 6)
                    _number = value;
                else throw new ArgumentException("This is an invalid phone number!");
            }
        }

        private SMS() { }
        public SMS(string number, string header, string body)
        {
            Header = header;
            Number = number;

            CheckTextValid(body);
        }

        private void CheckTextValid(string text) 
        {
            if (text.Length <= 140)
                Text = ConvertAbbreviations(text, _words);
            else throw new ArgumentException("This text is not valid, length must be 1 - 140 [inclusive] characters!");
        }

    }
}
