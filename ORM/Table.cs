using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Runtime.InteropServices;

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

        /// <summary>
        /// Creates new table
        /// </summary>
        /// <param name="reader">Delegate that creates new instance of type from DataReader</param>
        /// <param name="columns">List of columns table consists of</param>
        public Table(params Column<EntityType>[] columns)
        {
            Columns = columns.ToList();
            Columns.Insert(0, new Column<EntityType>("Id", SqlType.INTEGER, (x) => x.Id, Constraint.PrimaryKey));
        }

        public EntityType Create(EntityType item)
        {
            string columnNamesString = Columns
                                    .Where((x) => !(x.Name == "Id"))
                                    .Select((x) => x.Name)
                                    .Aggregate((x, y) => $"{x}, {y}");
            string valuesString = Columns
                                    .Where((x) => !(x.Name == "Id"))
                                    .Select((x) => $"${x.Name}PARAM")
                                    .Aggregate((x, y) => $"{x}, {y}");
            string commandText = 
                $"INSERT INTO {Name}" +
                $"({columnNamesString})" +
                $"VALUES({valuesString});"+
                $"SELECT Id FROM Product WHERE ROWID = last_insert_rowid();";
            var parameters = Columns
                .Select((x) => new SQLiteParameter($"${x.Name}PARAM", x.GetValue(item)))
                .ToArray();

            var command = new SQLiteCommand(commandText, Database.Connect());
            command.Parameters.AddRange(parameters);
            item.Id = (int)(long)command.ExecuteScalar();
            return item;
        }
        public EntityType Read(int id)
        {
            var commandText = $"SELECT * FROM {Name} WHERE Id=$id";
            using var rdr = Database.CommitReaderTransaction(commandText, new SQLiteParameter("$id", id));
            rdr.Read();
            return (EntityType) typeof(EntityType)
                .GetConstructor(Columns.Select((x)=>x.Type.UnderlyingType)
                .ToArray())
                .Invoke(rdr.GetAllValues());
        }
        public IList<EntityType> ReadAll()
        {
            var commandText = $"SELECT * FROM {Name}";
            using var rdr = Database.CommitReaderTransaction(commandText);

            List<EntityType> res = new List<EntityType>();
            while(rdr.Read())
            {
                res.Add
                (
                    (EntityType)typeof(EntityType)
                    .GetConstructor(Columns.Select((x) => x.Type.UnderlyingType)
                    .ToArray())
                    .Invoke(rdr.GetAllValues())
                );
            }
            return res;
        }
    }
}