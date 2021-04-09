namespace InventoryControl.Model
{
    public class InvoiceProduct
    {
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
