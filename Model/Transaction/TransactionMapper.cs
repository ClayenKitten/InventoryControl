using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace InventoryControl.Model
{
    public static class TransactionMapper
    {
        private static int CreateTransaction(DateTime dateTime, int SupplierStorageId, int PurchaserStorageId)
        {
            string commandText = @"
            INSERT INTO Transfer (DateTime, SupplierStorageId, PurchaserStorageId)
            VALUES ($DateTime, $SupplierStorageId, $PurchaserStorageId);
            SELECT Id FROM Transfer WHERE ROWID = last_insert_rowid();";
            return (int)(long)Database.CommitScalarTransaction(commandText,
                new SQLiteParameter("$DateTime", dateTime.Ticks),
                new SQLiteParameter("$SupplierStorageId", SupplierStorageId),
                new SQLiteParameter("$PurchaserStorageId", PurchaserStorageId));
        }
        private static void AddTransactionProducts(int transactionId, List<TransactionProductPresenter> products)
        {
            string commandText = "INSERT INTO TransferProducts (TransferId, ProductId, Number) VALUES ";
            foreach (var product in products)
            {
                commandText += $"({transactionId}, {product.Id}, {product.TransmitNumber}),";
            }
            commandText = commandText.TrimEnd(',') + ";";
            Database.CommitScalarTransaction(commandText);
        }
        public static void Create(Transaction transaction)
        {
            int transactionId =
                CreateTransaction(transaction.DateTime, transaction.SupplierStorageId, transaction.PurchaserStorageId);
            AddTransactionProducts(transactionId, transaction.Products);            
        }
    }
}
