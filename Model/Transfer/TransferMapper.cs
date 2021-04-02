using InventoryControl.ORM;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace InventoryControl.Model
{
    public static class TransferMapper
    {
        public static Transfer Create(Transfer transfer)
        {
            var createdTransfer = Transfer.Table.Create(transfer);
            foreach (var product in transfer.Products)
            {
                Transfer.TransferProducts.Create(transfer.Id, product.Id, product.TransmitNumber);
            }
            return createdTransfer;
        }
    }
}
