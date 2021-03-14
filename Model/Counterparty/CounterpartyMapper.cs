using System;
using System.Collections.Generic;
using System.Linq;
using InventoryControl.Util;

namespace InventoryControl.Model
{
    public class CounterpartyMapper
    {
        public static Counterparty Get(int id)
        {
            throw new NotImplementedException();
        }
        public static List<String> GetPurchasersNames()
        {
            using var rdr = Database.CommitReaderTransaction("SELECT Name FROM Counterparty");
            List<String> res = new List<string>();
            while (rdr.Read())
            {
                res.Add(rdr.GetString(0));
            }
            return res;
        }
        public static List<Counterparty> GetAll()
        {
            const string commandText = "SELECT * FROM Counterparty";
            using var rdr = Database.CommitReaderTransaction(commandText);
            List<Counterparty> res = new List<Counterparty>();
            while (rdr.Read())
            {
                res.Add(new Counterparty()
                {
                    Id = rdr.GetInt32(0),
                    Name = rdr.GetStringOrEmpty(1),
                    Address = rdr.GetStringOrEmpty(2),
                    Contacts = rdr.GetStringOrEmpty(3),
                    TaxpayerNumber = rdr.GetStringOrEmpty(4),
                    AccountingCode = rdr.GetStringOrEmpty(5),
                    BankDetails = rdr.GetStringOrEmpty(6),
                    IsSupplier = rdr.GetBoolean(7),
                    IsPurchaser = rdr.GetBoolean(8)
                });
            }
            return res;
        }
        public static List<Counterparty> GetSuppliers()
        {
            return GetAll().Where(Counterparty => Counterparty.IsSupplier).ToList();
        }
        public static List<Counterparty> GetPurchasers()
        {
            return GetAll().Where(Counterparty => Counterparty.IsPurchaser).ToList();
        }

        public static Counterparty Create(Counterparty counterpartyData)
        {
            throw new NotImplementedException();
        }
        public static void Update(Counterparty counterparty)
        {
            throw new NotImplementedException();
        }
    }
}