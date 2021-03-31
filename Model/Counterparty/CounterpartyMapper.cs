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
        public static Counterparty Get(int id)
            => Counterparty.Table.Read(id);
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
                res.Add(new Counterparty
                (
                    id: rdr.GetInt32(0),
                    name: rdr.GetStringOrEmpty(1),
                    address: rdr.GetStringOrEmpty(2),
                    contacts: rdr.GetStringOrEmpty(3),
                    taxpayerNumber: rdr.GetStringOrEmpty(4),
                    accountingCode: rdr.GetStringOrEmpty(5),
                    bankDetails: rdr.GetStringOrEmpty(6),
                    role: rdr.GetInt32(7)
                ));
            }
            return res;
        }

        public static List<Counterparty> GetSuppliers()
        {
            return GetAll()
                .Where((x) => x.Role == 1)
                .ToList();
        }
        public static List<Counterparty> GetPurchasers()
        {
            return GetAll()
                .Where((x) => x.Role == 0)
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
                new SQLiteParameter("$role", counterparty.Role)
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
                new SQLiteParameter("$role", counterparty.Role),
                new SQLiteParameter("$id", counterparty.Id)
            );

        }
    }
}