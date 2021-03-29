using System;
using System.Collections.Generic;
using System.Linq;

namespace InventoryControl.ORM
{
    public class DefaultValueConstraint : Constraint
    {
        private object defaultValue;

        public override string SqlName
            => $"DEFAULT ({defaultValue.ToString()})";
        public override void Execute(IList<object> items)
        {
            for(int i = 0; i < items.Count; i++)
            {
                if(items[i] == null)
                {
                    items[i] = defaultValue;
                }
            }
        }
        public override bool Check(IList<object> items)
        {
            return true;
        }
        public DefaultValueConstraint(object defaultValue)
        {
            this.defaultValue = defaultValue;
        }
    }
}
