using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Runtime.InteropServices;

namespace InventoryControl.ORM
{
    public class ConstructorEntityTable<EntityType> : TableBase where EntityType : IEntity
    {
        public override string Name
            => typeof(EntityType).Name;
        public IList<Column<EntityType>> Columns { get; }
        protected override string CreationString
        {
            get
            {
                var str = $"CREATE TABLE IF NOT EXISTS {Name}";
                var creationStrings = Columns.Select((x) => x.CreationString);
                if (creationStrings.Count() > 0)
                {
                    str += $" ({creationStrings.Aggregate((x, y) => $"{x}, {y}")});";
                }
                return str;
            }
        }

        private IList<EntityType> InitValues { get; }

        public ConstructorEntityTable(IList<EntityType> InitValues, params Column<EntityType>[] columns)
        {
            this.InitValues = InitValues;
            Columns = columns.ToList();
            Columns.Insert(0, new Column<EntityType>("Id", SqlType.LONG, (x) => x.Id, Constraint.PrimaryKey));
        }
        public ConstructorEntityTable(params Column<EntityType>[] columns)
            : this(new List<EntityType>(), columns) { }

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
                $"VALUES({valuesString});" +
                $"SELECT Id FROM {Name} WHERE ROWID = last_insert_rowid();";
            var parameters = Columns
                .Select((x) => new SQLiteParameter($"${x.Name}PARAM", x.GetValue(item)))
                .ToArray();

            var command = new SQLiteCommand(commandText, Database.Connect());
            command.Parameters.AddRange(parameters);
            item.Id = (long)command.ExecuteScalar();
            return item;
        }
        public void Update(EntityType item)
        {
            string settersString = Columns
                                    .Where((x) => !(x.Name == "Id"))
                                    .Select((x) => $"{x.Name}=${x.Name}PARAM")
                                    .Aggregate((x, y) => $"{x}, {y}");
            string commandText = $"UPDATE {Name} SET {settersString} WHERE Id={item.Id}";
            var parameters = Columns
                .Select((x) => new SQLiteParameter($"${x.Name}PARAM", x.GetValue(item)))
                .ToArray();

            var command = new SQLiteCommand(commandText, Database.Connect());
            command.Parameters.AddRange(parameters);
            command.ExecuteScalar();
        }
        public EntityType Read(long id)
        {
            var commandText = $"SELECT * FROM {Name} WHERE Id=$id";
            using var rdr = Database.CommitReaderTransaction(commandText, new SQLiteParameter("$id", id));
            if (rdr.Read())
            {
                return (EntityType)typeof(EntityType)
                    .GetConstructor(Columns.Select((x) => x.Type.UnderlyingType)
                    .ToArray())
                    .Invoke(rdr.GetAllValues());
            }
            else
            {
                throw new ArgumentOutOfRangeException($"Record of {typeof(EntityType).Name} with id {id} not found!");
            }
        }
        public EntityType ReadOr(long id, EntityType or)
        {
            return Exists(id) ? Read(id) : or;
        }
        public bool Exists(long id)
        {
            var commandText = $"SELECT COUNT(1) FROM {Name} WHERE Id=$id;";
            long res = (long)Database.CommitScalarTransaction(commandText, new SQLiteParameter("$id", id));
            return !(res == 0);
        }
        public IList<EntityType> ReadAll()
        {
            var commandText = $"SELECT * FROM {Name}";
            using var rdr = Database.CommitReaderTransaction(commandText);

            List<EntityType> res = new List<EntityType>();
            while (rdr.Read())
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
        public bool TryDelete(long id)
        {
            try
            {
                var commandText = $"DELETE FROM {Name} WHERE Id=$id";
                var num = Database.CommitNonQueryTransaction(commandText, new SQLiteParameter("$id", id));
                return num > 0;
            }
            catch (SQLiteException)
            {
                return false;
            }
        }

        protected override void InsertInitValues(SQLiteConnection con)
        {
            foreach (EntityType value in InitValues)
            {
                Create(value);
            }
        }
    }
}