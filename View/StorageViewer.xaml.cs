using InventoryControl.Model;
using InventoryControl.View.Controls;
using InventoryControl.ViewModel;
using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace InventoryControl.View
{
    /// <summary>
    /// Interaction logic for StorageViewer.xaml
    /// </summary>
    public partial class StorageViewer : ControlPanel
    {
        public StorageViewer()
        {
            InitializeComponent();
        }
        public StorageViewer(StorageViewerOptions options) : this()
        {
            dataContext.Options = options;
            GlobalCommands.ModelUpdated.Execute(null);
        }
        public StorageViewer(int storageId) : this(new StorageViewerOptions(storageId)) {}
        
        private StorageViewerVM dataContext { get { return (StorageViewerVM)DataContext; } }
        
        private void MainDataGrid_RowClicked(object sender, MouseButtonEventArgs e, DataGridRow row)
        {
            if(e.ClickCount == 2)
            {
                var panels = ((MainWindow)App.Current.MainWindow).Panel.ControlPanels;
                panels.Remove(this);
                foreach(var panel in panels)
                {
                    if (panel.DataContext is TransactionProductsViewerVM vm)
                    {
                        vm.AddProduct(((StockProductPresenter)row.Item).Id);
                    }
                }
            }
        }
    }
}
