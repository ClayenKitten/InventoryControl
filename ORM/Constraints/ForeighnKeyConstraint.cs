using System.Collections.Generic;

namespace InventoryControl.ORM
{
    public class ForeighnKeyConstraint : Constraint
    {
        private string refTableName { get; }
        private string refColumnName { get; }

        public override string SqlName
            => $"REFERENCES {refTableName} ({refColumnName})";

        public override bool Check(IList<object> items)
        {
            return true;
        }
        public override void Execute(IList<object> items) { }

        public ForeighnKeyConstraint(string refTableName, string refColumnName = "Id")
        {
            this.refTableName = refTableName;
            this.refColumnName = refColumnName;
        }
    }
}
