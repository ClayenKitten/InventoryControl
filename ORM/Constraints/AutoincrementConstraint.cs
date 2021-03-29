using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryControl.ORM
{
    public class AutoincrementConstraint : Constraint
    {
        public override string SqlName
            => "AUTOINCREMENT";
        public override bool Check(IList<object> items)
        {
            throw new NotImplementedException();
        }

        public override void Execute(IList<object> items)
        {
            throw new NotImplementedException();
        }
    }
}
