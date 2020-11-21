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

namespace InventoryControl.UserControls.OrderControl
{
    /// <summary>
    /// Interaction logic for OrderControlBuyingControls.xaml
    /// </summary>
    public partial class BuyingControls : UserControl, IBottomControl
    {
        public BuyingControls()
        {
            InitializeComponent();
        }

        public event ConfirmTurnoverHandler ConfirmTurnover;

        public void SetButtonsEnabled(bool isEnabled)
        {
            confirmButton.IsEnabled = isEnabled;
        }
        public UIElement GetUIElement()
        {
            return this;
        }

        private void ConfirmButtonClicked(object sender, RoutedEventArgs e)
        {
            ConfirmTurnover(true, SenderTB.Text);
        }
    }
}
