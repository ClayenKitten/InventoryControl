using InventoryControl.ORM;
using System.Collections.Generic;

namespace InventoryControl.Model
{
    public class PointOfSales : IEntity, INamed, ITransferSpot
    {
        public static Table<PointOfSales> Table { get; }
            = new Table<PointOfSales>
            (
                new Column<PointOfSales>("Name", SqlType.TEXT, x => x.Name),
                new Column<PointOfSales>("Address", SqlType.TEXT, x => x.Address)
            );

        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public PointOfSales(int id, string name, string address)
        {
            Id = id;
            Name = name;
            Address = address;
        }
    }
}
