using System;
using MahApps.Metro.Controls;
using System.Windows;
using System.Windows.Controls;
using InventoryControl.UserControls;
using InventoryControl.UserControls.OrderControl;

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
        }

        private const int OrderControlColumnIndex = 3;
        private void MakeSearch(string searchString)
        {
            throw new NotImplementedException();
            /*DataGridContent.Clear();
            foreach (ProductData product in Database.GetProductData())
            {
                String search = Searchbox.Text.ToLower().Replace('ё', 'е').Trim();
                String title = product.Title.ToLower().Replace('ё', 'е').Trim();

                if (title.Contains(search))
                {
                    DataGridContent.Add(product);
                }
            }*/
        }
        public void SetOrderControl(bool? isBuying)
        {
            if (!isBuying.HasValue)
            {
                ViewsSplitter.Visibility = Visibility.Collapsed;
                try
                {
                    MainWindowGrid.ColumnDefinitions.RemoveAt(OrderControlColumnIndex);
                    MainWindowGrid.Children.RemoveAt(OrderControlColumnIndex);
                }
                catch (IndexOutOfRangeException) { }

                return;
            }
            else if (MainWindowGrid.ColumnDefinitions.Count == OrderControlColumnIndex)
            {
                ViewsSplitter.Visibility = Visibility.Visible;

                var columnDefinition = new ColumnDefinition();
                columnDefinition.Width = new GridLength(1, GridUnitType.Star);
                MainWindowGrid.ColumnDefinitions.Add(columnDefinition);

                MainWindowGrid.Children.Add(new OrderControl(isBuying.Value));
                MainWindowGrid.Children[OrderControlColumnIndex].SetValue(FrameworkElement.NameProperty, "SellingList");
                MainWindowGrid.Children[OrderControlColumnIndex].SetValue(Grid.RowSpanProperty, 2);
                MainWindowGrid.Children[OrderControlColumnIndex].SetValue(Grid.ColumnProperty, OrderControlColumnIndex);
            }
        }
        public OrderControl GetOrderControl()
        {
            if (MainWindowGrid.Children.Count == OrderControlColumnIndex + 1)
            {
                return (OrderControl)MainWindowGrid.Children[OrderControlColumnIndex];
            }
            else
            {
                return null;
            }
        }

        private void SearchBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e) { MakeSearch(Searchbox.Text); }
    }
}