namespace InventoryControl.Model
{
    public class Piece : IMeasurement
    {
        private int rawValue;
        public Piece(int value)
        {
            rawValue = value;
            Unit = Unit.Piece;
        }

        public string FormattedValue
            => rawValue.ToString();
        public string Postfix
            => Unit.ToString();
        public override string ToString()
            => FormattedValue + " " + Postfix;

        public double RawValue
        {
            get => rawValue;
            set => rawValue = (int)value;
        }
        public Unit Unit { get; set; }
    }
}
