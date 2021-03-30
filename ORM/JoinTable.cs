using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryControl.ORM
{
    class JoinTable
    {
        private Type firstType;
        private Type secondType;

        public string Name { get; }

        public string CreationString
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
                    $"{valueColumn.CreationString}, " +
                    $"UNIQUE ({fname}, {sname}) ON CONFLICT ROLLBACK)";
                return str;
            }
        }

        private Column<IEntity> valueColumn;

        public void Create(int firstId, int secondId, object value)
        {
            var fname = firstType.Name;
            var sname = secondType.Name;
            var commandText = $"INSERT INTO {Name} ({fname}, {sname}, {valueColumn.Name}) VALUES ({firstId}, {secondId}, $value);";
            Database.CommitScalarTransaction(commandText, new SQLiteParameter("$value", value));
        }
        public void Update(int firstId, int secondId, object value)
        {
            var fname = firstType.Name;
            var sname = secondType.Name;
            var commandText = 
            $"UPDATE {Name} " +
            $"SET {valueColumn.Name}=$value " +
            $"WHERE {fname}={firstId} AND {sname}={secondId};";
            Database.CommitScalarTransaction(commandText, new SQLiteParameter("$value", value));
        }
        public object Read(int firstId, int secondId)
        {
            var fname = firstType.Name;
            var sname = secondType.Name;
            var commandText =
                $"SELECT {Name}.{valueColumn.Name} FROM {Name}" +
                $"WHERE {Name}.{fname}Id = $firstId" +
                $"AND {Name}.{sname}Id = $secondId";
            return Database.CommitScalarTransaction(commandText,
                new SQLiteParameter("$firstId", firstId),
                new SQLiteParameter("$secondId", secondId)
            );
        }

        public JoinTable(string name, Type first, Type second, Column<IEntity> valueColumn)
        {
            Name = name;
            firstType = first;
            secondType = second;
            this.valueColumn = valueColumn;
        }
    }
}
