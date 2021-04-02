using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace InventoryControl.ORM
{
    public static class SQLiteReaderExtensionMethods
    {
        public static string GetStringOrEmpty(this SQLiteDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetString(colIndex);
            return string.Empty;
        }
        private static T GetValue<T>(this SQLiteDataReader reader, string colName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.IsDBNull(i))
                {
                    throw new NullReferenceException($"Column with name {colName} is null");
                }
                if (reader.GetName(i) == colName)
                {
                    return (T)reader.GetValue(i);
                }
            }
            throw new ArgumentOutOfRangeException($"Column with name {colName} not found");
        }
        private static T GetValueOrDefault<T>(this SQLiteDataReader reader, string colName, T defaultValue)
        {
            try
            {
                return GetValue<T>(reader, colName);
            }
            catch(NullReferenceException)
            {
                return defaultValue;
            }
        }

        public static object[] GetAllValues(this SQLiteDataReader reader)
        {
            var objs = new List<object>();
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if(reader.GetFieldType(i).Name == typeof(long).Name)
                {
                    objs.Add((int)(long)reader.GetValue(i));
                }
                else if(reader.GetFieldType(i).Name == typeof(string).Name)
                {
                    objs.Add(reader.GetStringOrEmpty(i));
                }
                else
                {
                    objs.Add(reader.GetValue(i));
                }
            }
            return objs.ToArray();
        }

        public static Int32 GetInt32(this SQLiteDataReader reader, string colName)
            => (int)(long)GetValue<object>(reader, colName);
        public static Int32 GetInt32OrDefault(this SQLiteDataReader reader, string colName, int defaultValue)
            => (int)(long)GetValueOrDefault<object>(reader, colName, defaultValue);

        public static Double GetDouble(this SQLiteDataReader reader, string colName)
            => (double)GetValue<object>(reader, colName);
        public static Double GetDoubleOrDefault(this SQLiteDataReader reader, string colName, double defaultValue)
            => (double)GetValueOrDefault<object>(reader, colName, defaultValue);

        public static Boolean GetBoolean(this SQLiteDataReader reader, string colName)
            => (bool)GetValue<object>(reader, colName);
        public static Boolean GetBooleanOrDefault(this SQLiteDataReader reader, string colName, bool defaultValue)
            => (bool)GetValueOrDefault<object>(reader, colName, defaultValue);

        public static string GetString(this SQLiteDataReader reader, string colName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.IsDBNull(i))
                {
                    return "";
                }
                if (reader.GetName(i) == colName)
                {
                    return reader.GetValue(i).ToString();
                }
            }
            throw new ArgumentOutOfRangeException($"Column with name {colName} not found");
        }
    }
}
