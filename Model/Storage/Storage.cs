﻿using InventoryControl.ORM;
using InventoryControl.Util;
using System.Collections.Generic;

namespace InventoryControl.Model
{
    public class Storage : IEntity, INamed, ITransferSpot
    {
        public static JoinTable ProductsNumberTable { get; }
            = new JoinTable("ProductsNumber", typeof(Product), typeof(Storage), SqlType.INTEGER);
        public static Table<Storage> Table { get; } = new Table<Storage>
        (
            new List<Storage>() { new Storage(0, "Стандартный склад", "", 0) },
            new Column<Storage>("Name", SqlType.TEXT, (x) => x.Name,
                Constraint.NotNull),
            new Column<Storage>("Address", SqlType.TEXT, (x) => x.Address),
            new Column<Storage>("OwnerId", SqlType.INTEGER, (x) => x.ownerId,
                Constraint.NotNull | Constraint.ForeighnKey("Counterparty"))
        );

        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        private int ownerId;
        public Counterparty Owner
        {
            get => CounterpartyMapper.Get(ownerId);
            set => ownerId = value.Id;
        }
        
        public Storage(int id, string name, string address, int ownerId)
        {
            this.Id = id;
            this.Name = name;
            this.Address = address;
            this.ownerId = ownerId;
        }
    }
}
