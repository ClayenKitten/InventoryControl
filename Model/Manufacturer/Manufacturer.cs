using InventoryControl.ORM;

namespace InventoryControl.Model
{
    public class Manufacturer : ORM.IEntity, INamed
    {
        public static Table<Manufacturer> Table { get; } = new Table<Manufacturer>
        (
            new Column<Manufacturer>("Name", SqlType.TEXT, (x) => x.Name)
        );

        public long Id { get; set; }
        public string Name { get; set; }

        public Manufacturer(long id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
