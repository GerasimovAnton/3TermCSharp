using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlTypes;

namespace Models
{
    public class Product
    {
        public int ProductID { get; set; }
        //public int ProductCategoryID { get; set; }
        public int ProductModelID { get; set; }

        public string Name { get; set; }
        public string ProductNumber { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public string ThumbnailPhotoFileName { get; set; }
        public decimal Weight { get; set; }

        public SqlBinary ThumbNailPhoto { get; set; }

        public SqlMoney StandardCost { get; set; }
        public SqlMoney ListPrice { get; set; }

        public Guid rowguid { get; set; }

        public SqlDateTime SellStartDate { get; set; }
        public SqlDateTime SellEndDate { get; set; }
        public SqlDateTime DiscontinuedDate { get; set; }
        public SqlDateTime ModifiedDate{ get; set; }
    }
}
