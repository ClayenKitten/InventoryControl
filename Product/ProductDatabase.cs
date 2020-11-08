﻿using System;
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
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Id of created product</returns>
        static public int CreateProduct(String title, double? weight, int measurement, double? purchasePrice, double? salePrice)
        {
            var con = Connect();
            new SQLiteCommand(
                "INSERT INTO ProductsDictionary(Title,Weight,Packing,PurchasePrice,SalePrice)" +
                $"VALUES('{title}',{weight},{measurement},{purchasePrice},{salePrice});",
                con).ExecuteScalar();
            return 0;
        }
        static public int EditProduct(String title, double? weight, int measurement, double? purchasePrice, double? salePrice)
        {
            var con = Connect();
            new SQLiteCommand(
                $"UPDATE ProductsDictionary SET Title='{title}',Weight={weight}," +
                $"Packing={measurement},PurchasePrice={purchasePrice},SalePrice={salePrice} WHERE Id=24;",
                con).ExecuteScalar();
            return 0;
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
            new SQLiteCommand(
            "CREATE TABLE IF NOT EXISTS\"PointsOfSale\"(\"Id\"INTEGER NOT NULL UNIQUE,\"Title\"TEXT NOT NULL UNIQUE,PRIMARY KEY(\"Id\" AUTOINCREMENT));" +
            "CREATE TABLE IF NOT EXISTS\"ProductsDictionary\"(\"Id\"INTEGER NOT NULL UNIQUE, \"Title\"TEXT NOT NULL, \"Weight\"REAL, \"Packing\"INTEGER NOT NULL, \"PurchasePrice\"INTEGER, \"SalePrice\"INTEGER, PRIMARY KEY(\"Id\" AUTOINCREMENT));" +
            "CREATE TABLE IF NOT EXISTS\"Storage_Main\"(\"Id\"INTEGER NOT NULL UNIQUE, \"Number\"INTEGER NOT NULL, PRIMARY KEY(\"Id\" AUTOINCREMENT));", connection).ExecuteScalar();
        }
    }
}
