using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Order
    {
        public int SalesOrderID { get; set; }
        public Product product { get; set; }
        public Customer customer { get; set; }
        public Address ShipToAddress { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public string SalesOrderNumber { get; set; }
        public decimal SubTotal { get; set; }
    }
}
