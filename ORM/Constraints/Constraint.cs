using System.Collections.Generic;

namespace InventoryControl.ORM
{
    public abstract class Constraint
    {
        public abstract string SqlName { get; }
        public abstract void Execute(IList<object> items);
        public abstract bool Check(IList<object> items);

        public static IList<Constraint> operator |(Constraint x, Constraint y)
        {
            return new List<Constraint>() { x, y };
        }
        public static IList<Constraint> operator |(IList<Constraint> x, Constraint y)
        {
            x.Add(y);
            return x;
        }
    }
}
