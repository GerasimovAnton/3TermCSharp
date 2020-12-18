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
using System.ServiceProcess;
using FileManager;

namespace DataManagerForm
{
    public partial class Form1 : Form
    {
        Order order;
        ServiceLayer.ServiceLayer serviceLayer;
        ConfigurationManager.ConfigurationManager configurationManager;

        public Form1()
        {

            /*
            ServiceBase [] ServicesToRun;

            ServicesToRun = new ServiceBase[]
            {
                new FileManager.IFileTransferService()
            };

            ServiceBase.Run(ServicesToRun);*/

            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        { 
            configurationManager = new ConfigurationManager.ConfigurationManager();
            configurationManager.LoadOptions<DataOptions>(AppDomain.CurrentDomain.BaseDirectory);

            DataOptions options = configurationManager.GetOptions<DataOptions>() as DataOptions;

            options.TargetDirectory = @"C:\Users\Anton\source\repos\ISP_Lab_4\DataManager\bin\Debug\SourceDirectory";
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

            serviceLayer.CreateXMLFile(order, $"{numericUpDown.Value}.xml");
            serviceLayer.CreateXSDFile(order, $"{numericUpDown.Value}.xsd");


        }

        private void label_Click(object sender, EventArgs e)
        {

        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            var l = serviceLayer.GetAllOrders();

            listBox1.Items.Clear();
            listBox1.Update();

            foreach (var o in l) 
            {
                listBox1.Items.Add(o);
            }
        }

        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {
            numericUpDown.Value = (int)listBox1.SelectedItem;
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            order = serviceLayer.GetOrder((int)listBox1.SelectedItem);
            XMLGenerator generator = new XMLGenerator();
            textBox.Text = generator.ToXML(order);
        }

        private void numericUpDown_ValueChanged(object sender, EventArgs e)
        {
           
        }

        private void numericUpDown_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;

            var l = serviceLayer.GetAllOrders();

            listBox1.Items.Clear();
            textBox.Clear();

            order = null;

            if (l.Contains((int)numericUpDown.Value))
            {
                listBox1.Items.Add((int)numericUpDown.Value);
            }

            listBox1.Update();

        }
    }
}
