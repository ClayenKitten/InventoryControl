using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SQLite;
using System.Web.ClientServices.Providers;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace InventoryControl
{
    static class ProductDatabase
    {
        public static event EventHandler DatabaseChanged;
        private static void OnDatabaseChanged(EventArgs e)
        {
            DatabaseChanged.Invoke(typeof(ProductDatabase), e);
        }
        static public int CreateOrEditProduct(int? id, String title, String weightIn, int measurement, String purchasePriceIn, String salePrice)
        {
            string Format(string value, bool isOptional = false)
            {
                var Value = value.Replace(",", ".");
                if (Double.TryParse(Value, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat, out double Parsed))
                {
                    return Math.Round(Parsed, 2).ToString(CultureInfo.InvariantCulture);
                }
                else
                {
                    if (isOptional)
                        return "null";
                    else
                        throw new ArgumentException();
                }
            }
            //Validate
            if (title == "" || salePrice == "")
                throw new ArgumentException("Necessary string is empty");
            String weight = Format(weightIn, true);
            String purchasePrice = Format(purchasePriceIn, true);
            //Execute
            if (id.HasValue)
                return EditProduct(id.Value, title, weight, measurement, purchasePrice, Format(salePrice));
            else
                return CreateProduct(title, weight, measurement, purchasePrice, Format(salePrice));
        }
        static private int CreateProduct(String title, String weight, int measurement, String purchasePrice, String salePrice)
        {
            var con = Connect();
            new SQLiteCommand(
                "INSERT INTO ProductsDictionary(Title,Weight,Packing,PurchasePrice,SalePrice)" +
                $"VALUES(" +
                $"'{title}'," +
                $"{weight}," +
                $"{measurement}," +
                $"{purchasePrice}," +
                $"{salePrice}" +
                $");",
                con).ExecuteScalar();
            con.Close();
            OnDatabaseChanged(new EventArgs());
            return 0;
        }
        static private int EditProduct(int id, String title, String weight, int measurement, String purchasePrice, String salePrice)
        {
            var con = Connect();
            new SQLiteCommand(
                $"UPDATE ProductsDictionary SET " +

                $"Title='{title}'," +
                $"Weight={weight}," +
                $"Packing={measurement}," +
                $"PurchasePrice={purchasePrice}," +
                $"SalePrice={salePrice} " +
                
                $"WHERE Id={id};",
                con).ExecuteScalar();
            con.Close();
            OnDatabaseChanged(new EventArgs());
            return 0;
        }
        
        static public void AddProductNumber(int productId, int Number)
        {
            int number = GetProductNumber(productId);
            var con = Connect();

            
            new SQLiteCommand(
                $"UPDATE Storage_Main "+
                $"SET Number = Number {(Number<0?"-":"+")} {Math.Abs(Number)} "+
                $"WHERE Id = {productId};",
                con).ExecuteScalar();
            con.Close();
            OnDatabaseChanged(new EventArgs());
        }
        static public int GetProductNumber(int productId)
        {
            var con = Connect();
            var value = new SQLiteCommand("SELECT Number FROM Storage_Main WHERE Id = " + productId, con).ExecuteScalar();
            con.Close();
            if (value != null)
            {
                return (Int32)(Int64)value;
            }
            else
            {
                return 0;
            }
        }

        static public ProductData GetProductData(int productId)
        {
            var con = Connect();

            var rdr = new SQLiteCommand("SELECT * FROM ProductsDictionary WHERE Id = "+productId, con).ExecuteReader(CommandBehavior.CloseConnection);
            if (rdr.Read())
            {

                var productData = new ProductData
                (
                    rdr.GetInt32(0),                                    //UUID of product
                    rdr.GetString(1),                                   //Title of product
                    rdr.IsDBNull(2) ? null : (double?)rdr.GetDouble(2), //Weight
                    rdr.GetInt32(3),                                    //Measurement displaystring
                    rdr.IsDBNull(4) ? null : (double?)rdr.GetDouble(4), //Purchase price
                    rdr.GetDouble(5)                                    //Sale price
                );
                rdr.Close();
                return productData;
            }
            else
            {
                rdr.Close();
                return null;
            }
        }
        static public List<ProductData> GetProductData()
        {
            List<ProductData> output = new List<ProductData>();
            var con = Connect();

            var rdr = new SQLiteCommand("SELECT * FROM ProductsDictionary", con).ExecuteReader(CommandBehavior.CloseConnection);
            while (rdr.Read())
            {
                output.Add(new ProductData
                (
                    rdr.GetInt32(0),                                    //UUID of product
                    rdr.GetString(1),                                   //Title of product
                    rdr.IsDBNull(2) ? null : (double?)rdr.GetDouble(2), //Weight
                    rdr.GetInt32(3),                                    //Measurement displaystring
                    rdr.IsDBNull(4) ? null : (double?)rdr.GetDouble(4), //Purchase price
                    rdr.GetDouble(5)                                    //Sale price
                ));
            }
            rdr.Close();
            return output;
        }
        static public int GetProductsNumber()
        {
            var con = Connect();
            var value = (long)new SQLiteCommand("SELECT COUNT(*) FROM Storage_Main", con).ExecuteScalar();
            con.Close();
            return (int)value;
        }

        static public void AddPointOfSales(String title) 
        {
            var con = Connect(); 
            new SQLiteCommand($"INSERT INTO PointsOfSale(Title) VALUES('{title}');", con).ExecuteScalar();
            con.Close();
            OnDatabaseChanged(new EventArgs());
        }
        static public void DeletePointOfSales(String title)
        {
            var con = Connect();
            new SQLiteCommand($"DELETE FROM PointsOfSale WHERE Title == '{title}';", con).ExecuteScalar();
            con.Close();
            OnDatabaseChanged(new EventArgs());
        }

        static public List<String> GetPointsOfSales()
        {
            var con = Connect();
            var rdr = new SQLiteCommand("SELECT Title FROM PointsOfSale", con).ExecuteReader();
            var itemsSource = new List<String>();
            while (rdr.Read())
            {
                itemsSource.Add(rdr.GetString(0));
            }
            con.Close();
            return itemsSource;
        }
        
        static private SQLiteConnection Connect()
        {
            SQLiteConnectionStringBuilder builder = new SQLiteConnectionStringBuilder();
            builder.FailIfMissing = true;
            builder.DataSource = "Database.db";
            var connection = new SQLiteConnection(builder.ConnectionString);
            try
            {
                connection.Open();
                CreateTables(connection);
                return connection;
            }
            catch(SQLiteException)
            {
                SQLiteConnection.CreateFile("Database.db");
                connection.Open();
                CreateTables(connection);
                return connection;
            }
        }
        static private void CreateTables(SQLiteConnection connection)
        {
            //TODO
        }
    }
}
