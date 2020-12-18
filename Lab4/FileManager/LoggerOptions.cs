using ConfigurationManager;
using System;
using System.IO;

namespace FileManager
{
    public class LoggerOptions : Options
    {
        public bool EnableLogging { get; set; } = true;
        public string LogPath { get; set; }

        public LoggerOptions()
        {
            LogPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log.txt");
        }
    }
}
