namespace Hosts.Core.Test
{
    using System;
    using NUnit.Framework;

    [TestFixture]
    public class HostEntryFixture
    {
        private const string commentLine = "#this is a comment line";

        [Test, ExpectedException(typeof(ArgumentException))]
        public void InvalidHostEntry()
        {
            var hostEntry = new HostEntry(commentLine);
        }

        [Test, ExpectedException(typeof (ArgumentException))]
        public void NullYieldsException()
        {
            var hostEntry = new HostEntry(null);
        }

        [Test, ExpectedException(typeof (ArgumentException))]
        public void EmptyYieldsException()
        {
            var hostEntry = new HostEntry(string.Empty);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void WhitespaceYieldsException()
        {
            var hostEntry = new HostEntry("  ");
        }

        [Test, ExpectedException(typeof (ArgumentException))]
        public void CommentLineYieldsException()
        {
            var hostEntry = new HostEntry("# comment here");
        }

        [Test, ExpectedException(typeof(FormatException))]
        public void InvalidIpYieldsException()
        {
            var hostEntry = new HostEntry("999.123.456.111 hostname");
        }

        [Test, ExpectedException(typeof(Exception))]
        public void NoHostNamesYieldException()
        {
            var hostEntry = new HostEntry("192.168.1.1");
        }

        [Test]
        public void ValidYeildsHostEntry()
        {
            var hostEntry = new HostEntry("192.168.1.1 localhost");
            Assert.AreEqual(1, hostEntry.HostNames.Count);
        }

        [Test]
        public void AddingHostFormatsToStringCorrectly()
        {
            var hostEntry = new HostEntry("192.168.1.1 localhost dgoldenberg-pc");
            hostEntry.HostNames.Add(new HostName("somehost"));
            Assert.AreEqual("192.168.1.1\tlocalhost dgoldenberg-pc somehost", hostEntry.ToString());
        }
    }
}
