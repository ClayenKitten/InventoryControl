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

        public Counterparty CounterpartyData
        {
            get => (Counterparty)GetValue(CounterpartyDataProperty);
            set => SetCurrentValue(CounterpartyDataProperty, value);
        }
        static DependencyProperty CounterpartyDataProperty =
            DependencyProperty.Register("CounterpartyData", typeof(Counterparty), typeof(EditOrganizationPanel));

        public override void ReceiveMessage(object sender, object message)
        {
            if(sender is CounterpartyViewer)
            {
                CounterpartyData = Counterparty.Table.Read((long)message);
            }
        }

        private void ConfirmClick(object sender, RoutedEventArgs e)
        {
            Counterparty.Table.Update(CounterpartyData);
            GlobalCommands.ModelUpdated.Execute(null);
        }
    }
}
