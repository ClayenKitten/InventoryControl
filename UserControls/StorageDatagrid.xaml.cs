using InventoryControl.Data;
using InventoryControl.UserControls;
using InventoryControl.UserControls.OrderControl;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InventoryControl.UserControls
{
    /// <summary>
    /// Interaction logic for StorageDatagrid.xaml
    /// </summary>
    public partial class StorageDatagrid : UserControl
    {
        public ObservableCollection<ProductData> DataGridContent { get; set; }
        public String HighlightPhrase { get; set; }

        public static readonly DependencyProperty HighlightPhraseProperty =
            DependencyProperty.Register("HighlightPhrase", typeof(string),
            typeof(StorageDatagrid), new PropertyMetadata(""));

        public StorageDatagrid()
        {
            DataGridContent = new ObservableCollection<ProductData>(Database.GetProductData());
            InitializeComponent();
            
            Database.DatabaseChanged += (object sender, EventArgs e) =>
            {
                DataGridContent.Clear();
                foreach (var productData in Database.GetProductData())
                {
                    DataGridContent.Add(productData);
                }
            };
        }
        private void DataGridRow_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var MainWindow = (MainWindow)App.Current.MainWindow;    

            if (MainWindow.MainWindowGrid.Children.Count <= 2)
                return;
            var orderControl = (OrderControl.OrderControl)MainWindow.MainWindowGrid.Children[2];

            var row = ((DataGridRow)sender);
            var id = ((ProductData)row.Item).Id;

            foreach (var saleProduct in orderControl.OrderProducts)
            {
                if (saleProduct.Id == id)
                    return;
            }
            orderControl.OrderProducts.Add(new OrderProductData(id, MainWindow.GetOrderControl().IsBuying));
        }
        private void EditProductClick(object sender, RoutedEventArgs e)
        {
            var s = (MenuItem)sender;
            var contextMenu = (ContextMenu)s.Parent;
            var row = (DataGridRow)contextMenu.PlacementTarget;
            var id = ((ProductData)MainDataGrid.Items.GetItemAt(row.GetIndex())).Id;
            new UserControls.Windows.EditProductWindow(id).ShowDialog();
        }
    }
}
