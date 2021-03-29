using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryControl.ORM
{
    public static class Constraints
    {
        public static PrimaryKeyConstraint PrimaryKey
            => new PrimaryKeyConstraint();
        public static NotNullConstraint NotNull
            => new NotNullConstraint();
        public static UniqueConstraint Unique
            => new UniqueConstraint();
        public static AutoincrementConstraint Autoincrement
            => new AutoincrementConstraint();
    }
}
