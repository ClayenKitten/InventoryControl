using InventoryControl.Model.Product;
using InventoryControl.Model.Storage;
using InventoryControl.Model.Util;
using InventoryControl.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryControl.ViewModel
{
    public class StorageViewerVM : INotifyPropertyChanged
    {
        public ObservableCollection<StockProductPresenter> Content
        {
            get
            {
                ObservableCollection<StockProductPresenter> res = new ObservableCollection<StockProductPresenter>();
                foreach (var product in StorageDataMapper.GetProductsInStorage(StorageId))
                {
                    res.Add(new StockProductPresenter(product, StorageId));
                }
                return res;
            }
        }

        public int StorageId { get; set; }
        public string SearchString { get; set; }
        public List<StorageData> AllStoragesList { get { return StorageDataMapper.GetAllStorages(); } }
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
            GlobalCommands.ModelUpdated.Executed += (_) => { PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Content")); };
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
