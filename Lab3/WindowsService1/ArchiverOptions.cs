using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsService1
{
    public class ArchiverOptions : Options
    {
        public CompressionLevel compressionLevel { get; set; } = CompressionLevel.Optimal;
    }
}
