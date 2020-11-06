using System;
using BusinessLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessLayerTest
{
    [TestClass]
    public class ServiceFacadeTest
    { 
        private readonly ServiceFacade _sf = new ServiceFacade();

        [TestMethod]
        public void TestEmailProcess()
        {
            Assert.IsNotNull(_sf.ProcessEmail("40345422@live.napier.ac.uk", "E426781923", "Subject :D", "This is a default email body :)"));
        }

        [TestMethod]
        public void TestSIRProcess()
        {
            Assert.IsNotNull(_sf.ProcessSIR("40345422@live.napier.ac.uk", "E426715223", "SIR - 01/05/1957", "Sort Code: 99-12-34 Nature of Incident: Theft blah blah blah https://www.google.com/jeff_bezos"));
        }

        [TestMethod]
        public void TestSMSProcess()
        {
            Assert.IsNotNull(_sf.ProcessSMS("+44678276381", "S426781923", "This is a default SMS body :)"));
        }

        [TestMethod]
        public void TestTweetProcess()
        {
            Assert.IsNotNull(_sf.ProcessTweet("@refracc", "T426781923", "OMG this totally works LMAO"));
        }

        [TestMethod]
        public void TestSave()
        {
            Assert.IsNotNull(_sf.Save(_sf.ProcessTweet("@refracc", "T426781923", "OMG this totally works LMAO"), "T426781923"));
        }
    }
}
