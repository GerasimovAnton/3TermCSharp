using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WindowsService1
{
    public partial class ETL : ServiceBase
    {
        public ETL()
        {
            CanStop = true;
            CanPauseAndContinue = true;
            AutoLog = true;

            InitializeComponent();
            Init();
        }

        //protected override void OnStart(string[] args);

        //protected override void OnStop();

    }
}
