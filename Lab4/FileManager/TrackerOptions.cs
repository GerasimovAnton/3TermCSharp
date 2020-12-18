using System;
using System.IO;
using ConfigurationManager;

namespace FileManager
{
    public class TrackerOptions : Options
    {
        public string Filter { get; set; } = "*.*";
        public string TrackedDirectory { get; set; }
        public NotifyFilters notifyFilters { get; set; }  = NotifyFilters.LastWrite
                                                          | NotifyFilters.LastAccess
                                                          | NotifyFilters.FileName
                                                          | NotifyFilters.DirectoryName;

        public TrackerOptions()
        {
            string td = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SourceDirectory");

            if (!Directory.Exists(td)) Directory.CreateDirectory(td);

            TrackedDirectory = td;
        }
    }
}
