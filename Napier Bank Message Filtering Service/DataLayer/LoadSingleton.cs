using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DataLayer
{
    /// <summary>
    /// This is the class responsible for loading all the data from the files to create new objects.
    /// This was achieved through using this class as a singleton, and then referencing a factory which creates all the objects.
    /// (See PeopleFactory for more details).
    /// 
    /// This class makes use of the sealed modifier to prevent all classes from inheriting from it.
    /// </summary>
    public sealed class LoadSingleton
    {
        /// <summary>
        /// Private constructor -- Essential for a Singleton class as you shouldn't be able to create a new load object.
        /// </summary>
        private LoadSingleton() { }

        /// <summary>
        /// This is a really cool way of deferring the task of loading data which can be resource-intensive, which may not even occur in the program.
        /// Thus, the Lazy<T> waits for you to call an instance of the class before doing anything, to preventresources being used.
        /// 
        /// There's also the use of a 'static' and 'readonly' modifier. They're there so you can't modify it and so you can call a pre-made instance of this class
        /// without creating a new instance of this class.
        /// </summary>
        private static readonly Lazy<LoadSingleton> loadSingleton = new Lazy<LoadSingleton>(() => new LoadSingleton());

        /// <summary>
        /// This is the instance which is called to refer to this class and makes use of the Lazy<T>'s created instance.
        /// This makes use of the static modifier so you can call it from outwith the class.
        /// </summary>
        public static LoadSingleton Instance => loadSingleton.Value;

        public Dictionary<string, string> GetAbbreviations()
        {
            Dictionary<string, string> words = new Dictionary<string, string>();

            string[] lines = File.ReadAllLines("../../textwords.csv");

            foreach (string s in lines)
            {
                string[] word = s.Split(',');
                if (word.Length == 2)
                {
                    words.Add(word[0], word[1]);
                } else if (word.Length > 2)
                {
                    StringBuilder sb = new StringBuilder();
                    for (int i = 1; i < word.Length; i++)
                    {
                        sb.Append(word[i]);
                    }
                    words.Add(word[0], sb.ToString().Trim());
                }
            }

            return words;
        }
    }
}
