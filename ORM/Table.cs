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
        public IList<Column<EntityType>> Columns { get; }

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

        /// <summary>
        /// Creates new table
        /// </summary>
        /// <param name="reader">Delegate that creates new instance of type from DataReader</param>
        /// <param name="columns">List of columns table consists of</param>
        public Table(Func<SQLiteDataReader, EntityType> reader,
                     params Column<EntityType>[] columns)
        {
            Columns = columns.ToList();
            Columns.Insert(0, new Column<EntityType>("Id", SqlType.INTEGER, (x) => x.Id, Constraint.PrimaryKey));

            this.reader = reader;
        }

        public EntityType Read(int id)
        {
            var commandText = $"SELECT * FROM {Name} WHERE Id=$id";
            using var rdr = Database.CommitReaderTransaction(commandText, new SQLiteParameter("$id", id));
            rdr.Read();
            return reader.Invoke(rdr);
        }
        public IList<EntityType> ReadAll()
        {
            var commandText = $"SELECT * FROM {Name}";
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
