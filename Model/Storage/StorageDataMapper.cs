using System.Collections.Generic;
using System.Data.SQLite;

namespace InventoryControl.Model
{
    class StorageDataMapper
    {
        public static StorageData Create()
        {
            const string commandText = "INSERT INTO Storages DEFAULT VALUES; SELECT Id FROM Storages WHERE ROWID = last_insert_rowid();";
            var id = (int)Database.CommitScalarTransaction(commandText);
            return new StorageData(id, "");
        }
        public static void Update(StorageData storage)
        {
            const string commandText = "UPDATE Storages SET Title=$name, WHERE Id=$id;";
            Database.CommitNonQueryTransaction(commandText,
                new SQLiteParameter("$name", storage.Name),
                new SQLiteParameter("$id", storage.Id)
            );
        }
        public static StorageData Read(int id)
        {
            const string commandText = "SELECT * FROM Storages WHERE Id=$id";
            using var rdr = Database.CommitReaderTransaction(commandText, new SQLiteParameter("$id", id));
            if (rdr.Read())
                return new StorageData
                (
                    id:     rdr.GetInt32(0),
                    name:   rdr.GetString(1)
                );
            else
                throw new KeyNotFoundException();
        }
        public static void Delete(int id)
        {
            const string commandText = "DELETE * FROM Storages WHERE Id=$id";
            Database.CommitNonQueryTransaction(commandText, new SQLiteParameter("$id", id));
        }

        public static StorageData GetStorage(int storageId)
        {
            var res = new List<ProductData>();
            const string commandText =
            @"
                SELECT * FROM Storage
                WHERE Id=$storageId
            ";

            using var rdr = Database.CommitReaderTransaction(commandText,
                new SQLiteParameter("$storageId", storageId)
            );
            if (rdr.Read())
            {
                return new StorageData(rdr.GetInt32(0), rdr.GetString(1));
            }
            else
                throw new KeyNotFoundException();
        }
        public static List<StorageData> GetAllStorages()
        {
            var res = new List<StorageData>();
            const string commandText =
            @"
                SELECT * FROM Storage ORDER BY Id ASC
            ";
            using var rdr = Database.CommitReaderTransaction(commandText);
            while(rdr.Read())
            {
                res.Add(new StorageData(rdr.GetInt32(0), rdr.GetString(1)));
            }
            return res;
        }
        public static int GetProductAmount(int productId, int storageId)
        {
            const string commandText =
            @"
                SELECT ProductNumber.Number FROM ProductNumber
                WHERE ProductNumber.ProductId=$productId
                AND ProductNumber.StorageId=$storageId
            ";
            var res = Database.CommitScalarTransaction(commandText, 
                new SQLiteParameter("$productId", productId),
                new SQLiteParameter("$storageId", storageId)
            );
            if (!(res is null))
                return (int)(double)res;
            else
                return 0;
        }
    }
}
