using System;
using System.Text.RegularExpressions;
using BusinessLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessLayerTest
{
    [TestClass]
    public class SignificantIncidentReportTest
    {
        private SignificantIncidentReport _sir = new SignificantIncidentReport("40345422@live.napier.ac.uk", "SIR - 26/11/2008", "E321467812", "Sort Code: 99-12-34 Nature of Incident: Theft blah blah blah https://www.google.com/jeff_bezos");

        [TestMethod]
        public void TestValidate()
        {
            Assert.IsTrue(_sir.Validate(_sir.Sender, _sir.Subject, _sir.Text));
        }

        [TestMethod]
        public void TestHeaderLength()
        {
            Assert.IsTrue(_sir.Header.Length == 10);
        }

        [TestMethod]
        public void TestHeaderStart()
        {
            Assert.IsTrue(_sir.Header.StartsWith("E"));
        }

        [TestMethod]
        public void TestSender()
        {
            Assert.IsTrue(Regex.IsMatch(_sir.Sender, @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$"));
        }

        [TestMethod]
        public void TestSubjectLength()
        {
            Assert.IsTrue(_sir.Subject.Length <= 20);
        }

        [TestMethod]
        public void TestSubjectValid()
        {
            Assert.IsTrue(_sir.Subject.StartsWith("SIR"));
        }

        [TestMethod]
        public void TestTextLength()
        {
            Assert.IsTrue(_sir.Text.Length <= 1024);
        }

        [TestMethod]
        public void TestHeaderErrors()
        {
            try
            {
                SignificantIncidentReport sir = new SignificantIncidentReport(_sir.Sender, _sir.Subject, _sir.Header, _sir.Text);
                Assert.ThrowsException<ArgumentException>(() => sir = new SignificantIncidentReport("_sir.Sender", _sir.Subject, _sir.Header, _sir.Text));
            }
            catch (ArgumentException e)
            {
                Assert.Fail("The ArgumentException was not thrown.\n\n" + e.Message);
            }
        }

        [TestMethod]
        public void TestSenderErrors()
        {
            SignificantIncidentReport sir = new SignificantIncidentReport(_sir.Sender, _sir.Subject, _sir.Header, _sir.Text);
            Assert.ThrowsException<ArgumentException>(() => sir = new SignificantIncidentReport(_sir.Sender, "AHHHHHHHH", _sir.Header, _sir.Text));
        }

        [TestMethod]
        public void TestSubjectErrors()
        {
            SignificantIncidentReport sir = new SignificantIncidentReport(_sir.Sender, _sir.Subject, _sir.Header, _sir.Text);
            Assert.ThrowsException<ArgumentException>(() => sir = new SignificantIncidentReport(_sir.Sender, _sir.Subject, "_sir.Header", _sir.Text));
        }

        [TestMethod]
        public void TestTextErrors()
        {
            string msg = "a";
            for (int i = 0; i < 729; i++)
            {
                msg += "aaaaaaaa";
            }

            SignificantIncidentReport sir = new SignificantIncidentReport(_sir.Sender, _sir.Subject, _sir.Header, _sir.Text);
            Assert.ThrowsException<ArgumentException>(() => sir = new SignificantIncidentReport(_sir.Sender, _sir.Subject, _sir.Header, msg));
        }

        [TestMethod]
        public void TestCodeNotNull()
        {
            Assert.IsNotNull(_sir.Code);
        }

        [TestMethod]
        public void TestNatureNotNull()
        {
            Assert.IsNotNull(_sir.Nature);
        }

        [TestMethod]
        public void TestNatureValid()
        {
            Assert.IsTrue(_sir.Nature.Length > 3);
        }

        [TestMethod]
        public void TestCodeValid()
        {
            Assert.IsTrue(_sir.Code.Length == 8);
        }

        [TestMethod]
        public void TestCodeException()
        {
            Assert.ThrowsException<ArgumentException>(() =>
                _sir = new SignificantIncidentReport("40345422@live.napier.ac.uk", "SIR - 26/11/2008", "E326467812", 
                "Sort Code: 99-1-34 Nature of Incident: Theft blah blah blah https://www.google.com/jeff_bezos"));
        }

        [TestMethod]
        public void TestNatureException()
        {
            Assert.ThrowsException<ArgumentException>(() =>
                _sir = new SignificantIncidentReport("40345422@live.napier.ac.uk", "SIR - 26/11/2008", "E321467852",
                    "Sort Code: 99-13-34 Nature of Incident: blah blah blah https://www.google.com/jeff_bezos"));
        }
    }
}
