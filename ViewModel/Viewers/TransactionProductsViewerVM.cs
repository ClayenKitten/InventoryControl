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
    public enum TransferType
    {
        Buy,
        Sell,
        Return,
        Supply,
        Transport
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
                    case TransferType.Buy:
                        return "ÇÀÊÓÏÊÀ ÒÎÂÀĞÀ";
                    case TransferType.Sell:
                        return "ÏĞÎÄÀÆÀ ÒÎÂÀĞÀ";
                    case TransferType.Return:
                        return "ÂÎÇÂĞÀÒ ÒÎÂÀĞÀ";
                    case TransferType.Supply:
                        return "ÏÎÑÒÀÂÊÀ ÒÎÂÀĞÀ";
                    case TransferType.Transport:
                        return "ÏÅĞÅÂÎÇÊÀ ÒÎÂÀĞÀ";
                    default:
                        return "Bad transaction type";
                }
            }
        }
        public string TransferSpots1Title
        {
            get
            {
                switch (Type)
                {
                    case TransferType.Buy:
                        return "Ïîñòàâùèê";
                    case TransferType.Sell:
                        return "Ïîêóïàòåëü";
                    case TransferType.Return:
                        return "Ïîñòàâùèê";
                    case TransferType.Supply:
                        return "Òî÷êà ïğîäàæ";
                    case TransferType.Transport:
                        return "Ñêëàä 1";
                    default:
                        return "Bad transaction type";
                }
            }
        }
        public string TransferSpots2Title
        {
            get
            {
                if (Type == TransferType.Transport)
                {
                    return "Ñêëàä 2";
                }
                else
                {
                    return "Ñêëàä";
                }
            }
        }

        public ObservableCollection<TransactionProductPresenter> Content { get; }
            = new ObservableCollection<TransactionProductPresenter>();
        
        //Transaction info
        public IList<ITransferSpot> TransferSpots1
        {
            get
            {
                switch (Type)
                {
                    case TransferType.Buy:
                        return CounterpartyMapper.GetSuppliers().Cast<ITransferSpot>().ToList();
                    case TransferType.Sell:
                        return CounterpartyMapper.GetPurchasers().Cast<ITransferSpot>().ToList();
                    case TransferType.Return:
                        return CounterpartyMapper.GetSuppliers().Cast<ITransferSpot>().ToList();
                    case TransferType.Supply:
                        return PointOfSales.Table.ReadAll().Cast<ITransferSpot>().ToList();
                    case TransferType.Transport:
                        return Storage.Table.ReadAll().Cast<ITransferSpot>().ToList();
                    default:
                        return new List<ITransferSpot>();
                }
            }
        }
        public IList<ITransferSpot> TransferSpots2
        {
            get => StorageMapper.GetAllStorages().Cast<ITransferSpot>().ToList();
        }
        public ITransferSpot SelectedTransferSpot1 { get; set; }
        public ITransferSpot SelectedTransferSpot2 { get; set; }

        private TransferType type;
        public TransferType Type 
        {
            get => type;
            set
            {
                type = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Title"));

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TransferSpots1Title"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TransferSpots2Title"));

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TransferSpots1"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TransferSpots2"));

                SelectedTransferSpot1 = TransferSpots1.FirstOrDefault();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedTransferSpot1"));
                SelectedTransferSpot2 = TransferSpots2.FirstOrDefault();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedTransferSpot2"));
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
                        dateTime: DateTime.Now, 
                        transferSpot1: SelectedTransferSpot1,
                        transferSpot2: SelectedTransferSpot2,
                        products: new List<TransactionProductPresenter>(Content)
                    )
                );
                var pm = new PanelManager();
                pm.OpenStorageView.Execute();
            };
        }
        public void AddProduct(long id)
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
