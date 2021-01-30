using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryControl.ViewModel
{
    public static class GlobalCommands
    {
        //Executed when view must be refreshed due to updates in model
        private static readonly EventCommand modelUpdated = new EventCommand();
        public static EventCommand ModelUpdated { get { return modelUpdated; } }
        //Executed when product is edited from StorageViewer
        private static readonly EventCommand editProduct = new EventCommand();
        public static EventCommand EditProduct { get { return editProduct; } }
    }
}
