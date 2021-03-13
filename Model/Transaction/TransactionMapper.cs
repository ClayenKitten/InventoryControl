using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace InventoryControl.Model
{
    public static class TransactionMapper
    {
        public static Transaction Create(Transaction transactionData)
        {
            string commandText = @"
            INSERT INTO Transfer (DateTime, SupplierStorageId, PurchaserStorageId)
            VALUES ($DateTime, $SupplierStorageId, $PurchaserStorageId);
            SELECT Id FROM Transfer WHERE ROWID = last_insert_rowid();";
            transactionData.Id = (int)(long)Database.CommitScalarTransaction(commandText,
                new SQLiteParameter("$DateTime", transactionData.DateTime.Ticks),
                new SQLiteParameter("$SupplierStorageId", transactionData.SupplierStorageId),
                new SQLiteParameter("$PurchaserStorageId", transactionData.PurchaserStorageId));
            AddTransactionProducts(transactionData);
            return transactionData;
        }
        private static void AddTransactionProducts(Transaction transaction)
        {
            string commandText = "INSERT INTO TransferProducts (TransferId, ProductId, Number) VALUES ";
            foreach (var product in transaction.Products)
            {
                commandText += $"({transaction.Id}, {product.Id}, {product.TransmitNumber}),";
            }
            commandText = commandText.TrimEnd(',') + ";";
            Database.CommitScalarTransaction(commandText);
        }
    }
}
