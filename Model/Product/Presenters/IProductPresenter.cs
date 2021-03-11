namespace InventoryControl.Model
{
    public interface IProductPresenter
    {
        public string Name { get; }
        public string PurchasePrice { get; }
        public string SalePrice { get; }
        public string Measurement { get; }
        public string Packing { get; }
        public string Article { get; }
    }
}
