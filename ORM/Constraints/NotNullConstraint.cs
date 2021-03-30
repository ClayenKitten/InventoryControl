using System.Collections.Generic;
using System.Linq;
namespace InventoryControl.ORM
{
    public class NotNullConstraint : Constraint
    {
        public override string SqlName
            => "NOT NULL";
    }
}
