using System;
using BusinessLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessLayerTest
{
    [TestClass]
    public class TweetTest
    {
        private Tweet _tweet = new Tweet("@refracc", "T685496752", "OMG did you hear that @Amazon's #CEO Jeff Bezos has finally kicked the bucket?!");

        [TestMethod]
        public void TestTweetSender()
        {
            Assert.IsTrue(_tweet.Sender.StartsWith("@"));
        }

        [TestMethod]
        public void TestHeaderValid()
        {
            Assert.IsTrue(_tweet.Header.StartsWith("T"));
        }

        [TestMethod]
        public void TestHeaderLength()
        {
            Assert.IsTrue(_tweet.Header.Length == 10);
        }

        [TestMethod]
        public void TestHeaderNumber()
        {
            Assert.IsTrue(int.TryParse(_tweet.Header.Substring(1), out _));
        }

        [TestMethod]
        public void TestMessageValid()
        {
            Assert.IsTrue(_tweet.Text.Length <= 140);
        }

        [TestMethod]
        public void TestHashtagsList()
        {
            Assert.IsNotNull(_tweet.ExtractHashtags(_tweet.Text));
        }

        [TestMethod]
        public void TestTweet()
        {
            Assert.IsNotNull(_tweet);
        }

        [TestMethod]
        public void TestMentionsList()
        {
            Assert.IsNotNull(_tweet.ExtractMentions(_tweet.Text));
        }

        [TestMethod]
        public void TestMentionLength()
        {
            Assert.IsTrue(_tweet.ExtractMentions(_tweet.Text).Count > 0);
        }

        [TestMethod]
        public void TestHashLength()
        {
            Assert.IsTrue(_tweet.ExtractMentions(_tweet.Text).Count > 0);
        }

        [TestMethod]
        public void TestSenderException()
        {
            Tweet t = _tweet;

            Assert.ThrowsException<ArgumentException>(() => t = new Tweet("refracc", _tweet.Header, _tweet.Text));
        }

        [TestMethod]
        public void TestHeaderException()
        {
            Tweet t = _tweet;

            Assert.ThrowsException<ArgumentException>(() => t = new Tweet(_tweet.Sender, "_tweet.Header", _tweet.Text));
        }

        [TestMethod]
        public void TestTextException()
        {
            Tweet t = _tweet;
            string msg = "";

            for (int i = 0; i < 141; i++) msg += "a";

            Assert.ThrowsException<ArgumentException>(() => t = new Tweet(_tweet.Sender, _tweet.Header, msg));
        }
    }
}
