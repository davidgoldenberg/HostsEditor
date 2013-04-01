using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hosts.Core
{
    public class CommentEntry
    {
        private string comment;

        public string Comment { get { return comment; } set { if (value != null) comment = value; } }

        public CommentEntry(string entryLine)
        {
            if (string.IsNullOrWhiteSpace(entryLine)) throw new ArgumentException("commentLine");
            if (!IsComment(entryLine)) throw new ArgumentException("commentLine is not a comment");
            Comment = entryLine.Substring(1);
        }

        public override string ToString()
        {
            return string.Format("#{0}", Comment);
        }

        public static bool IsComment(string entryLine)
        {
            return entryLine.StartsWith("#");
        }
    }
}
