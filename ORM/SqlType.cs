using System;

namespace InventoryControl.ORM
{
    public class SqlType
    {
        public Type UnderlyingType { get; }
        public string StringRepresentation { get; }

        public override string ToString()
            => StringRepresentation;

        private SqlType(Type underlyingType, string stringRepresentation)
        {
            UnderlyingType = underlyingType;
            StringRepresentation = stringRepresentation;
        }

        public static SqlType BOOLEAN
            => new SqlType(typeof(bool), "BOOLEAN");
        public static SqlType INTEGER
            => new SqlType(typeof(int), "INTEGER");
        public static SqlType REAL
            => new SqlType(typeof(double), "REAL");
        public static SqlType TEXT
            => new SqlType(typeof(string), "TEXT");
        public static SqlType DATETIME
            => new SqlType(typeof(DateTime), "DATETIME");

    }
}
