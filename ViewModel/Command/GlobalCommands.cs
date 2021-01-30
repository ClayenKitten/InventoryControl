using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryControl.ViewModel
{
    public static class GlobalCommands
    {
        private static readonly EventCommand modelUpdated = new EventCommand();
        public static EventCommand ModelUpdated { get { return modelUpdated; } }
    }
}
