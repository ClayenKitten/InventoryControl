using InventoryControl.Model;
using InventoryControl.Model.Product;
using InventoryControl.Model.Storage;
using InventoryControl.Model.Util;
using InventoryControl.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
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

namespace InventoryControl.View
{
    /// <summary>
    /// Interaction logic for StorageViewer.xaml
    /// </summary>
    public partial class StorageViewer : UserControl
    {
        public StorageViewer()
        {
            InitializeComponent();
        }
        public StorageViewer(int storageId) : this()
        {
            ((StorageViewerVM)this.DataContext).StorageId = storageId;
        }

        private void CollectionViewSource_Filter(object sender, FilterEventArgs e)
        {
            var searchString = ((StorageViewerVM)this.DataContext).SearchString;

            string productName = ((ProductPresenter)e.Item).Name.ToLower().Replace('ё', 'е');
            searchString = searchString.ToLower().Replace('ё', 'е');

            e.Accepted = productName.Contains(searchString);
        }

        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var row = ((DataGridRow)sender);
            var id = ((StockProductPresenter)row.Item).Id;

            GlobalCommands.SendProduct.Execute(id);
        }
    }
}
