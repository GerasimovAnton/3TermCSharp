using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsService1
{
    static class Archiver
    {
        public static void Compress(string sourcePath, CompressionLevel compressionLevel = CompressionLevel.Optimal)
        {
            var tp = Path.GetDirectoryName(sourcePath) + Path.GetFileNameWithoutExtension(sourcePath) + ".gz";
            Compress(sourcePath, tp, compressionLevel);
        }

        public static void Compress(string sourcePath, string targetPath, CompressionLevel compressionLevel = CompressionLevel.Optimal)
        {
                using (FileStream fs = new FileStream(sourcePath, FileMode.Open, FileAccess.ReadWrite))
                {
                    using (FileStream nfs = File.Create(targetPath))
                    {
                        using (GZipStream gz = new GZipStream(nfs, compressionLevel))
                        {
                            fs.CopyTo(gz);
                        }
                    }
                }     
        }
   

        public static void Decompress(string sourcePath, string targetPath) 
        {
            using (FileStream fs = new FileStream(sourcePath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                using (FileStream nfs = File.Create(targetPath))
                {
                    using (GZipStream gz = new GZipStream(fs, CompressionMode.Decompress))
                    {
                        gz.CopyTo(nfs);
                    }
                }
            }
        }
    }
}
