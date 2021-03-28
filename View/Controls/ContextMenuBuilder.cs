using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace InventoryControl.View.Controls
{
    public class ContextMenuBuilder
    {
        private ContextMenuBuilder parentBuilder;
        private ItemsControl item;

        public ContextMenuBuilder BeginGroup(string header)
            => new ContextMenuBuilder(this, header);
        public ContextMenuBuilder EndGroup()
        {
            parentBuilder.item.Items.Add(item);
            return parentBuilder;
        }

        public ContextMenuBuilder AddAction(string header, Action onClicked)
        {
            var menuItem = new MenuItem() { Header = header };
            menuItem.Click += (sender, e) => { onClicked?.Invoke(); };
            item.Items.Add(menuItem);
            return this;
        }
        public ContextMenuBuilder AddCommand(string header, ICommand onClicked)
        {
            var menuItem = new MenuItem() { Header = header };
            menuItem.Click += (sender, e) => { onClicked?.Execute(null); };
            item.Items.Add(menuItem);
            return this;
        }
        public ContextMenuBuilder AddCheckable(string header, Action<bool> onClicked)
        {
            var menuItem = new MenuItem() { Header = header, IsCheckable = true, StaysOpenOnClick = true };
            menuItem.Click += (sender, e) => { onClicked?.Invoke(menuItem.IsChecked); };
            item.Items.Add(menuItem);
            return this;
        }
        public ContextMenuBuilder AddSeparator()
        {
            item.Items.Add(new Separator());
            return this;
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

        private ContextMenuBuilder(ContextMenuBuilder parent, string header)
        {
            parentBuilder = parent;
            item = new MenuItem() { Header = header };
        }
        public ContextMenuBuilder()
        {
            parentBuilder = null;
            item = new ContextMenu();
        }
    }
}
