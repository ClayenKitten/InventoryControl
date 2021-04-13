using InventoryControl.ORM;

namespace InventoryControl.Model
{
    public class InvoiceProduct : IEntity
    {
        public static PropertyEntityTable<InvoiceProduct> Table = new PropertyEntityTable<InvoiceProduct>
        (
            new PropertyColumn<InvoiceProduct, long>("InvoiceId"),
            new PropertyColumn<InvoiceProduct, string>("Name"),
            new PropertyColumn<InvoiceProduct, int>("NumberOfPackages"),
            new PropertyColumn<InvoiceProduct, double>("NumberInPackage"),
            new PropertyColumn<InvoiceProduct, string>("Measurement"),
            new PropertyColumn<InvoiceProduct, double>("Price")
        );
        public long Id { get; set; }
        public long InvoiceId { get; set; }
        public string Name { get; set; }
        public int NumberOfPackages { get; set; }
        public double NumberInPackage { get; set; }
        public double Number
            => NumberOfPackages * NumberInPackage;
        public string Measurement { get; set; }
        public double Price { get; set; }
        public double Cost
            => Price * Number;
    }
}
