using System;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessLayer;

namespace BusinessLayerTest
{
    [TestClass]
    public class EmailTest
    {
        private readonly Email _e = new Email("E467280192", "40345422@live.napier.ac.uk", "LessThan20", "A long time ago in a galaxy far far away...");

        [TestMethod]
        public void TestValidate()
        {
            Assert.IsTrue(_e.Validate(_e.Sender, _e.Subject, _e.Text));
        }

        [TestMethod]
        public void TestHeaderLength()
        {
            Assert.IsTrue(_e.Header.Length == 10);
        }

        [TestMethod]
        public void TestHeaderStart()
        {
            Assert.IsTrue(_e.Header.StartsWith("E"));
        }

        [TestMethod]
        public void TestSender()
        {
            Assert.IsTrue(Regex.IsMatch(_e.Sender, @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$"));
        }

        [TestMethod]
        public void TestSubjectLength()
        {
            Assert.IsTrue(_e.Subject.Length <= 20);
        }

        [TestMethod]
        public void TestTextLength()
        {
            Assert.IsTrue(_e.Text.Length <= 1024);
        }



    }
}
