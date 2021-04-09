using InventoryControl.Model;
using InventoryControl.View.Controls;
using InventoryControl.ViewModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Documents;

namespace InventoryControl.View
{
    /// <summary>
    /// Interaction logic for EditOrganizationPanel.xaml
    /// </summary>
    public partial class EditOrganizationPanel : ControlPanel, INotifyPropertyChanged
    {
        private TextAdorner adorner;
        public EditOrganizationPanel()
        {
            InitializeComponent();
            adorner = new TextAdorner(this);
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            AdornerLayer.GetAdornerLayer(this).Add(adorner);
            adorner.Text = "Выберите организацию из списка";
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
                ContentVisibility = Visibility.Visible;
                adorner.Text = "";
            }
        }
        private Visibility visibility = Visibility.Collapsed;
        public Visibility ContentVisibility
        {
            get => visibility;
            set
            {
                visibility = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ContentVisibility"));
            }
        }

        private void ConfirmClick(object sender, RoutedEventArgs e)
        {
            Counterparty.Table.Update(CounterpartyData);
            GlobalCommands.ModelUpdated.Execute(null);
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
