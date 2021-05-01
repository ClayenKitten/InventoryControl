using InventoryControl.Model;
using InventoryControl.View;
using InventoryControl.View.Controls;
using Microsoft.Xaml.Behaviors.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;

namespace InventoryControl.ViewModel
{
    public class StorageViewerVM : INotifyPropertyChanged, IDisposable
    {
        public long StorageId { get; set; }
        private ViewOptions options = new ViewOptions();
        public ViewOptions Options
        {
            get => options;
            set
            {
                options = value;
                options.ViewOptionsChanged += OnOptionsUpdated;
            }
        }
        //Binding-ready options getters
        public Visibility StorageSelectorAsComboboxVisibility
            => Options.HideStorageSelector ? Visibility.Collapsed : Visibility.Visible;
        public Visibility StorageSelectorAsTextboxVisibility 
            => Options.HideStorageSelector ? Visibility.Visible : Visibility.Collapsed;

        // Group
        public bool GroupOutOfStockProducts
            => GroupingPropertyPath == "IsInStock";
        public bool GroupPrintable
            => GroupingPropertyPath == "Category";
        public string GroupingPropertyPath
            => Options.Group;

        public List<StockProductPresenter> Content
        {
            get
            {
                List<StockProductPresenter> res = new List<StockProductPresenter>();
                foreach (var product in Product.Table.ReadAll())
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
            Options.ViewOptionsChanged += OnOptionsUpdated;
        }
        public void Dispose()
        {
            GlobalCommands.ModelUpdated.Executed -= OnModelUpdated;
            Options.ViewOptionsChanged -= OnOptionsUpdated;
        }

        public static ActionCommand ShowProductInDictionaryCommand { get; }
            = new ActionCommand
            (
                x => (App.Current.MainWindow as MainWindow).Panel =
                    new DualControlPanelContainer(new ProductDictionaryViewer(), new EditProductPanel((long)x), 0.5f)
            );
        public void OnOptionsUpdated(object _, object _1)
        {
            Options = new ViewOptions(Options);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Options"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Content"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("View"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("StorageSelectorAsComboboxVisibility"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("StorageSelectorAsTextboxVisibility"));

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("GroupOutOfStockProducts"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("GroupingPropertyPath"));
        }
        protected void OnModelUpdated(object _)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("StorageId"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Content"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("View"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentStorageName"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SaleSum"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PurchaseSum"));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
