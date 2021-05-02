using InventoryControl.Model;
using InventoryControl.View.Controls;
using InventoryControl.ViewModel;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for TransactionProductsViewer.xaml
    /// </summary>
    public partial class TransactionProductsViewer : ControlPanel
    {
        public TransactionProductsViewer(TransferType type)
        {
            InitializeComponent();
            ((TransactionProductsViewerVM)DataContext).Type = type;
            if (type == TransferType.Transport)
            {
                TransferSpotCombobox1.SelectedIndex = 0;
                TransferSpotCombobox2.SelectedIndex = 1;
            }
            else
            {
                TransferSpotCombobox1.SelectedIndex = 0;
                TransferSpotCombobox2.SelectedIndex = 0;
            }
        }
        public override void ReceiveMessage(object sender, object message)
        {
            if (sender is StorageViewer || sender is ProductDictionaryViewer)
            {
                ((TransactionProductsViewerVM)DataContext).AddProduct((long)message);
            }
        }

        private void MainDataGrid_RowClicked(object sender, MouseButtonEventArgs e, DataGridRow row)
        {
            if (e.ChangedButton == MouseButton.Right && e.ButtonState == MouseButtonState.Released)
            {
                ((TransactionProductsViewerVM)DataContext).ShowContextMenu((TransactionProductPresenter)row.Item);
            }
        }

        private void TransferSpotCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TransferSpotCombobox1.SelectedItem != null && TransferSpotCombobox2.SelectedItem != null)
            {
                if (TransferSpotCombobox1.SelectedItem.Equals(TransferSpotCombobox2.SelectedItem))
                {
                    System.Media.SystemSounds.Hand.Play();
                    ((ComboBox)e.Source).SelectedItem = e.RemovedItems[0];
                }
            }
        }
    }
}
