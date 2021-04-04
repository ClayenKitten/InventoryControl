namespace InventoryControl.Model
{
    public class Weight : IMeasurement
    {
        public Weight(double value)
        {
            RawValue = value;
            Unit = Unit.Kilogram;
        }

        public string FormattedValue
            => RawValue.ToString("0.0#", System.Globalization.CultureInfo.InvariantCulture);
        public string Postfix
            => Unit.ToString();
        public override string ToString()
            => FormattedValue + " " + Postfix;

        public Unit Unit { get; set; }
        public double RawValue { get; set; }
    }
}
