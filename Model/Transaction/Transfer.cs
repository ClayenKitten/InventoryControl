using InventoryControl.ORM;
using System;
using System.Collections.Generic;

namespace InventoryControl.Model
{
    public class Transfer : IEntity
    {
        public static Table<Transfer> Table { get; } = new Table<Transfer>
            (
                reader: (rdr) =>
                {
                    throw new NotImplementedException();
                },
                new Column<Transfer>("DateTime", SqlType.DATETIME, (x) => x.DateTime,
                    Constraint.NotNull),
                new Column<Transfer>("SupplierStorageId", SqlType.INTEGER, (x) => -1,
                    Constraint.NotNull | Constraint.ForeighnKey("Storage")),
                new Column<Transfer>("PurchaserStorageId", SqlType.INTEGER, (x) => -1,
                    Constraint.NotNull | Constraint.ForeighnKey("Storage"))
            );

        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public int PurchaserStorageId { get; set; }
        public int SupplierStorageId { get; set; }
        public List<TransactionProductPresenter> Products { get; set; }

        public Transfer(int id, DateTime dateTime, int purchaserStorageId, int supplierStorageId, List<TransactionProductPresenter> products)
        {
            Id = id;
            DateTime = dateTime;
            PurchaserStorageId = purchaserStorageId;
            SupplierStorageId = supplierStorageId;
            Products = products;
        }
    }
}
