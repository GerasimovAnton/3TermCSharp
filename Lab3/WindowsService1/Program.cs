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


            ServicesToRun = new ServiceBase[]
            {
                new ETL()
            };

            ServiceBase.Run(ServicesToRun);
        }
    }
}
