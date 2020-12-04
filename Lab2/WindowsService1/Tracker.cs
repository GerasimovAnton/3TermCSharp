using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsService1
{
    class Tracker : FileSystemWatcher
    {
        public Tracker(string directory) : base(directory)
        {
            NotifyFilter = NotifyFilters.LastWrite
                          | NotifyFilters.LastAccess
                          | NotifyFilters.FileName
                          | NotifyFilters.DirectoryName;

            Filter = "*.txt";
                                                     
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
