using InventoryControl.Model.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryControl.Model.Product
{
    public class StockProductPresenter : IProductPresenter
    {
        public readonly ProductData Product;
        private readonly int storageId;

        public int Id { get { return Product.Id; } }
        public string Name { get { return Product.Name; } }
        public string PurchasePrice
        {
            get
            {
                return Product.PurchasePrice.ToString();
            }
        }
        public string SalePrice
        {
            get
            {
                return Product.SalePrice.ToString();
            }
        }
        public string Packing
        {
            get
            {
                return Product.Measurement.GetFormattedValue();
            }
        }
        public string Measurement
        {
            get
            {
                return Product.Measurement.GetPostfix();
            }
        }
        public string Article { get { return Product.Article.ToString(); } }
        public int Remain
        {
            get
            {
                return StorageDataMapper.GetProductAmount(Product.Id, storageId);
            }
        }

        public StockProductPresenter(ProductData product, int storageId)
        {
            this.Product = product;
            this.storageId = storageId;
        }
    }
}
