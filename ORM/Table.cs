using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

namespace InventoryControl.ORM
{
    public class Table<EntityType> where EntityType : IEntity
    {
        public string Name
            => typeof(EntityType).Name;
        public IList<Column> Columns { get; }

        public string CreationString
        {
            get
            {
                var str = $"CREATE TABLE IF NOT EXISTS {Name}";
                var creationStrings = Columns.Select((x) => x.CreationString);
                if(creationStrings.Count() > 0)
                {
                    str += $" ({creationStrings.Aggregate((x, y) => $"{x}, {y}")});";
                }
                return str;
            }
        }

        private Func<SQLiteDataReader, EntityType> reader;
        
        public Table(Func<SQLiteDataReader, EntityType> reader, params Column[] columns)
        {
            Columns = columns.ToList();
            Columns.Insert(0, new Column("Id", SqlType.INTEGER, Constraint.PrimaryKey));

            this.reader = reader;
        }

        public EntityType Read(int id)
        {
            var commandText = "SELECT * FROM Product WHERE Id=$id";
            using var rdr = Database.CommitReaderTransaction(commandText, new SQLiteParameter("$id", id));
            rdr.Read();
            return reader.Invoke(rdr);
        }
        public IList<EntityType> ReadAll()
        {
            var commandText = "SELECT * FROM Product";
            using var rdr = Database.CommitReaderTransaction(commandText);

            List<EntityType> res = new List<EntityType>();
            while(rdr.Read())
            {
                res.Add(reader.Invoke(rdr));
            }
            return res;
        }

    }
}
