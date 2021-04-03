using System.Windows.Data;

namespace InventoryControl.View.Controls
{
    public class EditableDataGridHighlightColumn : HighlightColumn
    {
        public override BindingBase Binding
        {
            get => base.Binding;
            set
            {
                var tmp = value;
                if (tmp is Binding)
                {
                    (tmp as Binding).UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                }
                base.Binding = tmp;
            }
        }
    }
}
