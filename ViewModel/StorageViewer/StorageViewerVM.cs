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
        public ICollectionView View 
        {
            get
            {
                var view = CollectionViewSource.GetDefaultView(Content);
                view.Filter = FilterProducts;
                if (Options.GroupOutOfStockProducts)
                    view.GroupDescriptions.Add(new PropertyGroupDescription("IsInStock"));
                else
                    view.GroupDescriptions.Clear();
                return view;
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

        private string searchString = "";
        public string SearchString
        {
            get { return searchString; }
            set
            {
                searchString = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SearchString"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Content"));
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
        private bool FilterProducts(object item)
        {
            bool accepted = true;
            var product = (StockProductPresenter)item;
            //Check search
            string productName = product.Name.ToLower().Replace('ё', 'е');
            SearchString = SearchString.ToLower().Replace('ё', 'е');
            accepted = accepted && productName.Contains(SearchString);
            //Check ShowOutOfStockProducts
            accepted = accepted && (Options.ShowOutOfStockProducts || product.IsInStock);

            return accepted;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
