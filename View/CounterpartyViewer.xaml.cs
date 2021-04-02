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
            InputForm.Confirmed += (_, _1) => { ConfirmForm(); };
        }

        private void ConfirmForm()
        {
            CounterpartyMapper.Create(new Counterparty
            (
                id: -1,
                name: NameTB.Text,
                address: AddressTB.Text,
                contacts: ContactsTB.Text,
                taxpayerNumber: TaxpayerTB.Text,
                accountingCode: AccountTB.Text,
                bankDetails: BankIdTB.Text,
                role: ((CounterpatyViewerVM)DataContext).ShowPurchasers ? 0 : 1
            ));
            InputForm.ClearAllTextBoxes();
            GlobalCommands.ModelUpdated.Execute(null);
        }

        private void DeleteButtonClick(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show
                (
                    "Вы действительно хотите удалить контрагента? Эта операция необратима.",
                    "Удалить контрагента",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question,
                    MessageBoxResult.No
                ) == MessageBoxResult.No)
            {
                return;
            }
            var dc = ((FrameworkElement)sender).DataContext;
            if(!Counterparty.Table.TryDelete((dc as ORM.IEntity).Id))
            {
                MessageBox.Show
                (
                    "Контрагент является поставщиком продукта\nЗамените поставщика и повторите",
                    "Удаление невозможно",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
            }
            GlobalCommands.ModelUpdated.Execute(null);
        }
    }
}
