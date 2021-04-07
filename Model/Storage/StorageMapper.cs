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
        public static Storage Read(long id)
            => Storage.Table.Read(id);

        public static List<Storage> GetAllStorages()
            => (List<Storage>)Storage.Table.ReadAll();

        public static int GetProductAmount(long productId, long storageId)
        {
            var s = Storage.Table.ReadAll();
            object num = Storage.ProductsNumberTable.Read(productId, storageId);
            if (num is null)
            {
                return 0;
            }
            else
            {
                return (int)(long)num;
            }
        }
    }
}
