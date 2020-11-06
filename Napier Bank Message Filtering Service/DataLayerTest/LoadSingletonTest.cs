using System.Collections.Generic;
using System.Linq;
using DataLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataLayerTest
{
    [TestClass]
    public class LoadSingletonTest
    {
        [TestMethod]
        public void TestInstance()
        {
            LoadSingleton ls = LoadSingleton.Instance;
            LoadSingleton ls1 = LoadSingleton.Instance;

            Assert.AreEqual(ls, ls1);
        }

        [TestMethod]
        public void TestData()
        {
            Dictionary<string, string> abbreviations = LoadSingleton.Instance.GetAbbreviations();
            Assert.IsNotNull(abbreviations);
            Assert.IsTrue(abbreviations.Any());
        }
    }
}
