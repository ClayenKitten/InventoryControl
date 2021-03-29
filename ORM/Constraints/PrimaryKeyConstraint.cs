using System.Collections.Generic;

namespace InventoryControl.ORM
{
    public class PrimaryKeyConstraint : Constraint
    {
        public override string SqlName
            => "PRIMARY KEY AUTOINCREMENT UNIQUE NOT NULL";

        public override bool Check(IList<object> items)
        {
            return new UniqueConstraint().Check(items) && new NotNullConstraint().Check(items);
        }

        public override void Execute(IList<object> items)
        {
            new AutoincrementConstraint().Execute(items);
        }
    }
}
