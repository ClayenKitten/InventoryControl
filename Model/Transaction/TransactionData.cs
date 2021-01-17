using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryControl.Model.Transaction
{
    enum TransactionDirection
    {
        Sell,
        Buy,
    }
    class TransactionData
    {
        public DateTime DateTime { get; set; }
        public TransactionDirection Direction { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public List<int> ProductsIds { get; set; }

        public TransactionData(DateTime dateTime, TransactionDirection direction, string from, string to, List<int> productIds)
        {
            DateTime = dateTime;
            Direction = direction;
            From = from;
            To = to;
            ProductsIds = productIds;
        }
    }
}
