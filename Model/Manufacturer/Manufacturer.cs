namespace InventoryControl.Model
{
    public class Manufacturer : ORM.IEntity, INamed
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
