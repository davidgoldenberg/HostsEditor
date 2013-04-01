namespace HostsEditor
{
    using System;
    using Hosts.Core;

    class Program
    {
        static void Main(string[] args)
        {
            HostFile hostFile;
            var actionSeparator = new char[] {':'};
            try
            {
                hostFile = new HostFile(null);
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Failed to open the hosts file, insufficient permissions.");
                return;
            }

            if (args.Length == 0) foreach (var entry in hostFile.Entries)
                Console.WriteLine(entry);

            foreach (var arg in args)
            {
                if (arg.StartsWith("-"))
                {
                    var keyValue = arg.Substring(1).Split(actionSeparator);
                    var key = keyValue[0];
                    switch (key)
                    {
                        case "list":
                            foreach (var entry in hostFile.Entries)
                            {
                                if (entry.GetType() == typeof (HostEntry))
                                {
                                    var hostEntry = (HostEntry) entry;
                                    if (keyValue.Length > 1)
                                    {
                                        if (keyValue[1].Equals(hostEntry.Address.ToString()))
                                        {
                                            Console.WriteLine(hostEntry);
                                        }
                                    }
                                    else
                                        Console.WriteLine(hostEntry);
                                }
                            }
                            break;
                        case "add":
                            break;
                        case "del":
                            break;
                        case "print":
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}
