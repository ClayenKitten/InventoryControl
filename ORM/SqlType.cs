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
        public SqlType(Type type)
        {
            StringRepresentation = ToSqlType(type);
            UnderlyingType = type;
        }

        public static string ToSqlType(Type CLRType)
        {
            if(CLRType == typeof(int))
            {
                return "INTEGER";
            }
            else if(CLRType == typeof(double) || CLRType == typeof(float) || CLRType == typeof(decimal))
            {
                return "REAL";
            }
            else if(CLRType == typeof(bool))
            {
                return "BOOLEAN";
            }
            else if(CLRType == typeof(string))
            {
                return "TEXT";
            }
            else if(CLRType == typeof(DateTime))
            {
                return "DATETIME";
            }
            else
            {
                throw new ArgumentException("Unsupported CLRType for database");
            }
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
