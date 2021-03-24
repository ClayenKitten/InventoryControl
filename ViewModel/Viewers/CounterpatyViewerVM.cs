using InventoryControl.Model;
using System;
using System.Collections.Generic;
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
        private List<Purchaser> purchasers => CounterpartyMapper.GetPurchasers();
        private List<Supplier> suppliers => CounterpartyMapper.GetSuppliers();
        public List<ICounterparty> Content => 
            ShowPurchasers ?
                purchasers.Cast<ICounterparty>().ToList() :
                suppliers.Cast<ICounterparty>().ToList();

        public string Header { get => ShowPurchasers ? "Покупатели" : "Поставщики"; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void RowEditHandler(DataGridRowEditEndingEventArgs e)
        {
            CounterpartyMapper.Update((ICounterparty)e.Row.Item);
        }
    }
}
