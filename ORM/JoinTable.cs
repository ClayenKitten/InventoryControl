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
                    $"{fname}Id INTEGER REFERENCES {fname} (Id)," +
                    $"{sname}Id INTEGER REFERENCES {sname} (Id)," +
                    $"{valueColumn.CreationString}" +
                    $")";
                return str;
            }
        }

        private Column<IEntity> valueColumn;

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
