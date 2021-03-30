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
        public DefaultValueConstraint(object defaultValue)
        {
            this.defaultValue = defaultValue;
        }
    }
}
