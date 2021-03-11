using InventoryControl.Model;
using InventoryControl.ViewModel;
using System.Windows.Controls;
using System.Windows.Input;

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
        public StorageViewer(StorageViewerOptions options) : this()
        {
            dataContext.Options = options;
            GlobalCommands.ModelUpdated.Execute(null);
        }
        public StorageViewer(int storageId) : this(new StorageViewerOptions(storageId)) {}
        
        private StorageViewerVM dataContext { get { return (StorageViewerVM)DataContext; } }
        

        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var row = ((DataGridRow)sender);
            var id = ((StockProductPresenter)row.Item).Id;

            GlobalCommands.SendProduct.Execute(id);
        }
    }
}
