using System.Collections.Generic;
using System.Linq;

namespace InventoryControl.ORM
{
    public class UniqueConstraint : Constraint
    {
        public override string SqlName
            => "UNIQUE";
    }
}
