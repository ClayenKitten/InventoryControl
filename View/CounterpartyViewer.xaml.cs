﻿using InventoryControl.Model;
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
                CounterpartyMapper.Create(new Purchaser()
                {
                    Name = NameTB.Text,
                    Address = AddressTB.Text,
                    Contacts = ContactsTB.Text,
                    TaxpayerNumber = TaxpayerTB.Text,
                    AccountingCode = AccountTB.Text,
                    BankDetails = BankIdTB.Text
                });
            }
            else
            {
                CounterpartyMapper.Create(new Supplier()
                {
                    Name = NameTB.Text,
                    Address = AddressTB.Text,
                    Contacts = ContactsTB.Text,
                    TaxpayerNumber = TaxpayerTB.Text,
                    AccountingCode = AccountTB.Text,
                    BankDetails = BankIdTB.Text
                });
            }
            GlobalCommands.ModelUpdated.Execute(null);
        }
    }
}
