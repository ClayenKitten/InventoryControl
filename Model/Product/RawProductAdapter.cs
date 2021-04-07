using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryControl.Model
{
    public class RawProductAdapter
    {
        public readonly Product Product;

        public long Id { get { return Product.Id; } }
        public string Article
        {
            get => Product.Article;
            set => Product.Article = value;
        }
        public string Name
        {
            get => Product.Name;
            set => Product.Name = value;
        }
        public string Category
        {
            get => Product.Category.FullPath;
            set => Product.Category = new ProductCategory(value);
        }
        public long Manufacturer
        {
            get => Product.Manufacturer.Id;
            set => Product.Manufacturer = Model.Manufacturer.Table.Read(value);
        }
        public double PurchasePrice
        {
            get => Product.PurchasePrice.RawValue;
            set => Product.PurchasePrice.RawValue = value;
        }
        public double SalePrice
        {
            get => Product.SalePrice.RawValue;
            set => Product.SalePrice.RawValue = value;
        }
        public double Packing
        {
            get => Product.Measurement.RawValue;
            set => Product.Measurement.RawValue = value;
        }
        public int Measurement
        {
            get => Product.Measurement.Unit.value;
            set => Product.Measurement.Unit = new Unit(value);
        }

        public RawProductAdapter(Product product)
        {
            Product = product;
        }
        public static implicit operator RawProductAdapter(Product productData)
            => new RawProductAdapter(productData);
        public static implicit operator Product(RawProductAdapter product)
            => product.Product;
    }
}
