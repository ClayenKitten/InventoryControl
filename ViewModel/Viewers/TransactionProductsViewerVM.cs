using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using InventoryControl.Model;
using InventoryControl.View.Controls;

namespace InventoryControl.ViewModel
{
    public enum TransactionType
    {
        Buy,
        Sell,
        Return,
        Supply,
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
        public List<Counterparty> Counterparties
        {
            get
            {
                switch (Type)
                {
                    case TransactionType.Buy:
                        return CounterpartyMapper.GetSuppliers();
                    case TransactionType.Sell:
                        return CounterpartyMapper.GetPurchasers();
                    case TransactionType.Return:
                        return CounterpartyMapper.GetSuppliers();
                    default:
                        return new List<Counterparty>();
                }
            }
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
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Counterparties"));
                SelectedCounterparty = Counterparties.FirstOrDefault().Id;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedCounterparty"));
                SelectedStorage = Storages.FirstOrDefault().Id;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedStorage"));
            }
        }

        public TransactionProductsViewerVM()
        {
            GlobalCommands.CreateTransaction.Executed += (parameter) =>
            {
                TransferMapper.Create
                (
                    new Transfer
                    (
                        id: -1,
                        dateTime: DateTime.Now, 
                        counterpartyId: SelectedCounterparty,
                        storageId: SelectedStorage,
                        products: new List<TransactionProductPresenter>(Content)
                    )
                );
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
            Content.Add(new TransactionProductPresenter(Product.Table.Read(id), 1));
        }
        public void ShowContextMenu(TransactionProductPresenter product)
        {
            new ContextMenuBuilder()
                .AddAction("Óäàëèòü", () => Content.Remove(product))
            .Build()
            .IsOpen = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
