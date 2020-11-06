using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SQLite;

namespace InventoryControl
{
    class ProductDatabase
    {
        public ProductDatabase()
        {
            
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
                    GetProductNumber(productId),                        //Number of products
                    rdr.IsDBNull(2) ? null : (double?)rdr.GetDouble(2), //Weight
                    Measurement.FromInt(rdr.GetInt32(3)),               //Measurement displaystring
                    rdr.IsDBNull(4) ? null : (double?)rdr.GetDouble(4), //Purchase price
                    rdr.IsDBNull(5) ? null : (double?)rdr.GetDouble(5)  //Sale price
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

        static public SQLiteConnection Connect()
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
            new SQLiteCommand(
            "CREATE TABLE IF NOT EXISTS\"PointsOfSale\"(\"Id\"INTEGER NOT NULL UNIQUE,\"Title\"TEXT NOT NULL UNIQUE,PRIMARY KEY(\"Id\" AUTOINCREMENT));" +
            "CREATE TABLE IF NOT EXISTS\"ProductsDictionary\"(\"Id\"INTEGER NOT NULL UNIQUE, \"Title\"TEXT NOT NULL, \"Weight\"REAL, \"Packing\"INTEGER NOT NULL, \"PurchasePrice\"INTEGER, \"SalePrice\"INTEGER, PRIMARY KEY(\"Id\" AUTOINCREMENT));" +
            "CREATE TABLE IF NOT EXISTS\"Storage_Main\"(\"Id\"INTEGER NOT NULL UNIQUE, \"Number\"INTEGER NOT NULL, PRIMARY KEY(\"Id\" AUTOINCREMENT));", connection).ExecuteScalar();
        }
    }
}
