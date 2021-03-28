namespace InventoryControl.Model
{
    public class Product : IEntity, INamed
    {
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
