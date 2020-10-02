using System;
using System.Collections.Generic;
using DataLayer;

namespace BusinessLayer
{
    public class ServiceFacade
    {
        public static Dictionary<string, string> GetWords()
        {
            LoadSingleton ls = LoadSingleton.Instance;
            return ls.GetAbbreviations();

        }
    }
}
