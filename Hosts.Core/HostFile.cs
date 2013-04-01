using System;
using System.Collections.Generic;
using System.IO;

namespace Hosts.Core
{
    public class HostFile
    {
        private IList<object> entries  = new List<object>();
        public IList<object> Entries  
        {
            get { return entries; }
        }

        public string HostPath { get; set; }

        public HostFile(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                var winDir = Environment.GetEnvironmentVariable("windir");
                path = Path.Combine(winDir, @"system32\drivers\etc\hosts");
            }
            
            if (!File.Exists(path))
                throw new ArgumentException(string.Format("Could not find the hosts file located at \"{0}\"", path));

            HostPath = path;
            var stream = File.Open(path, FileMode.Open, FileAccess.ReadWrite);
            var reader = new StreamReader(stream);

            while (reader.Peek() > -1)
            {
                var line = reader.ReadLine();

                if (string.IsNullOrWhiteSpace(line))
                {
                    entries.Add(line);
                    continue;
                }

                if (CommentEntry.IsComment(line))
                {
                    entries.Add(new CommentEntry(line));
                    continue;
                }

                entries.Add(new HostEntry(line));
            }
        }
    }
}
