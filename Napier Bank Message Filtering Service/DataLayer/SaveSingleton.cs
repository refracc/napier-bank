using System;
using System.IO;
using Newtonsoft.Json;

namespace DataLayer
{
    /// <summary>
    /// This is the class responsible for saving all the data from the system to various CSV files.
    /// This was achieved through using this class as a singleton, which allows only 1 instance of this class to be loaded at any time.
    /// 
    /// This class makes use of the sealed modifier to prevent classes from inheriting from it unless specified.
    /// </summary>
    public sealed class SaveSingleton
    {
        /// <summary>
        /// Private Constructor -- Cannot be instantiated outside this class.
        /// Essential for a Singleton.
        /// </summary>
        private SaveSingleton() { }

        /// <summary>
        /// This is a really cool way of deferring the task of saving data which can be resource-intensive, which may not even occur in the program.
        /// Thus, Lazy<T> waits for you to call an instance of the class before doing anything, to prevent resources being used.
        /// 
        /// There's also the use of a 'static' and 'readonly' modifier. They're there so you can't modify it and so you can call a pre-made instance of this class
        /// without creating a new instance of this class.
        /// </summary>
        private static readonly Lazy<SaveSingleton> saveSingleton = new Lazy<SaveSingleton>(() => new SaveSingleton());

        /// <summary>
        /// A static instance of this class which can be referenced without having to create a new reference to this class.
        /// </summary>
        public static SaveSingleton Instance => saveSingleton.Value;

        /// <summary>
        /// Write the contents of a file to a new JSON file.
        /// </summary>
        /// <param name="o">The data to pass in to the file writer</param>
        /// <param name="header">The actual name of the file</param>
        /// <returns>True: If the file can be created. False: If it encounters some sort of exception.</returns>
        public bool WriteData(object o, string header)
        {
            try
            {
                string json = JsonConvert.SerializeObject(o, Formatting.Indented);
                File.WriteAllText($"../../{header}.json", json);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}