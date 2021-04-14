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
        public EditOrganizationPanel(Counterparty item) : this()
        {
            SetValue(CounterpartyDataProperty, item);
        }
        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (e.Property == CounterpartyDataProperty)
            {
                if ((e.NewValue as Counterparty).Role == -1)
                {
                    adorner.Text = "";
                    ContentVisibility = Visibility.Visible;
                }
            };
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            AdornerLayer.GetAdornerLayer(this).Add(adorner);
            if (CounterpartyData?.Role != -1) adorner.Text = "Выберите организацию из списка";
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
                CounterpartyData = Counterparty.Clone((Counterparty)message);
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
            if (CounterpartyData.Id < 0)
            {
                Counterparty.Table.Create(CounterpartyData);
            }
            else
            {
                Counterparty.Table.Update(CounterpartyData);
            }
            GlobalCommands.ModelUpdated.Execute(null);
            var PM = new PanelManager();
            if (CounterpartyData.Role == -1) PM.OpenStorageView.Execute();
            else if (CounterpartyData.Role == 0) PM.OpenPurchasers.Execute();
            else PM.OpenSuppliers.Execute();
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
