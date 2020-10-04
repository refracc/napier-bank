using System;

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
        /// Given the type specified, this method will all objects of that type to a JSON file.
        /// </summary>
        /// <param name="type">A string which is used to specify what objects to save.</param>
        public void SaveType(string type)
        {
            switch(type)
            {
                case "sms":
                    break;
                case "email":
                    break;
                case "tweet":
                    break;
                default: 
                    break;
            }
        }
    }
}