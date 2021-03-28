namespace InventoryControl.Model
{
    public class Weight : IMeasurement
    {
        private decimal value;
        private readonly Unit unit;
        public Weight(decimal value)
        {
            this.value = value;
            this.unit = Unit.Kilogram;
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
            return value.ToString("0.0#", System.Globalization.CultureInfo.InvariantCulture);
        }
        public string GetPostfix()
        {
            return unit.ToString();
        }
        public Unit GetUnit()
        {
            return unit;
        }

        public static Weight operator +(Weight a, Weight b)
        {
            return new Weight(a.value + b.value);
        }
        public static Weight operator -(Weight a, Weight b)
        {
            return new Weight(a.value - b.value);
        }
        public static Weight operator *(Weight a, int b)
        {
            return new Weight(a.value * b);
        }
        public static Weight operator /(Weight a, int b)
        {
            return new Weight(a.value / b);
        }
    }
}
