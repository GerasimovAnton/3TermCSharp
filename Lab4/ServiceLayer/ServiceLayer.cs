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
        DataAccessLayer Log;
        DataOptions dataOptions;

        public ServiceLayer(DataOptions options)
        {
            dataOptions = options;

            DAL = new DataAccessLayer(options.DataAccessOptions);
            Log = new DataAccessLayer(options.LogOptions);

            Log.Log("Service layer successfully created", "Info");

            DAL.GetAllOrderIDs();
        }


        public List<int> GetAllOrders()
        {
            List<int> ids = null;

            try
            {
                ids = DAL.GetAllOrderIDs();
            }
            catch (Exception e)
            {
                Log.Log(e.Message, "Error", "GetAllOrders");
            }

            return ids;
        }

        public Order GetOrder(int OrderID)
        {
            Order order = null;

            try
            {
                order = DAL.GetOrder(OrderID);
            }
            catch (Exception e)
            {
                Log.Log(e.Message, "Error", "GetOrder");
            }

            return order;
        }

        public void CreateXMLFile(Order order, string fileName)
        {
            try
            {
                XMLGenerator generator = new XMLGenerator();

                string x = generator.ToXML(order);
                using (var streamWriter = new StreamWriter(dataOptions.TargetDirectory +"\\"+ fileName)) streamWriter.Write(x);
            }
            catch (Exception e)
            {
                Log.Log(e.Message, "Error", "CreateXMLFile");
            }
        }

        public void CreateXSDFile(Order order, string fileName)
        {
            try
            {
                XMLGenerator generator = new XMLGenerator();
                using (var streamWriter = new StreamWriter(dataOptions.TargetDirectory +"\\"+ fileName)) streamWriter.Write(generator.ToXSD(order));
            }
            catch (Exception e)
            {
                Log.Log(e.Message, "Error", "CreateXSDFile");
            }
        } 
    }
}
