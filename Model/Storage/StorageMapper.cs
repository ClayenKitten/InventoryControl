using System.Collections.Generic;
using System.Data.SQLite;

namespace InventoryControl.Model
{
    class StorageMapper
    {
        public static Storage Create()
        {
            const string commandText = "INSERT INTO Storage DEFAULT VALUES; SELECT Id FROM Storage WHERE ROWID = last_insert_rowid();";
            var id = (int)Database.CommitScalarTransaction(commandText);
            return new Storage(id, "");
        }
        public static void Update(Storage storage)
        {
            const string commandText = "UPDATE Storage SET Title=$name, WHERE Id=$id;";
            Database.CommitNonQueryTransaction(commandText,
                new SQLiteParameter("$name", storage.Name),
                new SQLiteParameter("$id", storage.Id)
            );
        }
        public static Storage Read(int id)
        {
            const string commandText = "SELECT * FROM Storage WHERE Id=$id";
            using var rdr = Database.CommitReaderTransaction(commandText, new SQLiteParameter("$id", id));
            if (rdr.Read())
                return new Storage
                (
                    id:     rdr.GetInt32(0),
                    name:   rdr.GetString(1)
                );
            else
                throw new KeyNotFoundException();
        }
        public static void Delete(int id)
        {
            const string commandText = "DELETE * FROM Storage WHERE Id=$id";
            Database.CommitNonQueryTransaction(commandText, new SQLiteParameter("$id", id));
        }

        public static Storage GetStorage(int storageId)
        {
            var res = new List<Product>();
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
                return new Storage(rdr.GetInt32(0), rdr.GetString(1));
            }
            else
                throw new KeyNotFoundException();
        }
        public static List<Storage> GetAllStorages()
        {
            var res = new List<Storage>();
            const string commandText =
            @"
                SELECT * FROM Storage ORDER BY Id ASC
            ";
            using var rdr = Database.CommitReaderTransaction(commandText);
            while(rdr.Read())
            {
                res.Add(new Storage(rdr.GetInt32(0), rdr.GetString(1)));
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
