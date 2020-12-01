using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using InventoryControl.Data;

namespace InventoryControl.UserControls.OrderControl
{
    /// <summary>
    /// Interaction logic for SellingListControl.xaml
    /// </summary>
    public partial class OrderControl : UserControl
    {
        public string Title { get; set; }
        public bool IsBuying { get; }
        public ObservableCollection<OrderProductData> OrderProducts { get; set; }
        
        public OrderControl(bool isBuying)
        {
            this.Title = isBuying ? "Покупка" : "Продажа";
            IsBuying = isBuying;

            OrderProducts = new ObservableCollection<OrderProductData>();
            IBottomControl bottomControl = isBuying ? 
                (IBottomControl)new BuyingControls() : 
                (IBottomControl)new SaleControls();
            InitializeComponent();

            var bottomControlElem = bottomControl.GetUIElement();
            bottomControlElem.SetValue(Grid.RowProperty, 2);
            bottomControlElem.SetValue(Grid.ColumnSpanProperty, 2);
            MainGrid.Children.Add(bottomControlElem);

            bottomControl.ConfirmTurnover += (bool isBuyingType, string endpoint) =>
            {
                if (isBuyingType)
                {
                    foreach (var saleProduct in OrderProducts)
                    {
                        Database.AddProductNumber(saleProduct.Id, saleProduct.NumberToSell);
                    }
                }
                else
                {
                    foreach (var saleProduct in OrderProducts)
                    {
                        Database.AddProductNumber(saleProduct.Id, -saleProduct.NumberToSell);
                    }
                }
                ((MainWindow)App.Current.MainWindow).SetOrderControl(null);
            };
            OrderProducts.CollectionChanged += (object sender, NotifyCollectionChangedEventArgs e) =>
            {
                bottomControl.SetButtonsEnabled(OrderProducts.Count > 0);
                //Initiate edit
                if (e.Action == NotifyCollectionChangedAction.Add)
                {
                    var addedId = ((OrderProductData)e.NewItems[0]).Id;
                    for (int i = 0; i < SellingDataGrid.Items.Count; i++)
                    {
                        var saleProductData = (OrderProductData)SellingDataGrid.Items[i];
                        if (saleProductData.Id == addedId)
                        {
                            SellingDataGrid.Focus();
                            SellingDataGrid.CurrentCell = new DataGridCellInfo(SellingDataGrid.Items[i], SellingDataGrid.Columns[2]);
                            SellingDataGrid.BeginEdit();
                            return;
                        }
                    }
                }
            };
        }
        private void CloseButtonClicked(object sender, RoutedEventArgs e)
        {
            ((MainWindow)App.Current.MainWindow).SetOrderControl(null);
        }
        private void SellingDataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            foreach(var cell in e.AddedCells)
            {
                if (cell.Column.DisplayIndex != 2)
                {
                    SellingDataGrid.SelectedCells.Remove(e.AddedCells[0]);
                    SellingDataGrid.CommitEdit();
                }
            }
        }
        private void SellingDataGrid_MouseRightButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }
        private void SaleProductRemoved(object sender, RoutedEventArgs e)
        {
            var s = (MenuItem)sender;
            var contextMenu = (ContextMenu)s.Parent;
            var row = (DataGridRow)contextMenu.PlacementTarget;
            OrderProducts.RemoveAt(row.GetIndex());
        }
    }
    public class OrderProductData
    {
        public Int32 Id { get; }
        public String Title { get { return Database.GetProductData(Id).Title; } }
        public Int32 NumberToSell { set; get; }
        public Int32 Maximum 
        {
            get
            {
                return isBuying ? Int32.MaxValue : Database.GetProductNumber(Id); 
            }
        }

        private bool isBuying;

        public OrderProductData(Int32 id, bool isBuying)
        {
            this.Id = id;
            this.NumberToSell = 0;
            this.isBuying = isBuying;
        }
    }
}