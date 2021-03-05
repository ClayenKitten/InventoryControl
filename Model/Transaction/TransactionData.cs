using InventoryControl.Model.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryControl.Model.Transaction
{
    public class TransactionData
    {
        public DateTime DateTime { get; set; }
        public int PurchaserStorageId { get; set; }
        public int SupplierStorageId { get; set; }
        public List<TransactionProductPresenter> Products { get; set; }

        public TransactionData(DateTime dateTime, int purchaserStorageId, int supplierStorageId, List<TransactionProductPresenter> products)
        {
            DateTime = dateTime;
            PurchaserStorageId = purchaserStorageId;
            SupplierStorageId = supplierStorageId;
            Products = products;
        }
    }
}
