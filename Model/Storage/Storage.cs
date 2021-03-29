using InventoryControl.ORM;

namespace InventoryControl.Model
{
    public class Storage : IEntity, INamed
    {
        public static Table<Storage> Table { get; } = new Table<Storage>
            (
                new Column("Name", SqlType.TEXT, Constraint.NotNull),
                new Column("Address", SqlType.TEXT),
                new Column("CounterpartyId", SqlType.INTEGER, Constraint.NotNull | Constraint.ForeighnKey("Counterparty")),
                new Column("IsManaged", SqlType.BOOLEAN, Constraint.NotNull)
            );

        public int Id { get; private set; }
        public string Name { set; get; }
        
        public Storage(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
    }
}
