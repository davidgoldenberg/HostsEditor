using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hosts.Core.Test
{
    using NUnit.Framework;

    [TestFixture]
    public class CommentEntryFixture
    {
        [Test, ExpectedException(typeof (ArgumentException))]
        public void InvalidCommentYieldsException()
        {
            var commentEntry = new CommentEntry("not a comment");
        }

        [Test]
        public void ExposedCommentRemovesLeadingHash()
        {
            var commentEntry = new CommentEntry("# a comment");
            Assert.AreEqual(" a comment", commentEntry.Comment);
        }

        [Test]
        public void RevisedCommentPrependsHash()
        {
            var commentEntry = new CommentEntry("#comment");
            commentEntry.Comment = "some other comment";
            Assert.AreEqual("#some other comment", commentEntry.ToString());
        }
    }
}
