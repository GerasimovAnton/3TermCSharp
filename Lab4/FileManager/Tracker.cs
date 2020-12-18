using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
    class Tracker : FileSystemWatcher
    {

        public Tracker(string path) : base(path)
        {
            NotifyFilter = NotifyFilters.LastWrite
                          | NotifyFilters.LastAccess
                          | NotifyFilters.FileName
                          | NotifyFilters.DirectoryName;

            Filter = "*.*";
        }

        public Tracker(TrackerOptions options) : base(options.TrackedDirectory)
        {
            /*
            NotifyFilter = NotifyFilters.LastWrite
                          | NotifyFilters.LastAccess
                          | NotifyFilters.FileName
                          | NotifyFilters.DirectoryName;
                          */
            NotifyFilter = options.notifyFilters;
            Filter = options.Filter;
        }


        public void Start()
        {
            EnableRaisingEvents = true;
        }

        public void Stop()

        {
            EnableRaisingEvents = false;
        }
    }
}
