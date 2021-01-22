using InventoryControl.Util;
using InventoryControl.Model.Util;

namespace InventoryControl.Model.Product
{
    public class ProductData
    {
        public int Id { get; }
        public string Name { get; set; }
        public Money PurchasePrice { get; set; }
        public Money SalePrice { get; set; }
        public IMeasurement Measurement { get; set; }

        public ProductData(int id, string title, double purchasePrice, double salePrice, double value, int unit)
        {
            this.Id = id;
            this.Name = title;
            this.PurchasePrice = new Money((decimal)purchasePrice);
            this.SalePrice = new Money((decimal)salePrice);

            if (unit == Unit.Kilogram.value)
                this.Measurement = new Weight((decimal)value);
            else if (unit == Unit.Piece.value)
                this.Measurement = new Piece((int)value);
        }
    }
}
