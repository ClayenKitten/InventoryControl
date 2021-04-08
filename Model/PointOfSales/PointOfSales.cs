using InventoryControl.ORM;
using System.Collections.Generic;

namespace InventoryControl.Model
{
    public class PointOfSales : IEntity, INamed, ITransferSpot
    {
        public static PropertyEntityTable<PointOfSales> Table { get; } = new PropertyEntityTable<PointOfSales>
        (
            new PropertyColumn<PointOfSales, string>("Name"),
            new PropertyColumn<PointOfSales, string>("Address")
        );

        public long Id { get; set; }
        public string Name { get; set; } = "";
        public string Address { get; set; } = "";

        public PointOfSales() { }
        public PointOfSales(long id, string name, string address)
        {
            Id = id;
            Name = name;
            Address = address;
        }
    }
}
