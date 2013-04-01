namespace Hosts.Core.Test
{
    using System;
    using NUnit.Framework;
    using System.Text.RegularExpressions;

    [TestFixture]
    public class HostNameFixture
    {
        [Test, ExpectedException(typeof(ArgumentException))]
        public void EmptyThrowsException()
        {
            var hostName = new HostName(string.Empty);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void NullThrowsException()
        {
            var hostName = new HostName(null);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void WhitespaceThrowsException([Values(" ", "\t")] string whiteSpace)
        {
            var hostName = new HostName(whiteSpace);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void InvalidCharactersThrowException([Values("bad&name", "bad#name", "bad_name")] string badHostName)
        {
            var hostName = new HostName(badHostName);
        }

        [Test, ExpectedException(typeof (ArgumentException))]
        public void HostNameTooLong()
        {
            var hostName = new HostName("123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890");
        }

        [Test, ExpectedException(typeof (ArgumentException))]
        public void NameSegmentTooLong()
        {
            var hostName = new HostName("1234567890123456789012345678901234567890123456789012345678901234567890.1234567890");
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void BadNameEndsWithPeriod()
        {
            var hostName = new HostName("bad.name.");
        }

        [Test]
        public void SingleHostNameSegment()
        {
            const string host = "host";
            var hostName = new HostName(host);
            Assert.AreEqual(host, hostName.ToString());
        }

        [Test]
        public void MultipleHostNameSegment()
        {
            const string host = "host.name";
            var hostName = new HostName(host);
            Assert.AreEqual(host, hostName.ToString());
        }

        private const string validCharacters = @"[a-zA-Z0-9\-]";

        [Test]
        public void ValidCharacter()
        {
            Assert.IsTrue(Regex.IsMatch("name", validCharacters));
        }

        [Test]
        public void InvalidCharacter1()
        {
            Assert.IsFalse(Regex.IsMatch("_", string.Format(@"^{0}+$", validCharacters)));
        }

        [Test]
        public void InvalidCharacter2()
        {
            Assert.IsFalse(Regex.IsMatch(".", validCharacters));
        }

        private static string validName = string.Format(@"{0}+", validCharacters);
        private static string validNameAdapter = string.Format(@"^{0}$", validName); //need to make sure regex fails the entire string
        [Test]
        public void ValidName()
        {
            Assert.IsTrue(Regex.IsMatch("name", validNameAdapter));
        }

        [Test]
        public void ValidWithDash()
        {
            Assert.IsTrue(Regex.IsMatch("some-name", validNameAdapter));
        }

        [Test]
        public void InvalidName1()
        {
            Assert.IsFalse(Regex.IsMatch("bad#name", validNameAdapter));
        }

        [Test]
        public void InvalidName2()
        {
            Assert.IsFalse(Regex.IsMatch("badname_", validNameAdapter));
        }

        private static string validSegment = string.Format(@"{0}\.?", validName);
        private static string validSegmentAdapter = string.Format(@"^{0}$", validSegment);

        [Test]
        public void ValidSegment1()
        {
            Assert.IsTrue(Regex.IsMatch("name", validSegmentAdapter));
        }

        [Test]
        public void ValidSegment2()
        {
            Assert.IsTrue(Regex.IsMatch("name.", validSegmentAdapter));
        }

        [Test]
        public void InvalidSegment()
        {
            Assert.IsFalse(Regex.IsMatch(".name", validSegmentAdapter));
        }

        private static string validHost = string.Format(@"^({0})*{1}$", validSegment, validName);

        [Test]
        public void ValidHost()
        {
            Assert.IsTrue(Regex.IsMatch("valid.name", validHost));
        }

        [Test]
        public void InvalidHost()
        {
            Assert.IsFalse(Regex.IsMatch("_invalid.name", validHost));
        }
    }
}
