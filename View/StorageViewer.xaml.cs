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
        public StorageViewer(int storageId, bool isDriven) : this()
        {
            if (isDriven)
                dataContext.Options = new StorageViewerOptions(storageId)
                {
                    IsSelectorReadOnly = true,
                    ShowOutOfStockProducts = false,
                    GroupOutOfStockProducts = false,
                    ShowOptionsSettings = false
                };
            else
                dataContext.Options = new StorageViewerOptions(storageId);
        }
        public StorageViewer(int storageId) : this(storageId, false) {}
        
        private StorageViewerVM dataContext { get { return (StorageViewerVM)DataContext; } }
        

        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var row = ((DataGridRow)sender);
            var id = ((StockProductPresenter)row.Item).Id;

            GlobalCommands.SendProduct.Execute(id);
        }
    }
}
