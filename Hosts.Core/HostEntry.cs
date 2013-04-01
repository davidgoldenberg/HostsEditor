using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hosts.Core
{
    using System.Net;

    public class HostEntry
    {
        private IList<HostName> hostNames = new List<HostName>();
        public IList<HostName> HostNames
        {
            get { return hostNames; }
        }

        private char[] whiteSpace = new char[]{ ' ', '\t' };

        public IPAddress Address { get; set; }

        public HostEntry(string entryLine)
        {
            if (string.IsNullOrWhiteSpace(entryLine)) throw new ArgumentException("entryLine null/empty/whitespace");
            if (entryLine.StartsWith("#")) throw new ArgumentException("entry is a comment");
            var entries = entryLine.Split(whiteSpace);
            
            foreach (var entry in entries)
            {
                if (string.IsNullOrWhiteSpace(entry))
                    continue;

                if (Address == null)
                    Address = IPAddress.Parse(entry);
                else
                    HostNames.Add(new HostName(entry));
            }

            if (Address == null || HostNames.Count == 0)
                throw new Exception("Entry line did not contain valid input, please check the source file.");
        }

        public HostEntry(string address, IEnumerable<string> hostNames)
        {
            if (address == null || hostNames == null) throw new ArgumentNullException();
            Address = IPAddress.Parse(address);
            foreach (var hostName in hostNames)
            {
                if (hostName == null) throw new ArgumentNullException("hostName in hostNames");
                HostNames.Add(new HostName(hostName));
            }
        }

        public override string ToString()
        {
            return string.Format("{0}\t{1}", Address, string.Join(" ", HostNames));
        }
    }
}
