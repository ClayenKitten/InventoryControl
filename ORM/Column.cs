using System;
using System.Collections.Generic;
using System.Linq;

namespace InventoryControl.ORM
{
    public class Column
    {
        public string Name { get; }
        public string Type { get; }
        public IList<Constraint> Constraints { get; } = new List<Constraint>();
        public string CreationString
        {
            get
            {
                var sqlNamesQuery = Constraints.Select((x) => x.SqlName);
                if(sqlNamesQuery.Count() > 0)
                {
                    return $"{Name} {Type} {sqlNamesQuery.Aggregate((x, y) => $"{x} {y}")}";
                }
                else
                {
                    return $"{Name} {Type}";
                }
            }
        }

        public Column(string name, SqlType type)
            : this(name, type, new List<Constraint>()) { }
        public Column(string name, SqlType type, Constraint constraint) 
            : this(name, type, new List<Constraint>() { constraint }) { }
        public Column(string name, SqlType type, IList<Constraint> constraints)
        {
            Name = name;
            Type = type.ToString();

            Constraints = constraints;

        }
    }
}
