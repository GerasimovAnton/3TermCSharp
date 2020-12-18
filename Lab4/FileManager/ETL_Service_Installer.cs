using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;
using System.Threading.Tasks;

namespace FileManager
{
    [RunInstaller(true)]
    public partial class ETL_Service_Installer : Installer
    {
        ServiceInstaller serviceInstaller;
        ServiceProcessInstaller processInstaller;

        public ETL_Service_Installer()
        {
            InitializeComponent();

            serviceInstaller = new ServiceInstaller();
            processInstaller = new ServiceProcessInstaller();

            processInstaller.Account = ServiceAccount.LocalSystem;
            serviceInstaller.StartType = ServiceStartMode.Manual;
            serviceInstaller.ServiceName = "ETL_SERVICE";
            Installers.Add(processInstaller);
            Installers.Add(serviceInstaller);
        }
    }
}
