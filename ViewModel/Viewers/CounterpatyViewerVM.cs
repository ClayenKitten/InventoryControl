using InventoryControl.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;

namespace InventoryControl.ViewModel
{
    public class CounterpatyViewerVM : INotifyPropertyChanged
    {
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
        private List<Counterparty> purchasers { get { return CounterpartyMapper.GetPurchasers(); } }
        private List<Counterparty> suppliers { get { return CounterpartyMapper.GetSuppliers(); } }
        public List<Counterparty> Content { get => ShowPurchasers ? purchasers : suppliers; }

        public string Header { get => ShowPurchasers ? "Покупатели" : "Поставщики"; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void RowEditHandler(DataGridRowEditEndingEventArgs e)
        {
            CounterpartyMapper.Update((Counterparty)e.Row.Item);
        }
    }
}
