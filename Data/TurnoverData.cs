using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryControl.Data
{
    enum TurnoverDirection
    {
        Sell,
        Buy,
    }
    class TurnoverData
    {
        public DateTime DateTime { get; set; }
        public TurnoverDirection Direction { get; set; }
        public String From { get; set; }
        public String To { get; set; }
        public List<Int32> ProductsIds { get; set; }

        public TurnoverData(DateTime dateTime, TurnoverDirection direction, String from, String to, List<Int32> productIds)
        {
            DateTime = dateTime;
            Direction = direction;
            From = from;
            To = to;
            ProductsIds = productIds;
        }
    }
}
