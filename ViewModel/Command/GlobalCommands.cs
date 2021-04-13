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
        public static EventCommand ModelUpdated { get; } = new EventCommand();
        //Executed when transaction is created
        public static EventCommand CreateTransaction { get; } = new EventCommand();
    }
}
