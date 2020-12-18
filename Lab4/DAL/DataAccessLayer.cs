using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Data.SqlClient;
using Models;
using System.Data;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

namespace DAL
{
    public class DataAccessLayer
    {
        SqlConnection connection;
        public DataAccessLayer(ConnectionOptions options)
        {
            using (var ts = new TransactionScope())
            {
                connection = new SqlConnection($"Data Source={options.DataSource}; Database={options.Database}; User ID ={options.User}; Password={options.Password}; Integrated Security={options.IntegratedSecurity}");
                connection.Open();
                ts.Complete();
            }
        }

        public Order GetOrder(int ID)
        {
            Order order = new Order();

            using (var ts = new TransactionScope())
            {
                using (SqlCommand cmd = new SqlCommand("GetOrder", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter param = new SqlParameter("@OrderID", SqlDbType.Int);
                    param.Value = ID;
                    param.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@ProductID", SqlDbType.Int);
                    param.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(param);


                    var reader = cmd.ExecuteReader();
                    var info = GetInfo(reader);

                    int PID = GetValue<int>(param.Value);

                    if (info.Count != 0)
                        order = new Order()
                        {
                            SalesOrderID = ID,
                            product = GetProduct(PID),
                            customer = GetCustomer((int)info["CustomerID"]),
                            ShipToAddress = GetAddress((int)info["ShipToAddressID"]),
                            PurchaseOrderNumber = GetValue<string>(info["PurchaseOrderNumber"]),
                            SubTotal = (decimal)info["SubTotal"]
                        };

                }

                ts.Complete();
            }

            return order;
        }

        public Address GetAddress(int ID)
        {
            Address address = new Address();

            using (var ts = new TransactionScope())
            {
                using (SqlCommand cmd = new SqlCommand("GetAddress", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter param = new SqlParameter("@AddressID", SqlDbType.Int);
                    param.Value = ID;

                    param.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(param);

                    var reader = cmd.ExecuteReader();
                    var info = GetInfo(reader);

                    if (info.Count != 0)
                        address = new Address(){
                            AddressID = ID,
                            AddressLine1 = info["AddressLine1"] as string,
                            AddressLine2 = GetValue<string>( info["AddressLine2"]),
                            City = info["City"] as string,
                            StateProvince = info["StateProvince"] as string,
                            CountryRegion = info["CountryRegion"] as string,
                            PostalCode = info["PostalCode"] as string,
                            rowguid = (System.Guid)info["rowguid"],
                            ModifiedDate = (DateTime)info["ModifiedDate"]
                    };

                }

                ts.Complete();
            }

            return address;
        }

        public Customer GetCustomer(int ID)
        {
            Customer customer = null;

            using (var ts = new TransactionScope())
            {
                using (SqlCommand cmd = new SqlCommand("GetCustomer", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter param = new SqlParameter("@CustomerID", SqlDbType.Int);
                    param.Value = ID;

                    param.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(param);

                    var reader = cmd.ExecuteReader();
                    var info = GetInfo(reader);

                    if (info.Count != 0)
                        customer = new Customer
                        {
                            CustomerID = ID,
                            NameStyle = (bool)info["NameStyle"],
                            Title = GetValue<string>(info["Title"]),
                            FirstName = (string)info["FirstName"],
                            MiddleName = GetValue<string>(info["MiddleName"]),
                            LastName = (string)info["LastName"],
                            Suffix =  GetValue<string>(info["Suffix"]),
                            CompanyName = GetValue<string>(info["CompanyName"]),
                            SalesPerson = GetValue<string>(info["SalesPerson"]),
                            EmailAddress = GetValue<string>(info["EmailAddress"]),
                            Phone = GetValue<string>(info["Phone"]),
                            PasswordHash = (string)info["PasswordHash"],
                            PasswordSalt = (string)info["PasswordSalt"],

                            rowguid = (System.Guid)info["rowguid"],
                            ModifiedDate = (DateTime)info["ModifiedDate"]
                        };
                }

                ts.Complete();
            }

            return customer;
        }

        public Product GetProduct(int ID)
        {
            Product product  = null;// = new Product();

            using (var ts = new TransactionScope())
            {
                using (SqlCommand cmd = new SqlCommand("GetProduct", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter param = new SqlParameter("@ProductID", SqlDbType.Int);
                    param.Value = ID;

                    param.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(param);

                    var reader = cmd.ExecuteReader();
                    var info = GetInfo(reader);

                    if (info.Count != 0)
                        product = new Product()
                        {
                            Name = info["Name"] as string,
                            ProductNumber = info["ProductNumber"] as string,
                            Color = GetValue<string>(info["Color"]),
                            Size = info["Size"] as string,
                            ThumbnailPhotoFileName = GetValue<string>(info["ThumbnailPhotoFileName"]),
                            ProductID = ID,
                            //ProductCategoryID = GetValue<int>(info["ProductCategoryID"]),
                            ProductModelID = GetValue<int>(info["ProductModelID"]),
                            Weight = GetValue<decimal>(info["Weight"]),
                            ThumbNailPhoto = GetValue<byte[]>(info["ThumbNailPhoto"]),
                            StandardCost = (decimal)info["StandardCost"],
                            ListPrice = (decimal)info["ListPrice"],

                            SellStartDate = (DateTime)info["SellStartDate"],
                            SellEndDate = GetValue<SqlDateTime>(info["SellEndDate"]),
                            DiscontinuedDate = GetValue<SqlDateTime>(info["DiscontinuedDate"]),


                            rowguid = (System.Guid)info["rowguid"],
                            ModifiedDate = (DateTime)info["ModifiedDate"]
                        };
                }

                ts.Complete();
            }

            return product;
        }

        public ProductModel GetProductModel(int ID)
        {
            ProductModel productModel = null;// = new Product();

            using (var ts = new TransactionScope())
            {
                using (SqlCommand cmd = new SqlCommand("GetProductModel", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter param = new SqlParameter("@ProductModelID", SqlDbType.Int);
                    param.Value = ID;

                    param.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(param);

                    var reader = cmd.ExecuteReader();
                    var info = GetInfo(reader);

                    if (info.Count != 0)
                        productModel = new ProductModel()
                        {
                            ProductModelID = ID,
                            Name = info["Name"] as string,
                            CatalogDescription = GetValue<SqlXml>(info["CatalogDescription"]),
                            rowguid = (System.Guid)info["rowguid"],
                            ModifiedDate = (DateTime)info["ModifiedDate"]
                        };
                }

                ts.Complete();
            }

            return productModel;
        }

        public CustomerAddress GetCustomerAddress(int CustomerID,int AddressID)
        {
            CustomerAddress customerAddress = null;// = new Product();

            using (var ts = new TransactionScope())
            {
                using (SqlCommand cmd = new SqlCommand("GetCustomerAddress", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter param = new SqlParameter("@CustomerID", SqlDbType.Int);
                    param.Value = CustomerID;
                    param.Direction = ParameterDirection.Input;

                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@AddressID", SqlDbType.Int);
                    param.Value = AddressID;
                    param.Direction = ParameterDirection.Input;

                    cmd.Parameters.Add(param);

                    var reader = cmd.ExecuteReader();
                    var info = GetInfo(reader);

                    if (info.Count != 0)
                        customerAddress = new CustomerAddress()
                        {
                            CustomerID = CustomerID,
                            AddressID = AddressID,
                            AddressType = info["AddressType"] as string,
                            rowguid = (System.Guid)info["rowguid"],
                            ModifiedDate = (DateTime)info["ModifiedDate"]
                        };
                }

                ts.Complete();
            }

            return customerAddress;
        }

        private static T GetValue<T>(object value)
        {
            if (value == null || value == DBNull.Value)
                return default(T);
            else
                return (T)value;
        }

        private Dictionary<string, object> GetInfo(SqlDataReader reader)
        {
            Dictionary<string, object> info = new Dictionary<string, object>();

            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    info.Add(reader.GetName(i), reader.GetValue(i));
                }
            }

            reader.Close();
            return info;
        }

    }
}
