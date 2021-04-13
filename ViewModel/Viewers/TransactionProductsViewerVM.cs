using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using InventoryControl.Model;
using InventoryControl.View.Controls;
using Microsoft.Xaml.Behaviors.Core;

namespace InventoryControl.ViewModel
{
    public class TransactionProductsViewerVM : INotifyPropertyChanged
    {
        //Titles
        public string Title
        {
            get
            {
                if (Type == TransferType.Buy) return "ÇÀÊÓÏÊÀ ÒÎÂÀĞÀ";
                else if (Type == TransferType.Sell) return "ÏĞÎÄÀÆÀ ÒÎÂÀĞÀ";
                else if (Type == TransferType.Return) return "ÂÎÇÂĞÀÒ ÒÎÂÀĞÀ";
                else if (Type == TransferType.Supply) return "ÏÎÑÒÀÂÊÀ ÒÎÂÀĞÀ";
                else if (Type == TransferType.Transport) return "ÏÅĞÅÂÎÇÊÀ ÒÎÂÀĞÀ";
                else return "Given transaction type isn't implemented";
            }
        }
        public string TransferSpots1Title
        {
            get
            {
                if (Type == TransferType.Buy) return "Ïîñòàâùèê";
                else if (Type == TransferType.Sell) return "Ïîêóïàòåëü";
                else if (Type == TransferType.Return) return "Ïîñòàâùèê";
                else if (Type == TransferType.Supply) return "Òî÷êà ïğîäàæ";
                else if (Type == TransferType.Transport) return "Ñêëàä 1";
                else return "Given transaction type isn't implemented";
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
                if (Type == TransferType.Buy) return CounterpartyMapper.GetSuppliers().Cast<ITransferSpot>().ToList();
                else if (Type == TransferType.Sell) return CounterpartyMapper.GetPurchasers().Cast<ITransferSpot>().ToList();
                else if (Type == TransferType.Return) return CounterpartyMapper.GetSuppliers().Cast<ITransferSpot>().ToList();
                else if (Type == TransferType.Supply) return PointOfSales.Table.ReadAll().Cast<ITransferSpot>().ToList();
                else if (Type == TransferType.Transport) return Storage.Table.ReadAll().Cast<ITransferSpot>().ToList();
                else return new List<ITransferSpot>();
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
        public ActionCommand ConfirmTransactionCommand
            => new ActionCommand(Confirm);
        public void Confirm()
        {
            var pm = new PanelManager();
            pm.OpenTransferHistoryViewer.Execute();
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
