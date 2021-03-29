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
        public static Purchaser Get(int id)
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
                if (rdr.GetInt32(7) == 0)
                    res.Add(new Purchaser()
                    {
                        Id = rdr.GetInt32(0),
                        Name = rdr.GetStringOrEmpty(1),
                        Address = rdr.GetStringOrEmpty(2),
                        Contacts = rdr.GetStringOrEmpty(3),
                        TaxpayerNumber = rdr.GetStringOrEmpty(4),
                        AccountingCode = rdr.GetStringOrEmpty(5),
                        BankDetails = rdr.GetStringOrEmpty(6),
                    });
                else if (rdr.GetInt32(7) == 1) 
                    res.Add(new Supplier()
                    {
                        Id = rdr.GetInt32(0),
                        Name = rdr.GetStringOrEmpty(1),
                        Address = rdr.GetStringOrEmpty(2),
                        Contacts = rdr.GetStringOrEmpty(3),
                        TaxpayerNumber = rdr.GetStringOrEmpty(4),
                        AccountingCode = rdr.GetStringOrEmpty(5),
                        BankDetails = rdr.GetStringOrEmpty(6),
                    });
            }
            return res;
        }

        public static List<Supplier> GetSuppliers()
        {
            return GetAll()
                .Where(Counterparty => Counterparty is Supplier)
                .Cast<Supplier>()
                .ToList();
        }
        public static List<Purchaser> GetPurchasers()
        {
            return GetAll()
                .Where(Counterparty => Counterparty is Purchaser)
                .Cast<Purchaser>()
                .ToList();
        }

        public static Counterparty Create(Counterparty counterparty)
        {
            const string commandText =
            @"
                INSERT INTO Counterparty(Name, Address, Contacts, TaxpayerNumber, AccountingCode, BankDetails, Role)
                VALUES($name, $address, $contacts, $taxpayerNumber, $accountingCode, $bankDetails, $role);

                SELECT Id FROM Counterparty WHERE ROWID = last_insert_rowid();
            ";
            counterparty.Id = (int)(long)Database.CommitScalarTransaction(commandText,
                new SQLiteParameter("$name", counterparty.Name),
                new SQLiteParameter("$address", counterparty.Address),
                new SQLiteParameter("$contacts", counterparty.Contacts),
                new SQLiteParameter("$taxpayerNumber", counterparty.TaxpayerNumber),
                new SQLiteParameter("$accountingCode", counterparty.AccountingCode),
                new SQLiteParameter("$bankDetails", counterparty.BankDetails),
                new SQLiteParameter("$role", counterparty is Purchaser ? 0 : 1)
            );
            return counterparty;
        }
        public static void Update(Counterparty counterparty)
        {
            const string commandText =
            @"
                UPDATE Counterparty SET

                Name=$name,
                Address=$address,
                Contacts=$contacts,
                TaxpayerNumber=$taxpayerNumber,
                AccountingCode=$accountingCode,
                BankDetails=$bankDetails,
                Role=$role
                WHERE Id=$id;
            ";
            Database.CommitNonQueryTransaction(commandText,
                new SQLiteParameter("$name", counterparty.Name),
                new SQLiteParameter("$address", counterparty.Address),
                new SQLiteParameter("$contacts", counterparty.Contacts),
                new SQLiteParameter("$taxpayerNumber", counterparty.TaxpayerNumber),
                new SQLiteParameter("$accountingCode", counterparty.AccountingCode),
                new SQLiteParameter("$bankDetails", counterparty.BankDetails),
                new SQLiteParameter("$role", counterparty is Purchaser ? 0 : 1),
                new SQLiteParameter("$id", counterparty.Id)
            );

        }
    }
}