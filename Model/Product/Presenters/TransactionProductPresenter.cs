namespace InventoryControl.Model
{
    public class TransactionProductPresenter : ProductPresenter, INamed
    {
        public int TransmitNumber { get; set; }

        public TransactionProductPresenter(Product product, int transmitNumber) : base(product)
        {
            this.TransmitNumber = transmitNumber;
        }
    }
}
