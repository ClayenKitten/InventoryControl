using InventoryControl.ORM;
using InventoryControl.Util;
using System.Collections.Generic;

namespace InventoryControl.Model
{
    public class Product : IEntity, INamed
    {
        public static Table<Product> Table { get; } = new Table<Product>
        (
            reader: (rdr) =>
            {
                return new Product
                (
                    id: rdr.GetInt32("Id"),
                    name: rdr.GetString("Name"),
                    unit: rdr.GetInt32("Measurement"),
                    unitValue: rdr.GetDouble("Packing"),
                    purchasePrice: rdr.GetDouble("PurchasePrice"),
                    salePrice: rdr.GetDouble("SalePrice"),
                    article: rdr.GetString("Article"),
                    manufacturerId: rdr.GetInt32OrDefault("ManufacturerId", -1),
                    category: rdr.GetString("Category")
                );
            },
            new Column<Product>("Name", SqlType.TEXT, (x) => x.Name,
                Constraint.NotNull),
            new Column<Product>("Measurement", SqlType.INTEGER, (x) => x.Measurement.GetUnit().value,
                Constraint.NotNull | Constraint.DefaultValue(0)),
            new Column<Product>("Packing", SqlType.REAL, (x) => x.Measurement.GetRawValue(),
                Constraint.NotNull | Constraint.DefaultValue(0)),
            new Column<Product>("PurchasePrice", SqlType.REAL, (x) => x.PurchasePrice.GetRawValue(),
                Constraint.NotNull | Constraint.DefaultValue(0.0)),
            new Column<Product>("SalePrice", SqlType.REAL, (x) => x.SalePrice.GetRawValue(),
                Constraint.NotNull | Constraint.DefaultValue(0.0)),
            new Column<Product>("Article", SqlType.TEXT, (x) => x.Article.ToString(),
                Constraint.NotNull | Constraint.Unique),
            new Column<Product>("IsArchived", SqlType.BOOLEAN, (x) => false,
                Constraint.NotNull | Constraint.DefaultValue(0)),
            new Column<Product>("SupplierId", SqlType.INTEGER, (x) => "-1",
                Constraint.ForeighnKey("Counterparty")),
            new Column<Product>("ManufacturerId", SqlType.INTEGER, (x) => "-1",
                Constraint.ForeighnKey("Manufacturer")),
            new Column<Product>("Category", SqlType.TEXT, (x) => x.Category.FullPath)
        );

        public int Id { get; set; }
        public string Name { get; }
        public Money PurchasePrice { get; }
        public Money SalePrice { get; }
        public IMeasurement Measurement { get; set; }
        public Article Article { get; set; }
        public ProductCategory Category { get; set; }

        private int manufacturerId { get; set; }
        public Manufacturer Manufacturer
        {
            get => ManufacturerMapper.Read(manufacturerId);
            set => manufacturerId = value.Id;
        }
            

        //Database-oriented constructor
        public Product(int id, string name, 
            double purchasePrice, double salePrice,
            double unitValue, int unit,
            string article,
            int manufacturerId, string category)
        {
            this.Id = id;
            this.Name = name;
            this.PurchasePrice = new Money((decimal)purchasePrice);
            this.SalePrice = new Money((decimal)salePrice);

            if (unit == Unit.Kilogram.value)
                this.Measurement = new Weight((decimal)unitValue);
            else if (unit == Unit.Piece.value)
                this.Measurement = new Piece((int)unitValue);

            this.Article = new Article(article);
            this.Category = new ProductCategory("Молоко и молочные товары / Молоко");
        }
    }
}
