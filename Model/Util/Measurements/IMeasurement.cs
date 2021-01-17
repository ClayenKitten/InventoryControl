using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryControl.Model.Util
{
    public interface IMeasurement
    {
        public string ToString();
        public string GetRawValue();
        public string GetFormattedValue();
        public string GetPostfix();

        public Unit GetUnit();
    }
}
