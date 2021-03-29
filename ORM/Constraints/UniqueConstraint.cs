using System.Collections.Generic;
using System.Linq;

namespace InventoryControl.ORM
{
    public class UniqueConstraint : Constraint
    {
        public override string SqlName
            => "UNIQUE";
        public override bool Check(IList<object> items)
        {
            return items.Distinct().Count() == items.Count;
        }

        public override void Execute(IList<object> items) { }
    }
}
