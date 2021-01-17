using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryControl.Model.Util
{
    public class Piece : IMeasurement
    {
        private readonly int value;
        private readonly Unit unit;
        public Piece(int value)
        {
            this.value = value;
            this.unit = Unit.Piece;
        }
        public override string ToString()
        {
            return this.GetFormattedValue() + " " + this.GetPostfix();
        }
        public string GetRawValue()
        {
            return value.ToString();
        }
        public string GetFormattedValue()
        {
            return this.GetRawValue();
        }
        public string GetPostfix()
        {
            return unit.ToString();
        }
        public Unit GetUnit()
        {
            return unit;
        }

        public static Piece operator +(Piece a, Piece b)
        {
            return new Piece(a.value + b.value);
        }
        public static Piece operator -(Piece a, Piece b)
        {
            return new Piece(a.value - b.value);
        }
    }
}
