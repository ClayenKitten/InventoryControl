using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryControl.ORM
{
    [Flags]
    public enum ConstraintType
    {
        None = 0,
        PrimaryKey = 1,
        NotNull = 2,
        Unique = 4,
        Autoincrement = 8,
    }
}
