using InventoryControl.ORM;
using InventoryControl.Util;
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
                       if (rdr.GetInt32(7) == 0)
                       {
                           return new Purchaser()
                           {
                               Id = rdr.GetInt32(0),
                               Name = rdr.GetStringOrEmpty(1),
                               Address = rdr.GetStringOrEmpty(2),
                               Contacts = rdr.GetStringOrEmpty(3),
                               TaxpayerNumber = rdr.GetStringOrEmpty(4),
                               AccountingCode = rdr.GetStringOrEmpty(5),
                               BankDetails = rdr.GetStringOrEmpty(6)
                           };
                       }
                       else
                       {
                           return new Supplier()
                           {
                               Id = rdr.GetInt32(0),
                               Name = rdr.GetStringOrEmpty(1),
                               Address = rdr.GetStringOrEmpty(2),
                               Contacts = rdr.GetStringOrEmpty(3),
                               TaxpayerNumber = rdr.GetStringOrEmpty(4),
                               AccountingCode = rdr.GetStringOrEmpty(5),
                               BankDetails = rdr.GetStringOrEmpty(6)
                           };
                       }
                   },
                   new Column<Counterparty>("Name",           SqlType.TEXT, (x) => x.Name,
                        Constraint.NotNull),
                   new Column<Counterparty>("Address",        SqlType.TEXT, (x) => x.Address,
                        Constraint.NotNull),
                   new Column<Counterparty>("Contacts",       SqlType.TEXT, (x) => x.Contacts,
                        Constraint.NotNull),
                   new Column<Counterparty>("TaxpayerNumber", SqlType.TEXT, (x) => x.TaxpayerNumber,
                        Constraint.NotNull),
                   new Column<Counterparty>("AccountingCode", SqlType.TEXT, (x) => x.AccountingCode),
                   new Column<Counterparty>("BankDetails",    SqlType.TEXT, (x) => x.BankDetails),
                   new Column<Counterparty>("Role", SqlType.INTEGER, (x) => x is Purchaser ? 0 : 1,
                        Constraint.NotNull | Constraint.DefaultValue(0))
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
