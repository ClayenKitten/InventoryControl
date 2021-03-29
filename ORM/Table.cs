using System;
using System.Collections.Generic;
using System.Linq;

namespace InventoryControl.ORM
{
    public class Table<EntityType>
    {
        public string Name
            => typeof(EntityType).Name;
        public IList<Column> Columns { get; set; }

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
        
        public Table()
        {
            Columns = new List<Column>();
        }
        public Table(IList<Column> columns)
        {
            Columns = columns;
        }
    }
}
