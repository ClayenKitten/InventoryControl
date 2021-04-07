namespace InventoryControl.Model
{
    public class StockProductPresenter : ProductPresenter, INamed
    {
        private readonly long storageId;

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

        public StockProductPresenter(Product product, long storageId) : base(product)
        {
            this.storageId = storageId;
        }
    }
}
