using ConfigurationManager;

namespace FileManager
{
    public class LoggerOptions : Options
    {
        public bool EnableLogging { get; set; } = true;
        public string LogPath { get; set; } = null;
    }
}
