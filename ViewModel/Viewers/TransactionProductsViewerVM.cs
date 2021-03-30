using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using InventoryControl.Model;

namespace InventoryControl.ViewModel
{
    public enum TransactionType
    {
        Buy,
        Sell,
        Return,
    }

    public class TransactionProductsViewerVM : INotifyPropertyChanged
    {
        //Titles
        public string Title
        {
            get
            {
                switch (Type)
                {
                    case TransactionType.Buy:
                        return "ÇÀÊÓÏÊÀ ÒÎÂÀĞÀ";
                    case TransactionType.Sell:
                        return "ÏĞÎÄÀÆÀ ÒÎÂÀĞÀ";
                    case TransactionType.Return:
                        return "ÂÎÇÂĞÀÒ ÒÎÂÀĞÀ";
                    default:
                        return "Bad transaction type";
                }
            }
        }
        public string CounterpartyTitle
        {
            get
            {
                switch (Type)
                {
                    case TransactionType.Buy:
                        return "Ïîñòàâùèê";
                    case TransactionType.Sell:
                        return "Ïîêóïàòåëü";
                    case TransactionType.Return:
                        return "Ïîñòàâùèê";
                    default:
                        return "Bad transaction type";
                }
            }
        }

        public ObservableCollection<TransactionProductPresenter> Content { get; }
            = new ObservableCollection<TransactionProductPresenter>();
        
        //Transaction info
        public List<Supplier> Counterparties
        {
            get { return CounterpartyMapper.GetSuppliers(); }
        }
        public List<Storage> Storages
        {
            get => StorageMapper.GetAllStorages();
        }
        public int SelectedCounterparty { get; set; }
        public int SelectedStorage { get; set; }

        private TransactionType type;
        public TransactionType Type 
        {
            get => type;
            set
            {
                type = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Title"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CounterpartyTitle"));
            }
        }

        public TransactionProductsViewerVM()
        {
            GlobalCommands.CreateTransaction.Executed += (parameter) =>
            {
                TransactionMapper.Create(new Transfer(-1, DateTime.Now, -1, -1,
                    new List<TransactionProductPresenter>(Content)));
                var pm = new PanelManager();
                pm.OpenStorageView.Execute();
            };
        }
        public void AddProduct(int id)
        {
            foreach (var product in Content)
            {
                if (product.Id == id) return;
            }
            Content.Add(new TransactionProductPresenter(ProductMapper.Read(id), 1));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
