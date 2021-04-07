using InventoryControl.Model;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;

namespace InventoryControl.ORM
{
    static class Database
    {
        private static List<TableBase> Tables = new List<TableBase>()
        {
            Storage.ProductsNumberTable,
            Product.Table,
            Storage.Table,
            Transfer.Table,
            Counterparty.Table,
            PointOfSales.Table,
            Transfer.TransferProducts,
        };
        static public object CommitScalarTransaction(string commandText, params SQLiteParameter[] parameters)
        {
            using var con = Database.Connect();
            var command = con.CreateCommand();
            command.CommandText = commandText;
            command.Parameters.AddRange(parameters);
            return command.ExecuteScalar();
        }
        static public int CommitNonQueryTransaction(string commandText, params SQLiteParameter[] parameters)
        {
            using var con = Database.Connect();
            var command = con.CreateCommand();
            command.CommandText = commandText;
            command.Parameters.AddRange(parameters);
            return command.ExecuteNonQuery();
        }
        static public SQLiteDataReader CommitReaderTransaction(string commandText, params SQLiteParameter[] parameters)
        {
            var con = Database.Connect();
            var command = con.CreateCommand();
            command.CommandText = commandText;
            command.Parameters.AddRange(parameters);
            return command.ExecuteReader();
        }
        static public SQLiteConnection Connect()
        {
            var builder = new SQLiteConnectionStringBuilder
            {
                FailIfMissing = true,
                DataSource = "Database.db"
            };
            var con = new SQLiteConnection(builder.ConnectionString);
            try
            {
                con.Open();
            }
            catch (SQLiteException e)
            {
                if (e.ErrorCode == (int)SQLiteErrorCode.CantOpen)
                {
                    SQLiteConnection.CreateFile("Database.db");
                }
                con.Open();
            }
            return con;
        }

        static Database()
        {
            if (!File.Exists("Database.db"))
            {
                SQLiteConnection.CreateFile("Database.db");
            }

            var builder = new SQLiteConnectionStringBuilder
            {
                FailIfMissing = true,
                DataSource = "Database.db"
            };

            var con = new SQLiteConnection(builder.ConnectionString).OpenAndReturn();
            foreach(var table in Tables)
            {
                table.Create(con);
            }
            con.Close();
        }
    }
}
