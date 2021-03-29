using System.Collections.Generic;

namespace InventoryControl.ORM
{
    public interface IConstraint
    {
        string SqlName { get; }
        void Execute(IList<object> items);
        bool Check(IList<object> items);
    }
}
