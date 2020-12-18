using System;
using System.Data.SqlTypes;

namespace Models
{
    public class CustomerAddress
    {
        public int CustomerID { get; set; }
        public int AddressID { get; set; }
        public string AddressType { get; set; }
        public Guid rowguid { get; set; }
        public SqlDateTime ModifiedDate { get; set; }
    }
}
