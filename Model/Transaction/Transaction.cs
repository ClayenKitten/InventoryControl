using InventoryControl.ORM;
using System;
using System.Collections.Generic;

namespace InventoryControl.Model
{
    public class Transaction : IEntity
    {
        public static Table<Transaction> Table { get; } = new Table<Transaction>
            (
                new Column("DateTime", SqlType.DATETIME, Constraint.NotNull),
                new Column("SupplierStorageId", SqlType.INTEGER, Constraint.NotNull),
                new Column("PurchaserStorageId", SqlType.INTEGER, Constraint.NotNull)
            );

        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public int PurchaserStorageId { get; set; }
        public int SupplierStorageId { get; set; }
        public List<TransactionProductPresenter> Products { get; set; }

        public Transaction(DateTime dateTime, int purchaserStorageId, int supplierStorageId, List<TransactionProductPresenter> products)
        {
            DateTime = dateTime;
            PurchaserStorageId = purchaserStorageId;
            SupplierStorageId = supplierStorageId;
            Products = products;
        }
    }
}
