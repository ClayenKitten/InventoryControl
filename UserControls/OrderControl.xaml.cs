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

namespace InventoryControl.UserControls
{
    /// <summary>
    /// Interaction logic for SellingListControl.xaml
    /// </summary>
    public partial class OrderControl : UserControl
    {
        public ObservableCollection<SaleProductData> SaleProducts{ get; set; }

        public void UpdateButtonsState() 
        {
            if (SaleProducts.Count > 0)
            {
                printButton.IsEnabled = true;
                confirmButton.IsEnabled = true;
            }
            else
            {
                printButton.IsEnabled = false;
                confirmButton.IsEnabled = false;
            }
        }
        public OrderControl()
        {
            InitializeComponent();
            SaleProducts = new ObservableCollection<SaleProductData>();
            SellingDataGrid.SetBinding(ItemsControl.ItemsSourceProperty, new Binding { Source = SaleProducts });
            SaleProducts.CollectionChanged += (object sender, NotifyCollectionChangedEventArgs e) =>
            {
                UpdateButtonsState();
                if (e.Action == NotifyCollectionChangedAction.Add)
                {
                    var addedId = ((SaleProductData)e.NewItems[0]).Id;
                    for (int i = 0; i < SellingDataGrid.Items.Count; i++)
                    {
                        var saleProductData = (SaleProductData)SellingDataGrid.Items[i];
                        if (saleProductData.Id == addedId)
                        {
                            SellingDataGrid.Focus();
                            SellingDataGrid.CurrentCell = new DataGridCellInfo(SellingDataGrid.Items[i], SellingDataGrid.Columns[2]);
                            SellingDataGrid.BeginEdit();
                            return;
                        }
                    }
                }
                else if (e.Action == NotifyCollectionChangedAction.Remove)
                {
                    
                }
            };
            
            //Set points of sale combobox items
            storeDecideCombobox.ItemsSource = ProductDatabase.GetPointsOfSales();
        }

        private void CloseButtonClicked(object sender, RoutedEventArgs e)
        {
            ((MainWindow)App.Current.MainWindow).SetOrderControl(false, false);
        }
        private void PrintButtonClicked(object sender, RoutedEventArgs e)
        {
            new Product.Receipt(0, DateTime.Today, storeDecideCombobox.SelectedValue.ToString(), SaleProducts.ToList());
        }
        private void ConfirmButtonClicked(object sender, RoutedEventArgs e)
        {
            
        }
        private void SellingDataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            foreach(var cell in e.AddedCells)
            {
                if (cell.Column.DisplayIndex != 2)
                {
                    SellingDataGrid.SelectedCells.Remove(e.AddedCells[0]);
                }
                else
                {
                    SellingDataGrid.BeginEdit();
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
            SaleProducts.RemoveAt(row.GetIndex());
        }
    }
    public class SaleProductData
    {
        public Int32 Id { get; }
        public String Title { get; set; }
        public Int32 NumberToSell { get; set;  }

        public SaleProductData(Int32 id, Int32 numberToSell)
        {
            this.Id = id;
            this.Title = ProductDatabase.GetProductData(Id).Title;
            this.NumberToSell = numberToSell;
        }
    }
}