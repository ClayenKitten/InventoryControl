using InventoryControl.ORM;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InventoryControl.Model
{
    public class Transfer : IEntity
    {
        public static JoinTable TransferProducts { get; }
            = new JoinTable("TransferProducts", typeof(Transfer), typeof(Product), SqlType.INTEGER);
        public static Table<Transfer> Table { get; } = new Table<Transfer>
        (
            new Column<Transfer>("DateTime", SqlType.DATETIME, (x) => x.DateTime,
                Constraint.NotNull),
            new Column<Transfer>("CounterpartyId", SqlType.INTEGER, (x) => x.CounterpartyId,
                Constraint.NotNull | Constraint.ForeighnKey("Counterparty")),
            new Column<Transfer>("StorageId", SqlType.INTEGER, (x) => x.StorageId,
                Constraint.NotNull | Constraint.ForeighnKey("Storage"))
        );

        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public int CounterpartyId { get; set; }
        public int StorageId { get; set; }
        public List<TransactionProductPresenter> Products { get; private set; }

        public Transfer(int id, DateTime dateTime, int counterpartyId, int storageId)
        {
            Id = id;
            DateTime = dateTime;
            CounterpartyId = counterpartyId;
            StorageId = storageId;
            Products = TransferProducts.ReadAll()
                        .Where(x => x.Item1 == Id)
                        .Select(x => new TransactionProductPresenter(Product.Table.Read(x.Item2), (int)(long)x.Item3))
                        .ToList();
        }
        public Transfer(int id, DateTime dateTime, int counterpartyId, int storageId, List<TransactionProductPresenter> products)
        {
            Id = id;
            DateTime = dateTime;
            CounterpartyId = counterpartyId;
            StorageId = storageId;
            Products = products;
        }
    }
}
