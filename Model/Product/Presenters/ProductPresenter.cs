namespace InventoryControl.Model
{
    public class ProductPresenter : IProductPresenter
    {
        public readonly ProductData Product;

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

        public ProductPresenter(ProductData productData)
        {
            this.Product = productData;
        }
        public static implicit operator ProductPresenter(ProductData productData)
        {
            return new ProductPresenter(productData);
        }
    }
}
