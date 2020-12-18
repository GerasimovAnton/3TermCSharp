using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigurationManager;

namespace FileManager
{
    public class TrackerOptions : Options
    {
        public string Filter { get; set; } = "*.txt";
        public string Path { get; set; } = AppDomain.CurrentDomain.BaseDirectory;
        public NotifyFilters notifyFilters { get; set; } = NotifyFilters.LastWrite;
    }
}
