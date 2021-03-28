namespace InventoryControl.Model
{
    public interface IMeasurement
    {
        public string ToString();
        public string GetRawValue();
        public string GetFormattedValue();
        public string GetPostfix();

        public Unit GetUnit();
    }
}
