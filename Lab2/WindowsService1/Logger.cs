using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WindowsService1
{
    class Logger
    {
        public string LogPath { get; set; }

        public Logger() 
        {
            LogPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\Log.txt";
        }

        public Logger(string path)
        {
            LogPath = path;
        }

        public void Log(string message)
        {
            using (StreamWriter writer = new StreamWriter(LogPath, true))
            {
                writer.WriteLine($"[{DateTime.Now:hh:mm:ss dd.MM.yyyy}] - {message}");
            }     
        }

        public static void Log(string path,string message)
        {
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine($"[{DateTime.Now:hh:mm:ss dd.MM.yyyy}] - {message}");
            }
        }

    }
}
