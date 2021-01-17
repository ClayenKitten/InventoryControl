using InventoryControl.Model.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryControl.Model.Storage
{
    public class StorageData
    {
        public int Id { get; private set; }
        public String Name { set; get; }
        
        public StorageData(int id, String name)
        {
            this.Id = id;
            this.Name = Name;
        }
    }
}
