using InventoryControl.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryControl.Model
{
    public abstract class Counterparty : IEntity, INamed
    {
        public static Table<Counterparty> Table { get; } = new Table<Counterparty>
               (
                   reader: (rdr) =>
                   {
                       throw new NotImplementedException();
                   },
                   new Column("Name",           SqlType.TEXT, Constraint.NotNull),
                   new Column("Address",        SqlType.TEXT, Constraint.NotNull),
                   new Column("Contacts",       SqlType.TEXT, Constraint.NotNull),
                   new Column("TaxpayerNumber", SqlType.TEXT, Constraint.NotNull),
                   new Column("AccountingCode", SqlType.TEXT),
                   new Column("BankDetails",    SqlType.TEXT),
                   new Column("Role", SqlType.INTEGER, Constraint.NotNull | Constraint.DefaultValue(0))
               );

        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Contacts { get; set; }
        public string TaxpayerNumber { get; set; }
        public string AccountingCode { get; set; }
        public string BankDetails { get; set; }
    }
}
