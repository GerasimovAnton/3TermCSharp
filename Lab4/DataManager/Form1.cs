using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Models;
using ServiceLayer;
using DAL;

namespace DataManagerForm
{
    public partial class Form1 : Form
    {
        Order order;
        ServiceLayer.ServiceLayer serviceLayer;
        ConfigurationManager.ConfigurationManager configurationManager;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        { 
            configurationManager = new ConfigurationManager.ConfigurationManager();
            configurationManager.LoadOptions<ConnectionOptions>(AppDomain.CurrentDomain.BaseDirectory);
            ConnectionOptions options = configurationManager.GetOptions<ConnectionOptions>() as ConnectionOptions;
            serviceLayer = new ServiceLayer.ServiceLayer(options);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            order = serviceLayer.GetOrder((int)numericUpDown.Value);
            XMLGenerator generator = new XMLGenerator();
            textBox.Text =  generator.ToXML(order);
        }


        private void button2_Click(object sender, EventArgs e)
        {
            if (order == null) return;

            serviceLayer.CreateXMLFile(order, @"C:\Users\Anton\Downloads\order.xml");
            serviceLayer.CreateXSDFile(order, @"C:\Users\Anton\Downloads\order.xsd");


        }

        private void label_Click(object sender, EventArgs e)
        {

        }
    }
}
