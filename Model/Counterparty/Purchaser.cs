namespace InventoryControl.Model
{
    public class Purchaser : ICounterparty
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Contacts { get; set; }
        public string TaxpayerNumber { get; set; }
        public string AccountingCode { get; set; }
        public string BankDetails { get; set; }
    }
}
