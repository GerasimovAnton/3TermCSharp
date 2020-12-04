using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsService1
{
    class EtlOptions : Options
    {
        public TrackerOptions trackerOptions { get; set; }
        public ArchiverOptions archiverOptions { get; set; }
        public LoggerOptions loggerOptions { get; set; }
        public EncryptorOptions encryptorOptions { get; set; }

        public string TargetDirectory { get; set; }
    }
}
