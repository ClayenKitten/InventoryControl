using System;
using System.Data.SQLite;

namespace InventoryControl.ORM
{
    static class Database
    {
        private const string BuildingQuery =
        @"
            CREATE TABLE IF NOT EXISTS Counterparty (Id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL UNIQUE, Name TEXT NOT NULL, Address TEXT NOT NULL, Contacts TEXT NOT NULL, TaxpayerNumber TEXT NOT NULL, AccountingCode TEXT, BankDetails TEXT, IsSupplier BOOLEAN NOT NULL DEFAULT (0), IsPurchaser BOOLEAN DEFAULT (0) NOT NULL);
            CREATE TABLE IF NOT EXISTS Product (Id INTEGER PRIMARY KEY AUTOINCREMENT UNIQUE NOT NULL, Title TEXT NOT NULL, Measurement INTEGER NOT NULL DEFAULT (0), Packing REAL DEFAULT (0) NOT NULL, IsArchived BOOLEAN DEFAULT (0) NOT NULL, SupplierId INTEGER REFERENCES Counterparty (Id) ON DELETE RESTRICT ON UPDATE CASCADE);
            CREATE TABLE IF NOT EXISTS ProductNumber (ProductId INTEGER REFERENCES Product (Id) ON DELETE CASCADE ON UPDATE CASCADE NOT NULL, StorageId INTEGER REFERENCES Storage (Id) ON DELETE CASCADE ON UPDATE CASCADE NOT NULL, Number REAL NOT NULL DEFAULT (0), UNIQUE (ProductId, StorageId) ON CONFLICT ROLLBACK);
            CREATE TABLE IF NOT EXISTS Storage (Id INTEGER PRIMARY KEY AUTOINCREMENT UNIQUE NOT NULL, Name TEXT NOT NULL, Address TEXT, CounterpartyId INTEGER REFERENCES Counterparty (Id) ON DELETE RESTRICT NOT NULL, IsManaged BOOLEAN NOT NULL);
            CREATE TABLE IF NOT EXISTS Transfer (Id INTEGER PRIMARY KEY AUTOINCREMENT UNIQUE NOT NULL, DateTime DATETIME NOT NULL, SupplierStorageId INTEGER NOT NULL REFERENCES Storage (Id) ON DELETE RESTRICT ON UPDATE CASCADE, PurchaserStorageId INTEGER REFERENCES Storage (Id) ON DELETE RESTRICT ON UPDATE CASCADE NOT NULL);
            CREATE TABLE IF NOT EXISTS TransferProducts (TransferId INTEGER REFERENCES Transfer (Id) ON DELETE CASCADE ON UPDATE CASCADE NOT NULL, ProductId INTEGER REFERENCES Product (Id) ON DELETE RESTRICT ON UPDATE CASCADE NOT NULL, Number INTEGER NOT NULL, UNIQUE (TransferId, ProductId) ON CONFLICT ROLLBACK);
        ";
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
        static private SQLiteConnection Connect()
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
            catch(SQLiteException e)
            {
                if (e.ErrorCode == (int)SQLiteErrorCode.CantOpen)
                {
                    SQLiteConnection.CreateFile("Database.db");
                }
                con.Open();
            }
            new SQLiteCommand(BuildingQuery, con).ExecuteNonQuery();
            return con;
        }
    }
}
