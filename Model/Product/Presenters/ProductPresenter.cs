using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryControl.Model.Product
{
    public class ProductPresenter : IProductPresenter
    {
        public readonly ProductData productData;

        public int Id { get { return productData.Id; } }
        public string Name { get { return productData.Name; } }
        public string PurchasePrice
        {
            get
            {
                return productData.PurchasePrice.ToString();
            }
        }
        public string SalePrice
        {
            get
            {
                return productData.SalePrice.ToString();
            }
        }
        public string Packing
        {
            get
            {
                return productData.Measurement.GetFormattedValue();
            }
        }
        public string Measurement
        {
            get
            {
                return productData.Measurement.GetPostfix();
            }
        }
        public string Article { get { return productData.Article.ToString(); } }

        public ProductPresenter(ProductData productData)
        {
            this.productData = productData;
        }
    }
}
