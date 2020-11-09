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
using InventoryControl.UserControls;

namespace InventoryControl
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
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
            this.UpdateItems();
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
        public void UpdateItems()
        {
            MainDataGrid.ItemsSource = ProductDatabase.GetProductData();
        }
        private void MakeSearch(string searchString)
        {
            int sortIndex = 0;
            ListSortDirection? direction = null;
            for (int i = 0; i < MainDataGrid.Columns.Count; i++)
            {
                if (MainDataGrid.Columns[i].SortDirection != null)
                {
                    sortIndex = i;
                    direction = MainDataGrid.Columns[i].SortDirection;
                    break;
                }
            }

            var filtered =  new List<ProductData>();
            foreach (ProductData product in ProductDatabase.GetProductData())
            {
                String search = Searchbox.Text.ToLower().Replace('ё', 'е').Trim();
                String title = product.Title.ToLower().Replace('ё', 'е').Trim();

                if (title.Contains(search)) { filtered.Add(product); }
            }
            MainDataGrid.ItemsSource = filtered;
            MainDataGrid.Columns[sortIndex].SortDirection = direction;
        }
        public void SetOrderControl(bool IsEnabled, bool Direction)
        {
            if (!IsEnabled)
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

                MainWindowGrid.Children.Add(new OrderControl());
                MainWindowGrid.Children[2].SetValue(FrameworkElement.NameProperty, "SellingList");
                MainWindowGrid.Children[2].SetValue(Grid.ColumnProperty, 2);

                Console.WriteLine(((OrderControl)MainWindowGrid.Children[2]).Name);
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
        private void LoadBackupButtonClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = AppContext.BaseDirectory+"Backups\\";
            openFileDialog.Filter = "Database (*.db)|*.db|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                SaveBackupButtonClick();
                File.Copy(openFileDialog.FileName, "Database.db", true);
            }
        }
        private void SaveBackupButtonClick(object sender=null, RoutedEventArgs e=null)
        {
            System.IO.Directory.CreateDirectory("Backups");
            File.Copy("Database.db", "Backups\\"+DateTime.Now.ToString("YY-MM-dd HH-mm-dd") + ".db", true);
        }
        private void AddProductClick(object sender, RoutedEventArgs e)
        {
            new UserControls.Windows.EditProductWindow(null).ShowDialog();
            UpdateItems();
        }
        private void EditProductClick(object sender, RoutedEventArgs e)
        {
            var s = (MenuItem)sender;
            var contextMenu = (ContextMenu)s.Parent;
            var row = (DataGridRow)contextMenu.PlacementTarget;
            var id = ((ProductData)MainDataGrid.Items.GetItemAt(row.GetIndex())).Id;
            new UserControls.Windows.EditProductWindow(id).ShowDialog();
            UpdateItems();
        }
        private void DataGridRow_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (MainWindowGrid.Children.Count <= 2)
                return;
            var SellingList = (OrderControl)MainWindowGrid.Children[2];

            var row = ((DataGridRow)sender);
            var id = ((ProductData)row.Item).Id;

            foreach (var saleProduct in SellingList.SaleProducts)
            {
                if (saleProduct.Id == id)
                    return;
            }
            SellingList.SaleProducts.Add(new SaleProductData(id, 1));
        }

        private void SellingView_Click(object sender, RoutedEventArgs e){ SetOrderControl(true, false);}
        private void BuyingView_Click(object sender, RoutedEventArgs e) { SetOrderControl(true, true); }
    }
}