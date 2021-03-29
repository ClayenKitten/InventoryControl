using System.Collections.Generic;
using System.Linq;

namespace InventoryControl.ORM
{
    public class UniqueConstraint : IConstraint
    {
        public string SqlName
            => "UNIQUE";
        public bool Check(IList<object> items)
        {
            return items.Distinct().Count() == items.Count;
        }

        public void Execute(IList<object> items) { }
    }
}
