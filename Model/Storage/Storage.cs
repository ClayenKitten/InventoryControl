using InventoryControl.ORM;
using InventoryControl.Util;
using System.Collections.Generic;

namespace InventoryControl.Model
{
    public class Storage : IEntity, INamed, ITransferSpot
    {
        public static JoinTable ProductsNumberTable { get; }
            = new JoinTable("ProductsNumber", typeof(Product), typeof(Storage), SqlType.INT);
        public static PropertyEntityTable<Storage> Table { get; } = new PropertyEntityTable<Storage>
        (
            new List<Storage>() { new Storage(0, "Стандартный склад", "") },
            new PropertyColumn<Storage, string>("Name"),
            new PropertyColumn<Storage, string>("Address")
        );

        public long Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        
        public Storage() { }
        public Storage(long id, string name, string address)
        {
            this.Id = id;
            this.Name = name;
            this.Address = address;
        }
    }
}
