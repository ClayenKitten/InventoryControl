using System;
using System.Collections.ObjectModel;
using MahApps.Metro.Controls;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using System.IO;
using Microsoft.Win32;
using System.Linq;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;
using InventoryControl.UserControls.OrderControl;
using System.Collections.Specialized;
using System.Windows.Data;
using InventoryControl.Util;
using InventoryControl.Data;

namespace InventoryControl
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public ObservableCollection<ProductData> DataGridContent { get; set; }
        
        public MainWindow()
        {
            DataGridContent = new ObservableCollection<ProductData>(Database.GetProductData());
            InitializeComponent();
            var firstColumn = GenerateColumn("N", "Номер по прайсу", "Id", "3*");
            firstColumn.SortDirection = ListSortDirection.Ascending;
            MainDataGrid.Columns.Add(firstColumn);
            MainDataGrid.Columns.Add(GenerateColumn("Наименование", "Наименование", "Title", "12*"));
            MainDataGrid.Columns.Add(GenerateColumn("Кол.", "Количество упаковок", "Number", "2*"));
            MainDataGrid.Columns.Add(GenerateColumn("Вес", "Вес упаковки", "Weight", "60"));
            MainDataGrid.Columns.Add(GenerateColumn("Ед", "Единица измерения (тип упаковки)", "Packing", "45"));
            MainDataGrid.Columns.Add(GenerateColumn("Закуп. цена", "Закупочная цена", "PurchasePrice", "4*"));
            MainDataGrid.Columns.Add(GenerateColumn("Прод. цена", "Продажная цена", "SalePrice", "4*"));

            Database.DatabaseChanged += (object sender, EventArgs e) =>
            {
                DataGridContent.Clear();
                foreach (var productData in Database.GetProductData())
                {
                    DataGridContent.Add(productData);
                }
            };
        }
        private DataGridColumn GenerateColumn(String headerTitle, String headerTooltip, String Id, String width)
        {
            DataGridTemplateColumn column = new DataGridTemplateColumn();
            column.Header = headerTitle;
            column.HeaderStyle = new Style(typeof(DataGridColumnHeader));
            column.HeaderStyle.BasedOn = (Style)FindResource("MahApps.Styles.DataGridColumnHeader");
            column.HeaderStyle.Setters.Add(new Setter(ToolTipService.ToolTipProperty, headerTooltip));
            column.HeaderStyle.Setters.Add(new Setter(HorizontalContentAlignmentProperty, HorizontalAlignment.Center));
            column.HeaderStyle.Setters.Add(new Setter(TextBlock.TextWrappingProperty, TextWrapping.Wrap));

            column.SetValue(TextBlock.TextWrappingProperty, TextWrapping.Wrap);

            var widthConverter = new DataGridLengthConverter();
            column.Width = (DataGridLength)widthConverter.ConvertFromString(width);

            var CellDataTemplate = @"
            <DataTemplate
                xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation"" xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml"" xmlns:util=""clr-namespace:InventoryControl.Util;assembly=InventoryControl"">
                <util:HighlightTextBlock Text = ""{Binding " + Id + @"}"" HighlightPhrase=""{Binding ElementName=Searchbox, Path=Text}"" HighlightBrush=""Orange""/>
            </DataTemplate>";
            column.CellTemplate = (DataTemplate)XamlReader.Parse(CellDataTemplate);
            column.SortMemberPath = Id;

            return column;
        }
        private void MakeSearch(string searchString)
        {
            DataGridContent.Clear();
            foreach (ProductData product in Database.GetProductData())
            {
                String search = Searchbox.Text.ToLower().Replace('ё', 'е').Trim();
                String title = product.Title.ToLower().Replace('ё', 'е').Trim();

                if (title.Contains(search))
                {
                    DataGridContent.Add(product);
                }
            }
        }
        public void SetOrderControl(bool? isBuying)
        {
            if (!isBuying.HasValue)
            {
                ViewsSplitter.Visibility = Visibility.Collapsed;
                try
                {
                    MainWindowGrid.ColumnDefinitions.RemoveAt(2);
                    MainWindowGrid.Children.RemoveAt(2);
                }
                catch (IndexOutOfRangeException) { }

                return;
            }
            else if (MainWindowGrid.ColumnDefinitions.Count == 2)
            {
                ViewsSplitter.Visibility = Visibility.Visible;

                var columnDefinition = new ColumnDefinition();
                columnDefinition.Width = new GridLength(1, GridUnitType.Star);
                MainWindowGrid.ColumnDefinitions.Add(columnDefinition);

                MainWindowGrid.Children.Add(new OrderControl(isBuying.Value));
                MainWindowGrid.Children[2].SetValue(FrameworkElement.NameProperty, "SellingList");
                MainWindowGrid.Children[2].SetValue(Grid.ColumnProperty, 2);
            }
        }
        public OrderControl GetOrderControl()
        {
            if (MainWindowGrid.Children.Count == 3)
            {
                return (OrderControl)MainWindowGrid.Children[2];
            }
            else
            {
                return null;
            }
        }

        private void SearchBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e) { MakeSearch(Searchbox.Text); }
        private void EditProductClick(object sender, RoutedEventArgs e)
        {
            var s = (MenuItem)sender;
            var contextMenu = (ContextMenu)s.Parent;
            var row = (DataGridRow)contextMenu.PlacementTarget;
            var id = ((ProductData)MainDataGrid.Items.GetItemAt(row.GetIndex())).Id;
            new UserControls.Windows.EditProductWindow(id).ShowDialog();
        }
        private void DataGridRow_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (MainWindowGrid.Children.Count <= 2)
                return;
            var orderControl = (OrderControl)MainWindowGrid.Children[2];

            var row = ((DataGridRow)sender);
            var id = ((ProductData)row.Item).Id;

            foreach (var saleProduct in orderControl.OrderProducts)
            {
                if (saleProduct.Id == id)
                    return;
            }
            orderControl.OrderProducts.Add(new OrderProductData(id, GetOrderControl().IsBuying));
        }
    }
}