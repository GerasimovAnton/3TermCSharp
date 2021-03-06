﻿using ConfigurationManager;

namespace FileManager
{
    public class EtlOptions : Options
    {
        public TrackerOptions trackerOptions { get; set; }
        public ArchiverOptions archiverOptions { get; set; }
        public LoggerOptions loggerOptions { get; set; }
        public EncryptorOptions encryptorOptions { get; set; }

        public string TargetDirectory { get; set; }
    }
}
