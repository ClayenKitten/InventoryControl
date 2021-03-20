namespace InventoryControl.Model
{
    public class PointOfSales : INamed
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
