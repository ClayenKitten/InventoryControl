using InventoryControl.ORM;
using InventoryControl.ViewModel;
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
            new Column<Transfer>("Type", SqlType.INTEGER, (x) => (int)x.Type),
            new Column<Transfer>("TransferSpot1", SqlType.INTEGER, (x) => x.TransferSpot1.Id,
                Constraint.NotNull | Constraint.ForeighnKey("Counterparty")),
            new Column<Transfer>("TransferSpot2", SqlType.INTEGER, (x) => x.TransferSpot2.Id,
                Constraint.NotNull | Constraint.ForeighnKey("Storage"))
        );

        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public TransferType Type { get; set; }
        public ITransferSpot TransferSpot1 { get; set; }
        public ITransferSpot TransferSpot2 { get; set; }
        public List<TransactionProductPresenter> Products { get; private set; }

        public Transfer(int id, DateTime dateTime, int type, int transferSpot1Id, int transferSpot2Id)
        {
            Id = id;
            DateTime = dateTime;
            Type = (TransferType)type;
            if (Type == TransferType.Buy)
            {
                TransferSpot1 = Counterparty.Table.Read(transferSpot1Id);
                TransferSpot2 = Storage.Table.Read(transferSpot1Id);
            }
            else if (Type == TransferType.Sell)
            {
                TransferSpot1 = Counterparty.Table.Read(transferSpot1Id);
                TransferSpot2 = Storage.Table.Read(transferSpot1Id);
            }
            else if (Type == TransferType.Return)
            {
                TransferSpot1 = Counterparty.Table.Read(transferSpot1Id);
                TransferSpot2 = Storage.Table.Read(transferSpot1Id);
            }
            else if (Type == TransferType.Supply)
            {
                TransferSpot1 = Storage.Table.Read(transferSpot1Id);
                TransferSpot2 = PointOfSales.Table.Read(transferSpot1Id);
            }

            Products = TransferProducts.ReadAll()
                        .Where(x => x.Item1 == Id)
                        .Select(x => new TransactionProductPresenter(Product.Table.Read(x.Item2), (int)(long)x.Item3))
                        .ToList();
        }
        public Transfer(
            DateTime dateTime, 
            ITransferSpot transferSpot1, ITransferSpot transferSpot2,
            List<TransactionProductPresenter> products)
        {
            Id = -1;
            DateTime = dateTime;
            TransferSpot1 = transferSpot1;
            TransferSpot2 = transferSpot2;
            Products = products;
        }
    }
}
