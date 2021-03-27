using InventoryControl.Model;
using InventoryControl.Util;
using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace InventoryControl.View.Controls
{
    public class AdvancedDataGrid : DataGrid
    {
        public int TargetColumnIndex { get; set; }
        public static DependencyProperty TargetColumnIndexProperty =
            DependencyProperty.Register("TargetColumnIndex", typeof(int), typeof(AdvancedDataGrid));

        public string FilterString { get; set; }
        public static DependencyProperty FilterStringProperty =
            DependencyProperty.Register("FilterString", typeof(string), typeof(AdvancedDataGrid));
            
        public string GroupingPropertyPath { get; set; }
        public static DependencyProperty GroupingPropertyPathProperty =
            DependencyProperty.Register("GroupingPropertyPath", typeof(string), typeof(AdvancedDataGrid));

        public Action<DataGridRowEditEndingEventArgs> RowEditEndHandler { get; set; }
        public static DependencyProperty RowEditEndHandlerProperty =
            DependencyProperty.Register("RowEditEndHandler", typeof(Action<DataGridRowEditEndingEventArgs>), typeof(AdvancedDataGrid));


        static AdvancedDataGrid()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AdvancedDataGrid),
                new FrameworkPropertyMetadata(typeof(AdvancedDataGrid)));
        }
        public AdvancedDataGrid() : base()
        {
            Items.Filter = (obj) =>
            {
                if (obj is INamed)
                {
                    return (obj as INamed).Name.Includes(GetValue(FilterStringProperty).ToString());
                }
                return true;
            };
            MouseUp += OnMouseClicked;
        }
        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (e.Property == FilterStringProperty)
            {
                Items.Filter = Items.Filter;
            }
            if (e.Property == GroupingPropertyPathProperty && (string)e.NewValue != "")
            {
                Items.GroupDescriptions.Remove(new PropertyGroupDescription((string)e.OldValue));
                Items.GroupDescriptions.Add(new PropertyGroupDescription((string)e.NewValue));
            }
        }
        protected override void OnRowEditEnding(DataGridRowEditEndingEventArgs e)
        {
            base.OnRowEditEnding(e);
            ((Action<DataGridRowEditEndingEventArgs>)GetValue(RowEditEndHandlerProperty))?.Invoke(e);
        }

        private void OnMouseClicked(object sender, MouseButtonEventArgs e)
        {
            DependencyObject dep = (DependencyObject)e.OriginalSource;
            while ((dep != null) && !(dep is DataGridCell || dep is DataGridColumnHeader))
            {
                if (dep is Visual)
                    dep = VisualTreeHelper.GetParent(dep);
                else
                    dep = LogicalTreeHelper.GetParent(dep);
            }

            if (dep == null)
                return;

            if (dep is DataGridColumnHeader)
            {
                HeaderClicked?.Invoke(this, e, dep as DataGridColumnHeader);
            }
            if (dep is DataGridCell)
            {
                CellClicked?.Invoke(this, e, dep as DataGridCell);
                while ((dep != null) && !(dep is DataGridRow))
                {
                    dep = VisualTreeHelper.GetParent(dep);
                }
                RowClicked?.Invoke(this, e, dep as DataGridRow);
            }
        }

        //Click events
        public delegate void HeaderClickedEventHandler(object sender, MouseButtonEventArgs e, DataGridColumnHeader header);
        public event HeaderClickedEventHandler HeaderClicked;
        public delegate void RowClickedEventHandler(object sender, MouseButtonEventArgs e, DataGridRow row);
        public event RowClickedEventHandler RowClicked;
        public delegate void CellClickedEventHandler(object sender, MouseButtonEventArgs e, DataGridCell cell);
        public event CellClickedEventHandler CellClicked;
    }
}
