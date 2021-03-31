using InventoryControl.Model;
using System;
using System.Data.SQLite;
using System.IO;

namespace InventoryControl.ORM
{
    static class Database
    {
        private static string BuildingQuery =
            Storage.ProductsNumberTable.CreationString
          + "CREATE TABLE IF NOT EXISTS TransferProducts (TransferId INTEGER REFERENCES Transfer (Id) ON DELETE CASCADE ON UPDATE CASCADE NOT NULL, ProductId INTEGER REFERENCES Product (Id) ON DELETE RESTRICT ON UPDATE CASCADE NOT NULL, Number INTEGER NOT NULL, UNIQUE (TransferId, ProductId) ON CONFLICT ROLLBACK);"
          + Product.Table.CreationString
          + Storage.Table.CreationString
          + Transfer.Table.CreationString
          + Counterparty.Table.CreationString
          + Manufacturer.Table.CreationString
          + PointOfSales.Table.CreationString;
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
                File.Create("Database.db");
            }

            var builder = new SQLiteConnectionStringBuilder
            {
                FailIfMissing = true,
                DataSource = "Database.db"
            };

            var con = new SQLiteConnection(builder.ConnectionString).OpenAndReturn();
            try
            {
                new SQLiteCommand(BuildingQuery, con).ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }
            con.Close();
        }
    }
}
