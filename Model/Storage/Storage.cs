using InventoryControl.ORM;
using InventoryControl.Util;
using System;
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

        public override bool Equals(object obj)
        {
            if (obj is Storage st)
            {
                return Id == st.Id &&
                       Name == st.Name &&
                       Address == st.Address;
            }
            else
            {
                return false;
            }
        }
        public override int GetHashCode()
        {
            int hashCode = 1983353833;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Address);
            return hashCode;
        }

        public static bool operator ==(Storage a, Storage b)
            => a.Equals(b);
        public static bool operator !=(Storage a, Storage b)
            => !a.Equals(b);
    }
}
