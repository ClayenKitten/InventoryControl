using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryControl.Panel
{
    public interface IPanel
    {
        int MinWidth { get; }

        bool Close();
        bool Collapse();
    }
}
