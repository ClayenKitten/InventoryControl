using System;
using System.Collections.Generic;

namespace InventoryControl.Model
{
    public class Unit
    {
        public readonly int value;
        public Unit(int value)
        {
            this.value = value;
        }
        public override string ToString()
        {
            return FromInt(value);
        }

        private static readonly string[] PossibleValues = new string[] { "кг", "шт" };
        public static readonly Unit Kilogram = new Unit(0);
        public static readonly Unit Piece = new Unit(1);
        public static readonly Unit Ruble = new Unit(2);
        public static readonly Unit DefaultValue = Kilogram;
        public static string FromInt(int value)
        {
            try
            {
                return PossibleValues[value];
            }
            catch (IndexOutOfRangeException)
            {
                return "N/D";
            }
        }
        public static List<string> GetPossibleValues()
        {
            return new List<string>(PossibleValues);
        }
    }
}
