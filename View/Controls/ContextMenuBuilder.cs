using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;

namespace InventoryControl.View.Controls
{
    public class ContextMenuBuilder
    {
        private ContextMenuBuilder parentBuilder;
        private ItemsControl item;

        private List<Tuple<string, MenuItem, Action<bool>>> radioGroups = new List<Tuple<string, MenuItem, Action<bool>>>();

        public ContextMenuBuilder BeginGroup(string header)
            => new ContextMenuBuilder(this, header);
        public ContextMenuBuilder EndGroup()
        {
            parentBuilder.item.Items.Add(item);
            return parentBuilder;
        }

        public ContextMenuBuilder Check(bool isChecked = true)
        {
            ((MenuItem)item.Items.GetItemAt(item.Items.Count - 1)).IsChecked = isChecked;
            return this;
        }

        public ContextMenuBuilder AsRadioGroup()
        {
            foreach (var clicked in radioGroups)
            {
                clicked.Item2.Checked += (_, _1) =>
                {
                    if (!clicked.Item2.IsChecked)
                    {
                        clicked.Item2.IsChecked = true;
                        return;
                    }
                    foreach (var radioGroup in radioGroups)
                    {
                        if (radioGroup.Item1 == clicked.Item1)
                        {
                            radioGroup.Item2.IsChecked = radioGroup.Item2 == clicked.Item2;
                            radioGroup.Item3?.Invoke(radioGroup.Item2.IsChecked);
                        }
                    }
                };
            }
            return this;
        }

        public ContextMenuBuilder AddAction(string header, Action onClicked, bool isEnabled = true)
        {
            var menuItem = new MenuItem() { Header = header, IsEnabled = isEnabled };
            menuItem.Click += (sender, e) => { onClicked?.Invoke(); };
            item.Items.Add(menuItem);
            return this;
        }
        public ContextMenuBuilder AddCommand(string header, ICommand onClicked, bool isEnabled = true)
        {
            var menuItem = new MenuItem() { Header = header, IsEnabled = isEnabled };
            menuItem.Click += (sender, e) => { onClicked?.Execute(null); };
            item.Items.Add(menuItem);
            return this;
        }
        public ContextMenuBuilder AddCheckable(string header, Action<bool> onClicked, bool isEnabled = true)
        {
            var menuItem = new MenuItem() 
            {
                Header = header,
                IsCheckable = true,
                StaysOpenOnClick = true,
                IsEnabled = isEnabled
            };
            menuItem.Click += (sender, e) => { onClicked?.Invoke(menuItem.IsChecked); };
            item.Items.Add(menuItem);
            return this;
        }

        public ContextMenuBuilder AddRadio(string header, string radioGroup, Action<bool> OnSet, bool isEnabled = true)
        {
            var menuItem = new MenuItem()
            {
                Header = header,
                IsCheckable = true,
                StaysOpenOnClick = true,
                IsEnabled = isEnabled,
            };
            radioGroups.Add(new Tuple<string, MenuItem, Action<bool>>(radioGroup, menuItem, OnSet));
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
