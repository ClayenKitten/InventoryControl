using InventoryControl.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;

namespace InventoryControl.ViewModel
{
    public class CounterpatyViewerVM : INotifyPropertyChanged
    {
        private List<Counterparty> purchasers { get { return CounterpartyMapper.GetPurchasers(); } }
        private List<Counterparty> suppliers { get { return CounterpartyMapper.GetSuppliers(); } }
        public List<Counterparty> Content
        {
            get
            {
                switch(CounterpartyType)
                {
                    case CounterpartyType.Supplier:
                        return suppliers;
                    case (CounterpartyType.Purchaser):
                        return purchasers;
                    default:
                        return new List<Counterparty>();
                }
            }
        }
        private CounterpartyType counterpartyType;

        public event PropertyChangedEventHandler PropertyChanged;

        public CounterpartyType CounterpartyType 
        { 
            get { return counterpartyType; }
            set { counterpartyType = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Content")); }
        }

        public void RowEditHandler(DataGridRowEditEndingEventArgs e)
        {
            CounterpartyMapper.Update((Counterparty)e.Row.Item);
        }
    }
}
