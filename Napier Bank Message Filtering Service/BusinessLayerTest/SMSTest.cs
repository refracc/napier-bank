using System;
using BusinessLayer;
using DataLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessLayerTest
{
    [TestClass]
    public class SMSTest
    {
        private SMS sms = new SMS("+23467382", "S467382735", "OMG did you hear that Jeff Bezos DIED? TTYL xx");

        [TestMethod]
        public void TestSenderValid()
        {
            Assert.IsTrue(sms.Sender.StartsWith("+"));
        }

        [TestMethod]
        public void TestSenderLength()
        {
            Assert.IsTrue(sms.Sender.Length > 6);
        }

        [TestMethod]
        public void TestHeaderValid()
        {
            Assert.IsTrue(sms.Header.StartsWith("S"));
        }

        [TestMethod]
        public void TestHeaderLength()
        {
            Assert.IsTrue(sms.Header.Length == 10);
        }

        [TestMethod]
        public void TestHeaderNumber()
        {
            Assert.IsTrue(int.TryParse(sms.Header.Substring(1), out _));
        }

        [TestMethod]
        public void TestTextLength()
        {
            Assert.IsTrue(sms.Text.Length <= 140);
        }

        [TestMethod]
        public void TestTextException()
        {
            string msg = "";

            for (int i = 0; i < 141; i++) msg += "a";

            SMS sms1 = new SMS(sms.Sender, sms.Header, sms.Text);
            Assert.ThrowsException<ArgumentException>(() => sms1 = new SMS(sms.Sender, sms.Header, msg));
        }

        [TestMethod]
        public void TestHeaderException()
        {
            SMS sms1 = new SMS(sms.Sender, sms.Header, sms.Text);
            Assert.ThrowsException<ArgumentException>(() => sms1 = new SMS(sms.Sender, "sms.Header", sms.Text));
        }

        [TestMethod]
        public void TestSenderException()
        {
            SMS sms1 = new SMS(sms.Sender, sms.Header, sms.Text);
            Assert.ThrowsException<ArgumentException>(() => sms1 = new SMS("sms.Sender", sms.Header, sms.Text));
        }

        [TestMethod]
        public void TestConversion()
        {
            LoadSingleton _ls = LoadSingleton.Instance;
            string word = "ROTFL";

            sms.Text = word;
            string convert = sms.ConvertAbbreviations(word, _ls.GetAbbreviations());

            Assert.AreEqual(convert, word + " <" + _ls.GetAbbreviations()[word] + ">");
        }
    }
}
