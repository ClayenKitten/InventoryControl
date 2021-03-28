using InventoryControl.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;

namespace InventoryControl.ViewModel
{
    public class StorageViewerVM : INotifyPropertyChanged, IDisposable
    {
        public int StorageId { get; set; }
        public StorageViewerOptions Options { get; set; } = new StorageViewerOptions();
        //Binding-ready options getters
        public Visibility StorageSelectorAsComboboxVisibility
            => Options.HideStorageSelector ? Visibility.Collapsed : Visibility.Visible;
        public Visibility StorageSelectorAsTextboxVisibility 
            => Options.HideStorageSelector ? Visibility.Visible : Visibility.Collapsed;
        public bool GroupOutOfStockProducts
            => Options.GroupOutOfStockProducts;
        public string GroupingPropertyPath
            => GroupOutOfStockProducts ? "IsInStock" : string.Empty;
        public bool HideOutOfStockProducts
            => Options.HideOutOfStockProducts;
        public string FilterPropertyPath
            => HideOutOfStockProducts ? "IsInStock" : string.Empty;

        public List<StockProductPresenter> Content
        {
            get
            {
                List<StockProductPresenter> res = new List<StockProductPresenter>();
                foreach (var product in ProductMapper.GetFullDictionary())
                {
                    res.Add(new StockProductPresenter(product, StorageId));
                }
                return res;
            }
        }
        //Header
        public List<Storage> AllStoragesList
            => StorageMapper.GetAllStorages();
        public string CurrentStorageName
            => StorageMapper.Read(StorageId).Name;
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
            GlobalCommands.ModelUpdated.Executed += OnModelUpdated;
        }
        public void Dispose()
        {
            GlobalCommands.ModelUpdated.Executed -= OnModelUpdated;
        }

        protected void OnModelUpdated(object _)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Content"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("View"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("StorageSelectorAsComboboxVisibility"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("StorageSelectorAsTextboxVisibility"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentStorageName"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("GroupOutOfStockProducts"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("GroupingPropertyPath"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("HideOutOfStockProducts"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FilterPropertyPath"));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
