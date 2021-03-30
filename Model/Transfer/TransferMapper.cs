using InventoryControl.ORM;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace InventoryControl.Model
{
    public static class TransferMapper
    {
        public static Transfer Create(Transfer transferData)
        {
            string commandText = @"
            INSERT INTO Transfer (DateTime, SupplierStorageId, PurchaserStorageId)
            VALUES ($DateTime, $SupplierStorageId, $PurchaserStorageId);
            SELECT Id FROM Transfer WHERE ROWID = last_insert_rowid();";
            transferData.Id = (int)(long)Database.CommitScalarTransaction(commandText,
                new SQLiteParameter("$DateTime", transferData.DateTime.Ticks),
                new SQLiteParameter("$SupplierStorageId", transferData.SupplierStorageId),
                new SQLiteParameter("$PurchaserStorageId", transferData.PurchaserStorageId));
            AddTransactionProducts(transferData);
            return transferData;
        }
        private static void AddTransactionProducts(Transfer transfer)
        {
            string commandText = "INSERT INTO TransferProducts (TransferId, ProductId, Number) VALUES ";
            foreach (var product in transfer.Products)
            {
                commandText += $"({transfer.Id}, {product.Id}, {product.TransmitNumber}),";
            }
            commandText = commandText.TrimEnd(',') + ";";
            Database.CommitScalarTransaction(commandText);
        }
    }
}
