namespace InventoryControl.Model
{
    public class StorageData
    {
        public int Id { get; private set; }
        public string Name { set; get; }
        
        public StorageData(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
    }
}
