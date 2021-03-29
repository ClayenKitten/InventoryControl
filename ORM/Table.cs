using System;
using System.Collections.Generic;
using System.Linq;

namespace InventoryControl.ORM
{
    public class Table<EntityType> where EntityType : IEntity
    {
        public string Name
            => typeof(EntityType).Name;
        public IList<Column> Columns { get; }

        public string CreationString
        {
            get
            {
                var str = $"CREATE TABLE IF NOT EXISTS {Name}";
                var creationStrings = Columns.Select((x) => x.CreationString);
                if(creationStrings.Count() > 0)
                {
                    str += $" ({creationStrings.Aggregate((x, y) => $"{x}, {y}")});";
                }
                return str;
            }
        }
        
        public Table(IList<Column> columns)
        {
            Columns = columns;
            Columns.Insert(0, new Column("Id", SqlType.INTEGER, Constraint.PrimaryKey));
        }
    }
}
