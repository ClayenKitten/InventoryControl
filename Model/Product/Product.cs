namespace InventoryControl.Model
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; }
        public Money PurchasePrice { get; }
        public Money SalePrice { get; }
        public IMeasurement Measurement { get; set; }
        public int Article { get; set; }

        //Database-oriented constructor
        public Product(int id, string name, double purchasePrice, double salePrice, double value, int unit, int article)
        {
            this.Id = id;
            this.Name = name;
            this.PurchasePrice = new Money((decimal)purchasePrice);
            this.SalePrice = new Money((decimal)salePrice);

            if (unit == Unit.Kilogram.value)
                this.Measurement = new Weight((decimal)value);
            else if (unit == Unit.Piece.value)
                this.Measurement = new Piece((int)value);

            this.Article = article;
        }
    }
}
