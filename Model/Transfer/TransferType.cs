using System;

namespace InventoryControl.Model
{
    public class TransferType
    {
        private int id;
        public TransferType(int id) => this.id = id;

        public static TransferType Buy => new TransferType(0);
        public static TransferType Sell => new TransferType(1);
        public static TransferType Return => new TransferType(2);
        public static TransferType Supply => new TransferType(3);
        public static TransferType Transport => new TransferType(4);

        public bool Equals(TransferType other)
            => id == other.id;
        public static bool operator ==(TransferType first, TransferType second)
        {
            return first?.id == second?.id;
        }
        public static bool operator !=(TransferType first, TransferType second)
        {
            return first?.id != second?.id;
        }
        public static explicit operator TransferType(int id)
        {
            return new TransferType(id);
        }
        public static explicit operator int(TransferType type)
        {
            return type.id;
        }
    }
}
