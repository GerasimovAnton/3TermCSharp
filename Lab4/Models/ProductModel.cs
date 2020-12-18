using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlTypes;

namespace Models
{
    public class ProductModel
    {
        public int ProductModelID { get; set; }
        public string Name { get; set; }
        public SqlXml CatalogDescription { get; set; }
        public Guid rowguid { get; set; }
        public SqlDateTime ModifiedDate { get; set; }
    }
}
