using System;
using System.Collections.Generic;
using System.Linq;

namespace InventoryControl.ORM
{
    public class DefaultValueConstraint : IConstraint
    {
        private object defaultValue;

        public string SqlName
            => $"DEFAULT ({defaultValue.ToString()})";
        public void Execute(IList<object> items)
        {
            for(int i = 0; i < items.Count; i++)
            {
                if(items[i] == null)
                {
                    items[i] = defaultValue;
                }
            }
        }
        public bool Check(IList<object> items)
        {
            return true;
        }
        public DefaultValueConstraint(object defaultValue)
        {
            this.defaultValue = defaultValue;
        }
    }
}
