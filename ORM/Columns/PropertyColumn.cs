using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace InventoryControl.ORM
{
    public class PropertyColumn<EntityType, ValueType> : IColumn<EntityType>
    {
        public string Name
            => Property.Name;
        public SqlType Type
            => new SqlType(Property.PropertyType);
        public string CreationString
        {
            get
            {
                var sqlNamesQuery = Constraints.Select((x) => x.SqlName);
                if (sqlNamesQuery.Count() > 0)
                {
                    return $"{Name} {SqlType.ToSqlType(Property.PropertyType)} {sqlNamesQuery.Aggregate((x, y) => $"{x} {y}")}";
                }
                else
                {
                    return $"{Name} {SqlType.ToSqlType(Property.PropertyType)}";
                }
            }
        }

        public PropertyInfo Property { get; set; }
        public IList<Constraint> Constraints { get; } = new List<Constraint>();

        public object GetValue(EntityType obj)
            => (ValueType)Property.GetValue(obj);
        public void SetValue(EntityType obj, object value)
            => Property.SetValue(obj, value);

        public PropertyColumn(string propertyName, IList<Constraint> constraint) : this(propertyName)
        {
            Constraints = constraint;
        }
        public PropertyColumn(string propertyName, Constraint constraint)
            : this(propertyName, new List<Constraint>() { constraint }) { }
        public PropertyColumn(string propertyName)
        {
            var property = typeof(EntityType).GetProperty(propertyName);
            if (property.CanRead && property.CanWrite)
            {
                Property = property;
            }
            else
            {
                throw new ArgumentException($"Property with name {property.Name} must have public read and write");
            }
        }
    }
}
