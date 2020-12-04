using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsService1
{
    class LoggerOptions : Options
    {
        public bool EnableLogging { get; set; } = true;
        public string LogPath { get; set; } = null;
    }
}
