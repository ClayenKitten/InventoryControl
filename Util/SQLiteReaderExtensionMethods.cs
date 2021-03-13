using System.Data.SQLite;

namespace InventoryControl.Util
{
    public static class SQLiteReaderExtensionMethods
    {
        public static string GetStringOrEmpty(this SQLiteDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetString(colIndex);
            return string.Empty;
        }
    }
}
