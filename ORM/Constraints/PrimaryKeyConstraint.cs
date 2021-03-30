using System.Collections.Generic;

namespace InventoryControl.ORM
{
    public class PrimaryKeyConstraint : Constraint
    {
        public override string SqlName
            => "PRIMARY KEY AUTOINCREMENT UNIQUE NOT NULL";
    }
}
