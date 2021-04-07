using InventoryControl.Model;
using InventoryControl.View.Controls;
using InventoryControl.ViewModel;
using System.Windows;

namespace InventoryControl.View
{
    /// <summary>
    /// Interaction logic for EditOrganizationPanel.xaml
    /// </summary>
    public partial class EditOrganizationPanel : ControlPanel
    {
        public EditOrganizationPanel()
        {            
            InitializeComponent();
        }

        private void ConfirmForm()
        {
            //CounterpartyMapper.Update(new Counterparty
            //(
            //    id: 0,
            //    name: NameTB.Text,
            //    address: AddressTB.Text,
            //    contacts: ContactsTB.Text,
            //    taxpayerNumber: TaxpayerTB.Text,
            //    accountingCode: AccountTB.Text,
            //    bankDetails: BankIdTB.Text,
            //    role: ((CounterpatyViewerVM)DataContext).ShowPurchasers ? 0 : 1
            //));
            GlobalCommands.ModelUpdated.Execute(null);
        }
    }
}
