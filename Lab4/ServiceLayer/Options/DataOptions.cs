using System;
using System.IO;
using DAL;
using ConfigurationManager;


namespace ServiceLayer
{
    public class DataOptions : Options
    {
        public string TargetDirectory { get; set; }
        public ConnectionOptions LogOptions { get; set; } 

        public DataOptions()
        {
            string td = AppDomain.CurrentDomain.BaseDirectory + "\\TargetDirectory";

            if (!Directory.Exists(td)) Directory.CreateDirectory(td);

            TargetDirectory = td;

            //---------------------

            LogOptions = new ConnectionOptions();
            LogOptions.Database = "Log";
        }
    }
}
