using ConfigurationManager;
using System;
using System.IO;

namespace FileManager
{
    public class FileTransferOptions : Options
    {
        public TrackerOptions trackerOptions { get; set; }
        public ArchiverOptions archiverOptions { get; set; }
        public LoggerOptions loggerOptions { get; set; }
        public EncryptorOptions encryptorOptions { get; set; }
        public string TargetDirectory { get; set; }

        public FileTransferOptions()
        {
            string td = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TargetDirectory");

            if (!Directory.Exists(td)) Directory.CreateDirectory(td);

            TargetDirectory = td;

            trackerOptions = new TrackerOptions();
            archiverOptions = new ArchiverOptions();
            loggerOptions = new LoggerOptions();
            encryptorOptions = new EncryptorOptions();
        }
    }
}
