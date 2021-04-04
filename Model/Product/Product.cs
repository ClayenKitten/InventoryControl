using InventoryControl.ORM;
using InventoryControl.Util;
using System.Collections.Generic;

namespace InventoryControl.Model
{
    public class Product : IEntity, INamed
    {
        public static Table<Product> Table { get; } = new Table<Product>
        (
            new Column<Product>("Name", SqlType.TEXT, (x) => x.Name,
                Constraint.NotNull),
            new Column<Product>("Measurement", SqlType.INTEGER, (x) => x.Measurement.Unit.value,
                Constraint.NotNull | Constraint.DefaultValue(0)),
            new Column<Product>("Packing", SqlType.REAL, (x) => x.Measurement.RawValue,
                Constraint.NotNull | Constraint.DefaultValue(0)),
            new Column<Product>("PurchasePrice", SqlType.REAL, (x) => x.PurchasePrice.RawValue,
                Constraint.NotNull | Constraint.DefaultValue(0.0)),
            new Column<Product>("SalePrice", SqlType.REAL, (x) => x.SalePrice.RawValue,
                Constraint.NotNull | Constraint.DefaultValue(0.0)),
            new Column<Product>("Article", SqlType.TEXT, (x) => x.Article.ToString(),
                Constraint.NotNull | Constraint.Unique),
            new Column<Product>("IsArchived", SqlType.BOOLEAN, (x) => false,
                Constraint.NotNull | Constraint.DefaultValue(0)),
            new Column<Product>("SupplierId", SqlType.INTEGER, (x) => "-1",
                Constraint.ForeighnKey("Counterparty")),
            new Column<Product>("ManufacturerId", SqlType.INTEGER, (x) => x.manufacturerId,
                Constraint.ForeighnKey("Manufacturer")),
            new Column<Product>("Category", SqlType.TEXT, (x) => x.Category.FullPath)
        );

        public int Id { get; set; }
        public string Name { get; }
        public Money PurchasePrice { get; }
        public Money SalePrice { get; }
        public IMeasurement Measurement { get; set; }
        public string Article { get; set; }
        public ProductCategory Category { get; set; }

        private int manufacturerId { get; set; }
        public Manufacturer Manufacturer
        {
            get => Manufacturer.Table.ReadOr(manufacturerId, new Manufacturer(-1, ""));
            set => manufacturerId = value.Id;
        }
            

        //Database-oriented constructor
        public Product(int id, string name,
            int unit, double unitValue,
            double purchasePrice, double salePrice,
            string article, bool isArchived = false,
            int supplierId = -1, int manufacturerId = -1, string category = "")
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
            this.Category = new ProductCategory("Молоко и молочные товары / Молоко");
        }
    }
}
