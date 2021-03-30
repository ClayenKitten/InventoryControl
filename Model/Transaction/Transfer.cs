using InventoryControl.ORM;
using System;
using System.Collections.Generic;

namespace InventoryControl.Model
{
    public class Transfer : IEntity
    {
        public static Table<Transfer> Table { get; } = new Table<Transfer>
            (
                new Column("DateTime", SqlType.DATETIME, Constraint.NotNull),
                new Column("SupplierStorageId", SqlType.INTEGER, Constraint.NotNull | Constraint.ForeighnKey("Storage")),
                new Column("PurchaserStorageId", SqlType.INTEGER, Constraint.NotNull | Constraint.ForeighnKey("Storage"))
            );

        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public int PurchaserStorageId { get; set; }
        public int SupplierStorageId { get; set; }
        public List<TransactionProductPresenter> Products { get; set; }

        public Transfer(DateTime dateTime, int purchaserStorageId, int supplierStorageId, List<TransactionProductPresenter> products)
        {
            DateTime = dateTime;
            PurchaserStorageId = purchaserStorageId;
            SupplierStorageId = supplierStorageId;
            Products = products;
        }
    }
}
