using InventoryControl.ORM;
using InventoryControl.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InventoryControl.Model
{
    public class Transfer
    {
        public DateTime DateTime { get; set; }
        public TransferType Type { get; set; }
        public ITransferSpot TransferSpot1 { get; set; }
        public ITransferSpot TransferSpot2 { get; set; }
        public List<TransactionProductPresenter> Products { get; private set; }

        public Transfer(DateTime dateTime, TransferType type, ITransferSpot transferSpot1, ITransferSpot transferSpot2, List<TransactionProductPresenter> products)
        {
            DateTime = dateTime;
            Type = type;
            TransferSpot1 = transferSpot1;
            TransferSpot2 = transferSpot2;
            Products = products;
        }
    }
}
