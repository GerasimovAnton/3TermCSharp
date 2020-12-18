using System;
using System.IO;
using System.Reflection;

namespace FileManager
{
    class Logger
    {
        public bool Enabled { get; set; } = true;
        public string LogPath { get; set; }

        public Logger() 
        {
            LogPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log.txt");
        }

        public Logger(string path)
        {
            LogPath = path;
        }

        public void Log(string message)
        {
            if(Enabled)
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
