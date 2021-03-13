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
                        return "«¿ ”œ ¿ “Œ¬¿–¿";
                    case TransactionType.Sell:
                        return "œ–Œƒ¿∆¿ “Œ¬¿–¿";
                    case TransactionType.Return:
                        return "¬Œ«¬–¿“ “Œ¬¿–¿";
                    default:
                        throw new NotImplementedException();
                }
            }
        }
        public ObservableCollection<TransactionProductPresenter> Content { get; }
            = new ObservableCollection<TransactionProductPresenter>();
        
        //Transaction info
        public List<Counterparty> Suppliers
        {
            get { return CounterpartyMapper.GetSuppliers(); }
        }
        public List<Counterparty> Purchasers
        {
            get { return CounterpartyMapper.GetPurchasers(); }
        }
        public int SelectedSender { get; set; }
        public int SelectedReceiver { get; set; }

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
            GlobalCommands.SendProduct.Executed += (parameter) =>
            {
                int productId;
                try { productId = Convert.ToInt32(parameter); }
                catch { return; }

                foreach(var product in Content)
                {
                    if(product.Id == productId) return;
                }
                Content.Add(new TransactionProductPresenter(ProductMapper.Read(productId), 1));
            };
            GlobalCommands.CreateTransaction.Executed += (parameter) =>
            {
                TransactionMapper.Create(new Transaction(DateTime.Now, -1, -1,
                    new List<TransactionProductPresenter>(Content)));
                var pm = new PanelManager();
                pm.OpenStorageView.Execute();
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
