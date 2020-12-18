using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigurationManager;

namespace DAL
{
    public class ConnectionOptions : Options
    {
        public string DataSource { get; set; } = "DESKTOP-2M7COLO";
        public string Database { get; set; } = "AdventureWorksLT2019";
        public string User { get; set; } = "Anton";
        public string Password { get; set; } = "12";
        public bool IntegratedSecurity { get; set; } = true;
    }
}
