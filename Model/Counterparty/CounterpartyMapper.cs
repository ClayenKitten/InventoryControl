using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using InventoryControl.ORM;
using InventoryControl.Util;

namespace InventoryControl.Model
{
    public class CounterpartyMapper
    {
        public static Counterparty Get(long id)
            => Counterparty.Table.Read(id);
        public static List<String> GetPurchasersNames()
            => GetAll().Select(x => x.Name).ToList();
        public static List<Counterparty> GetAll()
            => (List<Counterparty>)Counterparty.Table.ReadAll();

        public static List<Counterparty> GetPurchasers()
            => GetAll().Where(x => x.Role == 0).ToList();
        public static List<Counterparty> GetSuppliers()
            => GetAll().Where(x => x.Role == 1).ToList();
        public static Counterparty GetManaged()
            => Counterparty.Table.Read(1);

        public static Counterparty Create(Counterparty counterparty)
            => Counterparty.Table.Create(counterparty);
        public static void Update(Counterparty counterparty)
            => Counterparty.Table.Update(counterparty);
    }
}