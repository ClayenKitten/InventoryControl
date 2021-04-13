using InventoryControl.View.Controls;
using static InventoryControl.ViewModel.TransferHistoryViewerVM;

namespace InventoryControl.View
{
    /// <summary>
    /// Interaction logic for TransferHistoryViewer.xaml
    /// </summary>
    public partial class TransferHistoryViewer : ControlPanel
    {
        public TransferHistoryViewer()
        {
            InitializeComponent();
        }

        private void ListView_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            try
            {
                SendMessage(typeof(TransferInvoiceViewer), (e.AddedItems[0] as ProductInvoicePresenter).Item);
            }
            catch(System.Exception) { }
        }
    }
}
