using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;


namespace DataManagerForm
{
    [RunInstaller(true)]
    public partial class FileTransferInstaller : System.Configuration.Install.Installer
    {

        ServiceInstaller serviceInstaller;
        ServiceProcessInstaller processInstaller;

        public FileTransferInstaller()
        {
            InitializeComponent();

            serviceInstaller = new ServiceInstaller();
            processInstaller = new ServiceProcessInstaller();

            processInstaller.Account = ServiceAccount.LocalSystem;
            serviceInstaller.StartType = ServiceStartMode.Manual;
            serviceInstaller.ServiceName = "FILE_TRANSFER_SERVICE";
            Installers.Add(processInstaller);
            Installers.Add(serviceInstaller);
        }
    }
}
