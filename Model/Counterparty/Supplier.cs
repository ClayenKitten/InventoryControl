namespace InventoryControl.Model
{
    public class Supplier : Counterparty
    {
        public Supplier(int id, string name, string address, string contacts,
                         string taxpayerNumber, string accountingCode, string bankDetails)
            : base(id, name, address, contacts, taxpayerNumber, accountingCode, bankDetails, 1) 
        { }
    }
}
