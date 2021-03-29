using System.Collections.Generic;
using System.Linq;
namespace InventoryControl.ORM
{
    public class NotNullConstraint : IConstraint
    {
        public string SqlName
            => "NOT NULL";
        public void Execute(IList<object> items) { }
        public bool Check(IList<object> items)
        {
            return items.All((value) => value != null);
        }
    }
}
