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
                Content.Add(new TransactionProductPresenter(ProductDataMapper.Read(productId), 1));
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
