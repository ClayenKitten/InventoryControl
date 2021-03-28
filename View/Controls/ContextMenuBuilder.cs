using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace InventoryControl.View.Controls
{
    public class ContextMenuBuilder
    {
        private ContextMenuBuilder parentBuilder;
        private ItemsControl item;

        public ContextMenuBuilder AddAction(string header, Action onClicked)
        {
            var cmb = new ContextMenuBuilder(this, header, isCheckable: false, isEnabled: true);
            ((MenuItem)cmb.item).Click += (sender, e) => { onClicked?.Invoke(); };
            return cmb;
        }
        public ContextMenuBuilder AddCommand(string header, ICommand onClicked)
        {
            var cmb = new ContextMenuBuilder(this, header, isCheckable: false, isEnabled: true);
            ((MenuItem)cmb.item).Click += (sender, e) => { onClicked?.Execute(null); };
            return cmb;
        }
        public ContextMenuBuilder AddCheckable(string header, Action<bool> onClicked)
        {
            var cmb = new ContextMenuBuilder(this, header, isCheckable: true, isEnabled: true);
            ((MenuItem)cmb.item).Click += (sender, e) => { onClicked?.Invoke(((MenuItem)cmb.item).IsChecked); };
            return cmb;
        }

        public ContextMenuBuilder Commit()
        {
            parentBuilder.item.Items.Add(item);
            return parentBuilder;
        }
        public ContextMenu Build()
        {
            var ctxMenuBuilder = this;
            while (!(ctxMenuBuilder.item is ContextMenu))
            {
                ctxMenuBuilder = ctxMenuBuilder.parentBuilder;
            }
            return (ContextMenu)ctxMenuBuilder.item;
        }

        private ContextMenuBuilder(ContextMenuBuilder parent, string header, bool isCheckable, bool isEnabled)
        {
            parentBuilder = parent;
            item = new MenuItem()
            {
                Header = header,
                IsCheckable = isCheckable,
                StaysOpenOnClick = isCheckable,
                IsEnabled = isEnabled,
            };
        }
        public ContextMenuBuilder()
        {
            item = new ContextMenu();
        }
    }
}
