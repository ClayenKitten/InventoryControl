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
                if (Type == TransferType.Buy) return "«¿ ”œ ¿ “Œ¬¿–¿";
                else if (Type == TransferType.Sell) return "œ–Œƒ¿∆¿ “Œ¬¿–¿";
                else if (Type == TransferType.Return) return "¬Œ«¬–¿“ “Œ¬¿–¿";
                else if (Type == TransferType.Supply) return "œŒ—“¿¬ ¿ “Œ¬¿–¿";
                else if (Type == TransferType.Transport) return "œ≈–≈¬Œ« ¿ “Œ¬¿–¿";
                else return "Given transaction type isn't implemented";
            }
        }
        public string TransferSpots1Title
        {
            get
            {
                if (Type == TransferType.Buy) return "œÓÒÚ‡‚˘ËÍ";
                else if (Type == TransferType.Sell) return "œÓÍÛÔ‡ÚÂÎ¸";
                else if (Type == TransferType.Return) return "œÓÒÚ‡‚˘ËÍ";
                else if (Type == TransferType.Supply) return "“Ó˜Í‡ ÔÓ‰‡Ê";
                else if (Type == TransferType.Transport) return "—ÍÎ‡‰ 1";
                else return "Given transaction type isn't implemented";
            }
        }
        public string TransferSpots2Title
        {
            get
            {
                if (Type == TransferType.Transport)
                {
                    return "—ÍÎ‡‰ 2";
                }
                else
                {
                    return "—ÍÎ‡‰";
                }
            }
        }
        public string Cause { get; set; }

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
        public ITransferSpot SelectedTransferSpot1
        {
            get => selectedTransferSpot1;
            set
            {
                selectedTransferSpot1 = value;
                Content.Clear();
            }
        }
        public ITransferSpot SelectedTransferSpot2
        {
            get => selectedTransferSpot2;
            set
            {
                selectedTransferSpot2 = value;
                Content.Clear();
            }
        }

        private TransferType type;
        private ITransferSpot selectedTransferSpot1;
        private ITransferSpot selectedTransferSpot2;

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
                // If type is transport
                if (SelectedTransferSpot1 is Storage s)
                {
                    if (StorageMapper.GetProductAmount(product.Id, s.Id) < product.TransmitNumber) return;
                }
                else if (Type != TransferType.Buy)
                {
                    if (StorageMapper.GetProductAmount(product.Id, (SelectedTransferSpot2 as Storage).Id) < product.TransmitNumber) return;
                }
            }
            Content.Add(new TransactionProductPresenter(Product.Table.Read(id), 1));
        }
        public void ShowContextMenu(TransactionProductPresenter product)
        {
            new ContextMenuBuilder()
                .AddAction("”‰‡ÎËÚ¸", () => Content.Remove(product))
            .Build()
            .IsOpen = true;
        }
        public ActionCommand ConfirmTransactionCommand
            => new ActionCommand(Confirm);
        public void Confirm()
        {
            string sender = ""; string receiver = ""; string payer = "";
            if (Type == TransferType.Buy)
            {
                sender = (SelectedTransferSpot1 as Counterparty).DisplayName;
                receiver = CounterpartyMapper.GetManaged().DisplayName;

                foreach (var product in Content)
                {
                    StorageMapper.AddProductAmount(product.Id, (SelectedTransferSpot2 as Storage).Id, product.TransmitNumber);
                }
            }
            else if (Type == TransferType.Sell)
            {
                sender = CounterpartyMapper.GetManaged().DisplayName;
                receiver = (SelectedTransferSpot1 as Counterparty).DisplayName;

                foreach (var product in Content)
                {
                    StorageMapper.AddProductAmount(product.Id, (SelectedTransferSpot2 as Storage).Id, -product.TransmitNumber);
                }
            }
            else if (Type == TransferType.Supply)
            {
                sender = CounterpartyMapper.GetManaged().DisplayName;
                receiver = CounterpartyMapper.GetManaged().DisplayName;

                foreach (var product in Content)
                {
                    StorageMapper.AddProductAmount(product.Id, (SelectedTransferSpot2 as Storage).Id, -product.TransmitNumber);
                }
            }
            else if (Type == TransferType.Transport)
            {
                sender = CounterpartyMapper.GetManaged().DisplayName;
                receiver = CounterpartyMapper.GetManaged().DisplayName;

                foreach (var product in Content)
                {
                    StorageMapper.AddProductAmount(product.Id, (SelectedTransferSpot1 as Storage).Id, -product.TransmitNumber);
                    StorageMapper.AddProductAmount(product.Id, (SelectedTransferSpot2 as Storage).Id, product.TransmitNumber);
                }
            }

            payer = receiver;
            var invoice = new ProductInvoice(Type, sender, receiver, payer, Cause);
            var initedInvoice = ProductInvoice.Table.Create(invoice);

            foreach (var product in Content)
            {
                var invoiceProduct = new InvoiceProduct()
                {
                    Id = -1,
                    InvoiceId = initedInvoice.Id,
                    Name = product.Name,
                    Measurement = product.Product.Measurement.Postfix,
                    NumberInPackage = product.Product.Measurement.RawValue,
                    NumberOfPackages = product.TransmitNumber,
                    Price = Type == TransferType.Buy ?
                                    product.Product.PurchasePrice.RawValue :
                                    product.Product.SalePrice.RawValue
                };
                InvoiceProduct.Table.Create(invoiceProduct);
            }

            var pm = new PanelManager();
            pm.OpenTransferHistoryViewer.Execute();
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
