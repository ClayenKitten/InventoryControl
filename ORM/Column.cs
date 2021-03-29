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

        public Column(string name, SqlType type, object defaultValue = null)
            : this(name, type, new List<Constraint>(), defaultValue) { }
        public Column(string name, SqlType type, Constraint constraint, object defaultValue = null) 
            : this(name, type, new List<Constraint>() { constraint }, defaultValue) { }
        public Column(string name, SqlType type, IList<Constraint> constraints, object defaultValue = null)
        {
            Name = name;
            Type = type.ToString();

            Constraints = constraints;
            if (defaultValue != null)
            {
                Constraints.Add(new DefaultValueConstraint(defaultValue));
            }

        }
    }
}
