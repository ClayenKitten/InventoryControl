using InventoryControl.ORM;
using System.Collections.Generic;
using System.Data.SQLite;

namespace InventoryControl.Model
{
    class StorageMapper
    {
        public static Storage Create(Storage storage)
            => Storage.Table.Create(storage);
        public static void Update(Storage storage)
            => Storage.Table.Update(storage);
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
