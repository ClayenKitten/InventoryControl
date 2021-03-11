using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryControl.Model.Counterparty
{
    public class CounterpartyData
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Contacts{ get; set; }
        public string TaxpayerNumber { get; set; }
        public string AccountingCode { get; set; }
        public string BankDetails { get; set; }

        public bool IsSupplier { get; set; }
        public bool IsPurchaser{ get; set; }
    }
}
