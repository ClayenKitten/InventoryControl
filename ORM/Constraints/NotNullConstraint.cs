using System.Collections.Generic;
using System.Linq;
namespace InventoryControl.ORM
{
    public class NotNullConstraint : Constraint
    {
        public override string SqlName
            => "NOT NULL";
        public override void Execute(IList<object> items) { }
        public override bool Check(IList<object> items)
        {
            return items.All((value) => value != null);
        }
    }
}
