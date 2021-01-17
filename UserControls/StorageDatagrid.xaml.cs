using InventoryControl.Model;
using InventoryControl.Model.Product;
using InventoryControl.Model.Storage;
using InventoryControl.Panel;
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


        public int StorageId { get; set; }
        public static readonly DependencyProperty StorageIdDependencyProperty =
            DependencyProperty.Register("StorageId", typeof(int),
            typeof(StorageDatagrid));

        public String HighlightPhrase { get; set; }
        public static readonly DependencyProperty HighlightPhraseProperty =
            DependencyProperty.Register("HighlightPhrase", typeof(string),
            typeof(StorageDatagrid));

        public StorageDatagrid()
        {
            DataGridContent = new ObservableCollection<ProductData>(StorageDataMapper.GetProductsInStorage(this.StorageId));
            InitializeComponent();
            
            Database.DatabaseChanged += (object sender, EventArgs e) =>
            {
                DataGridContent.Clear();
                foreach (var productData in StorageDataMapper.GetProductsInStorage(this.StorageId))
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
            var id = ((ProductData)row.Item).id;

            foreach (var saleProduct in orderControl.OrderProducts)
            {
                if (saleProduct.Id == id)
                    return;
            }
            orderControl.OrderProducts.Add(new OrderProductData(id));
        }
        private void EditProductClick(object sender, RoutedEventArgs e)
        {
            var contextMenu = (ContextMenu)((MenuItem)sender).Parent;
            var row = (DataGridRow)contextMenu.PlacementTarget;
            var id = ((ProductData)MainDataGrid.Items.GetItemAt(row.GetIndex())).id;

            ((MainWindow)App.Current.MainWindow).SetPanel(new EditProductPanel(id));
        }
    }
}
