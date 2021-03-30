using System.Collections.Generic;

namespace InventoryControl.ORM
{
    public class ForeighnKeyConstraint : Constraint
    {
        private string refTableName { get; }
        private string refColumnName { get; }

        public override string SqlName
            => $"REFERENCES {refTableName} ({refColumnName})";

        public ForeighnKeyConstraint(string refTableName, string refColumnName = "Id")
        {
            this.refTableName = refTableName;
            this.refColumnName = refColumnName;
        }
    }
}
