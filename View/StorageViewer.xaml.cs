﻿using InventoryControl.Model;
using InventoryControl.Model.Product;
using InventoryControl.Model.Storage;
using InventoryControl.Model.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InventoryControl.View
{
    /// <summary>
    /// Interaction logic for StorageViewer.xaml
    /// </summary>
    public partial class StorageViewer : UserControl, INotifyPropertyChanged
    {
        public ObservableCollection<StockProductPresenter> DataGridContent { get; set; } = new ObservableCollection<StockProductPresenter>();

        public int StorageId { get; set; }
        public static readonly DependencyProperty StorageIdProperty =
            DependencyProperty.Register("StorageId", typeof(int),
            typeof(StorageViewer), new PropertyMetadata(-1));

        public event PropertyChangedEventHandler PropertyChanged;

        public StorageData Storage { get { return StorageDataMapper.GetStorage(storageId); } }
        public List<StorageData> AllStoragesList { get { return StorageDataMapper.GetAllStorages(); } }
        //Statusbar values
        private int storageId { get { return (int)GetValue(StorageIdProperty); } }
        public string SaleSum
        {
            get 
            {
                Money sum = 0.0M;
                foreach(var product in DataGridContent)
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
                foreach (var product in DataGridContent)
                {
                    sum += product.Product.PurchasePrice * product.Remain;
                }
                return sum.ToString();
            }
        }

        public StorageViewer()
        {
            InitializeComponent();
        }
        public StorageViewer(int index)
        {
            InitializeComponent();
            SetValue(StorageIdProperty, index);
        }
        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if(e.Property == StorageIdProperty) 
            { 
                UpdateContent();
            }
        }
        private void UpdateContent()
        {
            DataGridContent.Clear();
            foreach (var product in StorageDataMapper.GetProductsInStorage(storageId))
            {
                DataGridContent.Add(new StockProductPresenter(product, storageId));
            }
            SaleSumRun.GetBindingExpression(Run.TextProperty).UpdateTarget();
            PurchaseSumRun.GetBindingExpression(Run.TextProperty).UpdateTarget();
        }
        private void EditProductClick(object sender, RoutedEventArgs e)
        {
            var contextMenu = (ContextMenu)((MenuItem)sender).Parent;
            var row = (DataGridRow)contextMenu.PlacementTarget;
            var id = ((StockProductPresenter)MainDataGrid.Items.GetItemAt(row.GetIndex())).Product.Id;

            throw new NotImplementedException();
        }

        private void MakeSearch(string searchString)
        {
            DataGridContent.Clear();
            foreach (ProductData product in StorageDataMapper.GetProductsInStorage(storageId))
            {
                String search = searchString.ToLower().Replace('ё', 'е').Replace(" ", "").Trim();
                String title = product.Name.ToLower().Replace('ё', 'е').Replace(" ","").Trim();

                if (title.Contains(search))
                {
                    DataGridContent.Add(new StockProductPresenter(product, storageId));
                }
            }
        }
        private void Searchbox_KeyUp(object sender, KeyEventArgs e)
        {
            MakeSearch(Searchbox.Text);
        }
        private void StorageSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void ReloadContent_Click(object sender, RoutedEventArgs e)
        {
            UpdateContent();
        }
    }
}
