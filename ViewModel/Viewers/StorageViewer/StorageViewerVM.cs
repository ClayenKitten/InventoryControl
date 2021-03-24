using InventoryControl.Model;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;

namespace InventoryControl.ViewModel
{
    public class StorageViewerVM : INotifyPropertyChanged
    {
        public StorageViewerOptions Options { get; set; } = new StorageViewerOptions(0);
        public int StorageId 
        {
            get
            {
                return Options.StorageId;
            }
            set
            {
                Options.StorageId = value;
            }
        }
        public bool IsSelectorEnabled
        {
            get
            {
                return Options.IsSelectorEnabled;
            }
            set
            {
                Options.IsSelectorEnabled = value;
            }
        }
        public List<StockProductPresenter> Content
        {
            get
            {
                List<StockProductPresenter> res = new List<StockProductPresenter>();
                foreach (var product in ProductMapper.GetFullDictionary())
                {
                    res.Add(new StockProductPresenter(product, Options.StorageId));
                }
                return res;
            }
        }

        public List<Storage> AllStoragesList { get { return StorageMapper.GetAllStorages(); } }
        //Statusbar
        public string SaleSum
        {
            get
            {
                Money sum = 0.0M;
                foreach (var product in Content)
                {
                    sum += product.Product.SalePrice * product.Remain;
                }
                return sum.ToString();
            }
        }
        public string PurchaseSum
        {
            get
            {
                Money sum = 0.0M;
                foreach (var product in Content)
                {
                    sum += product.Product.PurchasePrice * product.Remain;
                }
                return sum.ToString();
            }
        }
        //Context menu
        public StockProductPresenter SelectedProduct { get; set; }

        public StorageViewerVM()
        {
            GlobalCommands.ModelUpdated.Executed += (_) => 
            { 
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Content"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("View"));
            };
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
