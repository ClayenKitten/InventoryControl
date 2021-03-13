namespace InventoryControl.Model
{
    public class StockProductPresenter : ProductPresenter
    {
        private readonly int storageId;

        public int Remain
        {
            get
            {
                return StorageMapper.GetProductAmount(base.Product.Id, storageId);
            }
        }
        public bool IsInStock
        {
            get { return Remain > 0; }
        }

        public StockProductPresenter(Product product, int storageId) : base(product)
        {
            this.storageId = storageId;
        }
    }
}
