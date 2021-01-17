using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryControl.Model.Product
{
    public interface IProductPresenter
    {
        public string Title { get; }
        public string PurchasePrice { get; }
        public string SalePrice { get; }
        public string Measurement { get; }
        public string Packing { get; }
    }
}
