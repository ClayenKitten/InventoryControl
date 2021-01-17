using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using InventoryControl.Model;
using InventoryControl.Model.Counterparty;

namespace InventoryControl.UserControls.OrderControl
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class SaleControls : UserControl, IBottomControl
    {
        public event ConfirmTurnoverHandler ConfirmTurnover;

        public SaleControls()
        {
            InitializeComponent();
            storeDecideCombobox.ItemsSource = CounterpartyMapper.GetPurchasersNames();
        }
        public void SetButtonsEnabled(bool isEnabled)
        {
            printButton.IsEnabled = isEnabled;
            confirmButton.IsEnabled = isEnabled;
        }
        private void PrintButtonClicked(object sender, RoutedEventArgs e)
        {
            OrderControl orderControl = ((MainWindow)App.Current.MainWindow).GetOrderControl();
            new Product.Receipt(0, DateTime.Today, storeDecideCombobox.SelectedValue.ToString(), orderControl.OrderProducts.ToList());
        }
        private void ConfirmButtonClicked(object sender, RoutedEventArgs e)
        {
            ConfirmTurnover(false, storeDecideCombobox.Text);
        }

        public UIElement GetUIElement()
        {
            return this;
        }
    }
}
