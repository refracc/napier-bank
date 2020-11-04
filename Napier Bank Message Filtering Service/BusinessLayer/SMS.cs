using DataLayer;
using System;
using System.Collections.Generic;

namespace BusinessLayer
{
    [Serializable]
    public class SMS : Message
    {
        private static readonly LoadSingleton _ls = LoadSingleton.Instance;

        private readonly Dictionary<string, string> _words = _ls.GetAbbreviations();

        private SMS() { }
        public SMS(string number, string header, string body)
        {
            Header = header;
            Sender = number;

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
