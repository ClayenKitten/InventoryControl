using InventoryControl.Model.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryControl.Model.Product
{
    public class StockProductPresenter : ProductPresenter
    {
        private readonly int storageId;

        public int Remain
        {
            get
            {
                return StorageDataMapper.GetProductAmount(base.Product.Id, storageId);
            }
        }

        public StockProductPresenter(ProductData product, int storageId) : base(product)
        {
            this.storageId = storageId;
        }
    }
}
