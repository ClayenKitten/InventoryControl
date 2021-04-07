namespace InventoryControl.Model
{
    public class ProductPresenter : IProductPresenter, INamed
    {
        public readonly Product Product;

        public long Id { get { return Product.Id; } }
        public string Name { get { return Product.Name; } }
        public string Category => Product.Category.FullPath;
        public string Manufacturer => Product.Manufacturer.Name;
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
                return Product.Measurement.FormattedValue;
            }
        }
        public string Measurement
        {
            get
            {
                return Product.Measurement.Postfix;
            }
        }
        public string Article { get { return Product.Article.ToString(); } }

        public ProductPresenter(Product productData)
        {
            this.Product = productData;
        }
        public static implicit operator ProductPresenter(Product productData)
        {
            return new ProductPresenter(productData);
        }
        public static implicit operator Product(ProductPresenter product)
        {
            return product.Product;
        }
    }
}
