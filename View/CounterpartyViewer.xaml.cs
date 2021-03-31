using InventoryControl.Model;
using InventoryControl.View.Controls;
using InventoryControl.ViewModel;
using System.Windows;

namespace InventoryControl.View
{
    /// <summary>
    /// Interaction logic for CounterpartyViewer.xaml
    /// </summary>
    public partial class CounterpartyViewer : ControlPanel
    {
        public CounterpartyViewer(bool showPurchasers)
        {            
            InitializeComponent();
            ((CounterpatyViewerVM)DataContext).ShowPurchasers = showPurchasers;
        }

        private void AddButtonClick(object sender, RoutedEventArgs e)
        {
            if(((CounterpatyViewerVM)DataContext).ShowPurchasers)
            {
                CounterpartyMapper.Create(new Purchaser
                (
                    id: -1,
                    name: NameTB.Text,
                    address: AddressTB.Text,
                    contacts: ContactsTB.Text,
                    taxpayerNumber: TaxpayerTB.Text,
                    accountingCode: AccountTB.Text,
                    bankDetails: BankIdTB.Text
                ));
            }
            else
            {
                CounterpartyMapper.Create(new Supplier
                (
                    id: -1,
                    name: NameTB.Text,
                    address: AddressTB.Text,
                    contacts: ContactsTB.Text,
                    taxpayerNumber: TaxpayerTB.Text,
                    accountingCode: AccountTB.Text,
                    bankDetails: BankIdTB.Text
                ));
            }
            GlobalCommands.ModelUpdated.Execute(null);
        }
    }
}
