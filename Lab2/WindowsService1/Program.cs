using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;
using System.Collections.Specialized;

namespace WindowsService1
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;

            var t = ConfigurationSettings.AppSettings.Get("TargetDirectory");
            var s = ConfigurationSettings.AppSettings.Get("SourceDirectory");

            ETL etl = new ETL(s, t);

            ServicesToRun = new ServiceBase[]
            {
                etl
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
