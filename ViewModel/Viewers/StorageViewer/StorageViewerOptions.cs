using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryControl.ViewModel
{
    public class StorageViewerOptions
    {
        public int StorageId { get; set; }
        public bool IsSelectorEnabled { get; set; } = true;
        public bool ShowOutOfStockProducts { get; set; } = true;
        public bool GroupOutOfStockProducts { get; set; } = true;
        public bool ShowOptionsSettings { get; set; } = true;

        public StorageViewerOptions(int storageId)
        {
            this.StorageId = storageId;
        }
    }
}
