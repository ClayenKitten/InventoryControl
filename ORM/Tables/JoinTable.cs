using System;
using System.Collections.Generic;
using System.Data.SQLite;
namespace InventoryControl.ORM
{
    public class JoinTable : TableBase
    {
        private Type firstType;
        private Type secondType;
        private SqlType valueType;

        public override string Name { get; }

        protected override string CreationString
        {
            get
            {
                var fname = firstType.Name;
                var sname = secondType.Name;
                var str =
                    $"CREATE TABLE IF NOT EXISTS {Name} " +
                    $"(" +
                    $"{fname}Id INTEGER REFERENCES {fname} (Id) ON UPDATE CASCADE ON DELETE CASCADE NOT NULL," +
                    $"{sname}Id INTEGER REFERENCES {sname} (Id) ON UPDATE CASCADE ON DELETE CASCADE NOT NULL," +
                    $"Value {valueType.StringRepresentation} NOT NULL, " +
                    $"UNIQUE ({fname}Id, {sname}Id) ON CONFLICT ROLLBACK);";
                return str;
            }
        }

        public void Create(int firstId, int secondId, object value)
        {
            var fname = firstType.Name;
            var sname = secondType.Name;
            var commandText = $"INSERT INTO {Name} ({fname}Id, {sname}Id, Value) VALUES ({firstId}, {secondId}, $value);";
            Database.CommitScalarTransaction(commandText, new SQLiteParameter("$value", value));
        }
        public void Update(int firstId, int secondId, object value)
        {
            var fname = firstType.Name;
            var sname = secondType.Name;
            var commandText = $"UPDATE {Name} SET Value=$value WHERE {fname}Id={firstId} AND {sname}Id={secondId};";
            Database.CommitScalarTransaction(commandText, new SQLiteParameter("$value", value));
        }
        public object Read(int firstId, int secondId)
        {
            var fname = firstType.Name;
            var sname = secondType.Name;
            var commandText =
                $"SELECT {Name}.Value FROM {Name} " +
                $"WHERE {Name}.{fname}Id = $firstId " +
                $"AND {Name}.{sname}Id = $secondId;";
            return Database.CommitScalarTransaction(commandText,
                new SQLiteParameter("$firstId", firstId),
                new SQLiteParameter("$secondId", secondId)
            );
        }
        public IList<Tuple<int, int, object>> ReadAll()
        {
            var fname = firstType.Name;
            var sname = secondType.Name;
            var commandText = $"SELECT * FROM {Name};";
            var rdr = Database.CommitReaderTransaction(commandText);

            List<Tuple<int, int, object>> res = new List<Tuple<int, int, object>>();
            while(rdr.Read())
            {
                int id1 = rdr.GetInt32Safe(0);
                int id2 = rdr.GetInt32Safe(1);
                var val = rdr.GetValue(2);
                res.Add(new Tuple<int, int, object>(id1, id2, val));
            }
            return res;
        }

        protected override void InsertInitValues(SQLiteConnection con) { }

        public JoinTable(string name, Type first, Type second, SqlType value)
        {
            Name = name;
            firstType = first;
            secondType = second;
            valueType = value;
        }
    }
}
