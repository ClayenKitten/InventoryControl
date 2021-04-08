using InventoryControl.Model;
using InventoryControl.View.Controls;
using InventoryControl.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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

        private void MainDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var counterparty = (Counterparty)MainDataGrid.SelectedItem;
            if (counterparty != null)
            {
                SendMessage(typeof(EditOrganizationPanel), counterparty.Id);
            }
        }
    }
}
