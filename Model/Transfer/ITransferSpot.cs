﻿using InventoryControl.ORM;

namespace InventoryControl.Model
{
    public interface ITransferSpot : IEntity, INamed
    {
        string Address { get; }
    }
}
