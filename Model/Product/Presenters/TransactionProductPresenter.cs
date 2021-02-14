using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryControl.Model.Product
{
    public class TransactionProductPresenter : ProductPresenter
    {
        public int TransmitNumber { get; set; }

        public TransactionProductPresenter(ProductData product, int transmitNumber) : base(product)
        {
            this.TransmitNumber = transmitNumber;
        }
    }
}
