using InventoryControl.ORM;
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
            return new Storage(id, "", "", -1);
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
            => Storage.Table.Read(id);

        public static List<Storage> GetAllStorages()
            => (List<Storage>)Storage.Table.ReadAll();

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
