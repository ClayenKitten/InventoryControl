namespace InventoryControl.Model
{
    public interface IMeasurement
    {        
        public string FormattedValue { get; }
        public string Postfix { get; }
        public string ToString();

        public double RawValue { get; set; }
        public Unit Unit { get; set; }
    }
}
