namespace InventoryControl.Model
{
    public class Storage
    {
        public int Id { get; private set; }
        public string Name { set; get; }
        
        public Storage(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
    }
}
