using System.Collections.Generic;

namespace InventoryControl.ORM
{
    public class PrimaryKeyConstraint : IConstraint
    {
        public string SqlName
            => "PRIMARY KEY AUTOINCREMENT UNIQUE NOT NULL";

        public bool Check(IList<object> items)
        {
            return new UniqueConstraint().Check(items) && new NotNullConstraint().Check(items);
        }

        public void Execute(IList<object> items)
        {
            new AutoincrementConstraint().Execute(items);
        }
    }
}
