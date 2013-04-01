namespace Hosts.Core
{
    using System;
    using System.Text.RegularExpressions;

    public class HostName
    {
        private string name;

        public HostName(string hostName)
        {
            char[] nameSeparator = new char[] {'.'};
            if (string.IsNullOrWhiteSpace(hostName)) throw new ArgumentException("null/empty/whitespace");
            if (hostName.Length > 255) throw new ArgumentException("hostname lengh is greater than 255 characters");
            foreach (var nameSegment in hostName.Split(nameSeparator))
                if (nameSegment.Length > 63) throw new ArgumentException("names cannot be longer than 63 characters");

            string validCharacters = @"[a-zA-Z0-9\-]";
            string validName = string.Format(@"{0}+", validCharacters);
            string validSegment = string.Format(@"{0}\.?", validName);
            string validHost = string.Format(@"^({0})*{1}$", validSegment, validName);
            if (!Regex.IsMatch(hostName, validHost))
                throw new ArgumentException("invalid host name format");

            name = hostName;
        }

        public override string ToString()
        {
            return name;
        }

    }
}
