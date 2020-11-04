using System;
using DataLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataLayerTest
{
    [TestClass]
    public class SaveSingletonTest
    {
        [TestMethod]
        public void TestInstance()
        {
            SaveSingleton save = SaveSingleton.Instance;
            SaveSingleton save1 = SaveSingleton.Instance;
            
            Assert.AreEqual(save, save1);
        }
    }
}
