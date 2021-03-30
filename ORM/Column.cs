using System;
using System.Collections.Generic;
using System.Linq;

namespace InventoryControl.ORM
{
    public class Column<EntityType> where EntityType : IEntity
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

        private Func<EntityType, object> getter;

        public Column(string name, SqlType type, Func<EntityType, object> getter)
            : this(name, type, getter, new List<Constraint>()) { }
        public Column(string name, SqlType type, Func<EntityType, object> getter, Constraint constraint) 
            : this(name, type, getter, new List<Constraint>() { constraint }) { }
        public Column(string name, SqlType type, Func<EntityType, object> getter, IList<Constraint> constraints)
        {
            Name = name;
            Type = type.StringRepresentation;

            Constraints = constraints;
            this.getter = getter;
        }
    }
}
