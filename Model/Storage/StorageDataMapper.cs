using InventoryControl.Model.Product;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryControl.Model.Storage
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

        public static List<ProductData> GetProductsInStorage(int storageId)
        {
            var res = new List<ProductData>();
            const string commandText =
            @"
                SELECT Product.* FROM ProductNumber
                INNER JOIN Product ON ProductNumber.ProductId = Product.Id
                WHERE ProductNumber.StorageId=$storageId
            ";
            using var rdr = Database.CommitReaderTransaction(commandText, new SQLiteParameter("$storageId", storageId));
            while(rdr.Read())
            {
                res.Add(new ProductData
                (
                    title:          rdr.GetString(1),
                    purchasePrice:  rdr.GetDouble(4),
                    salePrice:      rdr.GetDouble(5),
                    unit:           rdr.GetInt32(3),
                    value:          rdr.GetDouble(2)
                ));
            }
            return res;
        }

        public static int GetProductAmount(int productId, int storageId)
        {
            const string commandText =
            @"
                SELECT Count(*) FROM ProductNumber
                WHERE ProductNumber.ProductId=$productId
                AND ProductNumber.StorageId=$storageId
            ";
            return (int)Database.CommitScalarTransaction(commandText,
                new SQLiteParameter("$productId",productId),
                new SQLiteParameter("$storageId",storageId)
            );
        }
    }
}
