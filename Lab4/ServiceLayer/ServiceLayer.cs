using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using ConfigurationManager;
using System.Xml.Serialization;
using System.IO;
using Models;

namespace ServiceLayer
{
    public class ServiceLayer
    {
        DataAccessLayer DAL;

        public ServiceLayer(ConnectionOptions options)
        {
            DAL = new DataAccessLayer(options);
        }

        public Order GetOrder(int OrderID)
        {
            return DAL.GetOrder(OrderID);
        }

        public void CreateXMLFile(Order order, string path)
        {
            XMLGenerator generator = new XMLGenerator();

            string x = generator.ToXML(order);
            using (var streamWriter = new StreamWriter(path)) streamWriter.Write(x);
        }

        public void CreateXSDFile(Order order, string path)
        {
            XMLGenerator generator = new XMLGenerator();
            using (var streamWriter = new StreamWriter(path)) streamWriter.Write(generator.ToXSD(order));
        } 
    }
}
