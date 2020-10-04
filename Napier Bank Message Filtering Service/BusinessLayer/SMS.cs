using DataLayer;
using System;
using System.Collections.Generic;

namespace BusinessLayer
{
    public class SMS : Message
    {
        private static readonly LoadSingleton ls = LoadSingleton.Instance;

        private readonly Dictionary<string, string> words = ls.GetAbbreviations();

        private SMS() { }
        public SMS(string header, string body)
        {
            Header = header;
            Body = body;

            Body = ConvertAbbreviations(Body, words);

            if (Body.Length > 140)
            {
                throw new Exception("The length of the body cannot exceed 140 characters!");
            }

        }
    }
}
