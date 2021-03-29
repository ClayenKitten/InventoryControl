using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryControl.ORM
{
    class AutoincrementConstraint : IConstraint
    {
        public string SqlName
            => "AUTOINCREMENT";
        public bool Check(IList<object> items)
        {
            throw new NotImplementedException();
        }

        public void Execute(IList<object> items)
        {
            throw new NotImplementedException();
        }
    }
}
