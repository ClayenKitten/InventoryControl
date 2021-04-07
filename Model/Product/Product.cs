using InventoryControl.ORM;
using InventoryControl.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InventoryControl.Model
{
    public class Product : IEntity, INamed
    {
        public static Table<Product> Table { get; } = new Table<Product>
        (
            new Column<Product>("Name", SqlType.TEXT, (x) => x.Name,
                Constraint.NotNull),
            new Column<Product>("Measurement", SqlType.INT, (x) => x.Measurement.Unit.value,
                Constraint.NotNull | Constraint.DefaultValue(0)),
            new Column<Product>("Packing", SqlType.REAL, (x) => x.Measurement.RawValue,
                Constraint.NotNull | Constraint.DefaultValue(0)),
            new Column<Product>("PurchasePrice", SqlType.REAL, (x) => x.PurchasePrice.RawValue,
                Constraint.NotNull | Constraint.DefaultValue(0.0)),
            new Column<Product>("SalePrice", SqlType.REAL, (x) => x.SalePrice.RawValue,
                Constraint.NotNull | Constraint.DefaultValue(0.0)),
            new Column<Product>("Article", SqlType.TEXT, (x) => x.Article.ToString(),
                Constraint.NotNull | Constraint.Unique),
            new Column<Product>("Category", SqlType.TEXT, (x) => x.Category.FullPath)
        );

        public long Id { get; set; }
        public string Name { get; set; } = "";
        public Money PurchasePrice { get; set; } = new Money(0);
        public Money SalePrice { get; set; } = new Money(0);
        public IMeasurement Measurement { get; set; } = new Weight(1);
        public string Article { get; set; } = "";
        public ProductCategory Category { get; set; } = new ProductCategory("Без категории");

        //Database-oriented constructor
        public Product()
        {
            int val = 0;
            var products = Table.ReadAll();
            while (products.Any(x => x.Article == val.ToString()))
            {
                val += 1;
            }
            Article = val.ToString();
        }
        public Product(long id, string name,
            int unit, double unitValue,
            double purchasePrice, double salePrice,
            string article, string category = "")
        {
            this.Id = id;
            this.Name = name;
            this.PurchasePrice = new Money((decimal)purchasePrice);
            this.SalePrice = new Money((decimal)salePrice);

            if (unit == Unit.Kilogram.value)
                this.Measurement = new Weight(unitValue);
            else if (unit == Unit.Piece.value)
                this.Measurement = new Piece((int)unitValue);

            this.Article = article;
            this.Category = new ProductCategory(category);
        }
    }
}
