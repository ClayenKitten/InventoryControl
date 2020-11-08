using System;
using System.Collections.Generic;

namespace InventoryControl
{
    public class Measurement
    {
        private static readonly string[] PossibleValues = new string[] { "кг", "шт" };
        public static readonly String Kilogram = PossibleValues[0];
        public static readonly String Piece = PossibleValues[1];
        public static String FromInt(int value)
        {
            try
            {
                return PossibleValues[value];
            }
            catch(IndexOutOfRangeException)
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
