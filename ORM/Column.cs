using System;
using System.Collections.Generic;
using System.Linq;

namespace InventoryControl.ORM
{
    public class Column
    {
        public string Name { get; }
        public string Type { get; }
        public IList<IConstraint> Constraints { get; } = new List<IConstraint>();
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

        public Column(string name, SqlType type, 
                      ConstraintType constraints = ConstraintType.None,
                      object defaultValue = null)
        {
            Name = name;
            Type = type.ToString();

            if (defaultValue != null)
            {
                Constraints.Add(new DefaultValueConstraint(defaultValue));
            }
            if (constraints.HasFlag(ConstraintType.PrimaryKey))
            {
                Constraints.Add(new PrimaryKeyConstraint());
            }
            if (constraints.HasFlag(ConstraintType.Autoincrement))
            {
                Constraints.Add(new AutoincrementConstraint());
            }
            if (constraints.HasFlag(ConstraintType.Unique))
            {
                Constraints.Add(new UniqueConstraint());
            }
            if (constraints.HasFlag(ConstraintType.NotNull))
            {
                Constraints.Add(new NotNullConstraint());
            }
        }
    }
}
