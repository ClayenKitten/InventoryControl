using InventoryControl.Model;
using Microsoft.Xaml.Behaviors.Core;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;

namespace InventoryControl.ViewModel
{
    public class CounterpatyViewerVM : INotifyPropertyChanged
    {
        public CounterpatyViewerVM()
        {
            AddNewCounterpartyCommand = new ActionCommand(() =>
            {
                var content = new List<Counterparty>(Content);
                content.Add(new Counterparty() { Role = showPurchasers ? 0 : 1 });
                Content = content;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Content"));
            });
            GlobalCommands.ModelUpdated.Executed += _ =>
            {
                purchasers = CounterpartyMapper.GetPurchasers();
                suppliers = CounterpartyMapper.GetSuppliers();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Content"));
            };            
        }

        private bool showPurchasers;
        public bool ShowPurchasers
        {
            get => showPurchasers;
            set
            {
                showPurchasers = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Header"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Content"));
            }
        }
        private List<Counterparty> purchasers { get; set; } = CounterpartyMapper.GetPurchasers();
        private List<Counterparty> suppliers { get; set; } = CounterpartyMapper.GetSuppliers();
        public List<Counterparty> Content
        {
            get => ShowPurchasers ? purchasers : suppliers;
            set
            {
                if (ShowPurchasers)
                {
                    purchasers = value;
                }
                else
                {
                    suppliers = value;
                }
            }
        }
        public ActionCommand AddNewCounterpartyCommand { get; }

        public string Header { get => ShowPurchasers ? "Покупатели" : "Поставщики"; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
