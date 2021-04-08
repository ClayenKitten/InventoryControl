using InventoryControl.Model;
using InventoryControl.Util;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using InventoryControl.ViewModel;
using System.Linq;
using System.Windows.Documents;
using System.Collections.Specialized;
using System.ComponentModel;

namespace InventoryControl.View.Controls
{
    public class AdvancedDataGrid : DataGrid, IDisposable, INotifyPropertyChanged, IValidatable
    {
        #region Dependency properties
        public int TargetColumnIndex { get; set; }
        public static DependencyProperty TargetColumnIndexProperty =
            DependencyProperty.Register("TargetColumnIndex", typeof(int), typeof(AdvancedDataGrid));

        public string FilterString { get; set; }
        public static DependencyProperty FilterStringProperty =
            DependencyProperty.Register("FilterString",
            typeof(string), typeof(AdvancedDataGrid),
            new PropertyMetadata(""));

        public string EmptyHint { get; set; }
        public static DependencyProperty EmptyHintProperty =
            DependencyProperty.Register("EmptyHint",
            typeof(string), typeof(AdvancedDataGrid),
            new PropertyMetadata(""));

        // Validation
        public bool InvalidateOnEmpty { get; set; }
        public static DependencyProperty InvalidateOnEmptyProperty =
            DependencyProperty.Register("InvalidateOnEmpty", typeof(bool), typeof(AdvancedDataGrid));

        public Action<DataGridRowEditEndingEventArgs> RowEditEndHandler { get; set; }
        public static DependencyProperty RowEditEndHandlerProperty =
            DependencyProperty.Register("RowEditEndHandler", typeof(Action<DataGridRowEditEndingEventArgs>), typeof(AdvancedDataGrid));

        public ViewOptions Options { get; set; }
        public static DependencyProperty OptionsProperty =
            DependencyProperty.Register("Options", typeof(ViewOptions), typeof(AdvancedDataGrid),
            new PropertyMetadata(new ViewOptions()));
        #endregion

        static AdvancedDataGrid()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AdvancedDataGrid),
                new FrameworkPropertyMetadata(typeof(AdvancedDataGrid)));
        }
        public AdvancedDataGrid() : base()
        {
            Items.Filter = (obj) =>
            {
                bool passed = true;
                // Search check
                if (obj is INamed)
                {
                    passed &= (obj as INamed).Name.Includes(GetValue(FilterStringProperty).ToString());
                }
                // Options check
                passed &= ((ViewOptions)GetValue(OptionsProperty)).Filter(obj);
                return passed;                
            };
            MouseUp += OnMouseClicked;
            MouseDown += OnMouseClicked;
        }
        public void Dispose()
        {
            MouseUp -= OnMouseClicked;
        }
        public bool IsValid
        {
            get
            {
                if ((bool)GetValue(InvalidateOnEmptyProperty) && Items.Count == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (e.Property == FilterStringProperty)
            {
                Items.Filter = Items.Filter;
            }
            if (e.Property == OptionsProperty)
            {
                Items.GroupDescriptions.Clear();
                if (((ViewOptions)e.NewValue).Group != "")
                {
                    Items.GroupDescriptions.Add(new PropertyGroupDescription(((ViewOptions)e.NewValue).Group));
                }
                foreach (var column in Columns)
                {
                    if (column is DataGridBoundColumn)
                    {
                        var itemSourceType = ItemsSource.GetType().GetGenericArguments().Single();
                        column.Visibility = ((ViewOptions)GetValue(OptionsProperty))
                            .ColumnVisibility(itemSourceType, column as DataGridBoundColumn);
                    }
                }
                Items.Filter = Items.Filter;                
            }
            if (e.Property == EmptyHintProperty)
            {
                adorner?.SetText((string)GetValue(EmptyHintProperty));
            }
        }
        protected override void OnItemsChanged(NotifyCollectionChangedEventArgs e)
        {
            base.OnItemsChanged(e);
            if (Items.Count > 0)
            {
                adorner?.SetText("");
            }
            else
            {
                adorner?.SetText((string)GetValue(EmptyHintProperty));
            }
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsValid"));
        }
        protected override void OnRowEditEnding(DataGridRowEditEndingEventArgs e)
        {
            base.OnRowEditEnding(e);
            ((Action<DataGridRowEditEndingEventArgs>)GetValue(RowEditEndHandlerProperty))?.Invoke(e);
        }
        private DataGridAdorner adorner;
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            adorner = new DataGridAdorner(this, (string)GetValue(EmptyHintProperty));
            AdornerLayer.GetAdornerLayer(this).Add(adorner);
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
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
