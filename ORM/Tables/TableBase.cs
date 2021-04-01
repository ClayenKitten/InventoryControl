using System.Data.SQLite;

namespace InventoryControl.ORM
{
    public abstract class TableBase
    {
        public abstract string Name { get; }
        protected abstract string CreationString { get; }

        public void Create(SQLiteConnection con)
        {
            if (!DoesExist(con))
            {
                new SQLiteCommand(CreationString, con).ExecuteNonQuery();
                InsertInitValues(con);
            }            
        }
        protected bool DoesExist(SQLiteConnection con)
        {
            return new SQLiteCommand(
                $"SELECT name FROM sqlite_master WHERE type='table' AND name='{Name}';", con)
                .ExecuteScalar() != null;
        }
        protected abstract void InsertInitValues(SQLiteConnection con);

    }
}
