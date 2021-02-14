using System;
using System.Collections.Generic;
using System.ComponentModel;
using InventoryControl.Model.Product;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace InventoryControl.ViewModel
{
    public enum TransactionType
    {
        Buy,
        Sell,
        Return
    }

    public class TransactionProductsViewerVM : INotifyPropertyChanged
    {
        public string Title
        {
            get
            {
                switch (Type)
                {
                    case TransactionType.Buy:
                        return "������� ������";
                    case TransactionType.Sell:
                        return "������� ������";
                    case TransactionType.Return:
                        return "������� ������";
                    default:
                        throw new NotImplementedException();
                }
            }
        }
        public ObservableCollection<TransactionProductPresenter> Content { get; }
            = new ObservableCollection<TransactionProductPresenter>();
        
        private TransactionType type;
        public TransactionType Type 
        {
            get => type;
            set
            {
                type = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Title"));
            }
        }

        public TransactionProductsViewerVM()
        {
            
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
