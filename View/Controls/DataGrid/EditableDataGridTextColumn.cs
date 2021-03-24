using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace InventoryControl.View.Controls
{
    public class EditableDataGridTextColumn : DataGridTextColumn
    {
        public override BindingBase Binding 
        { 
            get => base.Binding; 
            set
            {
                var tmp = value;
                if(tmp is Binding)
                {
                    (tmp as Binding).UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                }
                base.Binding = tmp;
            }
        }
    }
}
