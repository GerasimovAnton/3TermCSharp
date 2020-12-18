using System.IO.Compression;
using ConfigurationManager;

namespace FileManager
{
    public class ArchiverOptions : Options
    {
        public CompressionLevel compressionLevel { get; set; } = CompressionLevel.Optimal;
    }
}
