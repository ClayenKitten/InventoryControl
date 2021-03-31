using InventoryControl.Model;
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
        private List<Counterparty> purchasers => CounterpartyMapper.GetPurchasers();
        private List<Counterparty> suppliers => CounterpartyMapper.GetSuppliers();
        public List<Counterparty> Content =>
            ShowPurchasers ?
                purchasers.Cast<Counterparty>().ToList() :
                suppliers.Cast<Counterparty>().ToList();

        public string Header { get => ShowPurchasers ? "Покупатели" : "Поставщики"; }

        public event PropertyChangedEventHandler PropertyChanged;

        public Action<DataGridRowEditEndingEventArgs> OnRowEditEnded => (e) =>
        {
            CounterpartyMapper.Update((Counterparty)e.Row.Item);
        };
    }
}
