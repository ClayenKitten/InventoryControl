using InventoryControl.ORM;
using InventoryControl.Util;

namespace InventoryControl.Model
{
    public class Storage : IEntity, INamed
    {
        public static Table<Storage> Table { get; } = new Table<Storage>
        (
            reader: (rdr) =>
            {
                return new Storage
                (
                    id: rdr.GetInt32(0),
                    name: rdr.GetStringOrEmpty(1),
                    address: rdr.GetStringOrEmpty(2),
                    ownerId: rdr.GetInt32(3)
                );
            },
            new Column<Storage>("Name", SqlType.TEXT, (x) => x.Name,
                Constraint.NotNull),
            new Column<Storage>("Address", SqlType.TEXT, (x) => x.Address),
            new Column<Storage>("OwnerId", SqlType.INTEGER, (x) => x.Owner.Id,
                Constraint.NotNull | Constraint.ForeighnKey("Counterparty"))
        );

        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public Counterparty Owner { get; set; }
        
        public Storage(int id, string name, string address, int ownerId)
        {
            this.Id = id;
            this.Name = name;
            this.Address = address;
            Owner = CounterpartyMapper.Get(ownerId);
        }
    }
}
