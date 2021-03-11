using System;
using System.Collections.Generic;

namespace InventoryControl.Model
{
    public class CounterpartyMapper
    {
        public static CounterpartyData Read(int id)
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
    }
}
