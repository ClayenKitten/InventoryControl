namespace InventoryControl.ORM
{
    public interface IColumn<ObjectType>
    {
        public string Name { get; }
        public SqlType Type { get; }
        public string CreationString { get; }

        public object GetValue(ObjectType obj);
        public void SetValue(ObjectType obj, object value);
    }
}