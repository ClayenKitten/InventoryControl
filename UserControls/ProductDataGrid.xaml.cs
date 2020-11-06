using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InventoryControl.UserControls
{
    /// <summary>
    /// Interaction logic for ProductDataGrid.xaml
    /// </summary>
    public partial class ProductDataGrid : UserControl
    {
        public List<ProductData> Products = new List<ProductData>();

        public ProductDataGrid()
        {
            InitializeComponent();

            for (int z = 0; z < 17; z++)
            {
                Products.Add(ProductDatabase.GetProductData(z));
            }
            MainDataGrid.ItemsSource = Products;

            var firstColumn = GenerateColumn("N", "Номер по прайсу", "Id", "3*");
            firstColumn.SortDirection = ListSortDirection.Ascending;
            MainDataGrid.Columns.Add(firstColumn);
            MainDataGrid.Columns.Add(GenerateColumn("Наименование", "Наименование", "Title", "12*"));
            MainDataGrid.Columns.Add(GenerateColumn("Кол.", "Количество", "Number", "2*"));
            MainDataGrid.Columns.Add(GenerateColumn("Вес", "Вес", "Weight", "3*"));
            MainDataGrid.Columns.Add(GenerateColumn("Ед. изм.", "Единица измерения", "Measurement", "3*"));
            MainDataGrid.Columns.Add(GenerateColumn("Закуп. цена", "Закупочная цена", "PurchasePrice", "4*"));
            MainDataGrid.Columns.Add(GenerateColumn("Прод. цена", "Продажная цена", "SalePrice", "4*"));

            MainDataGrid.Columns[0].DisplayIndex = MainDataGrid.Columns.Count - 1;
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

        private void SendButtonClicked(object sender, RoutedEventArgs e)
        {
            var t = (MainWindow)this.Parent;
            t.SendButtonClicked(sender, e);
        }
    }
}
