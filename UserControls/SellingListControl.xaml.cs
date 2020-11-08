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
    public partial class SellingListControl : UserControl
    {
        public ObservableCollection<SaleProductData> SaleProducts{ get; set; }

        public void UpdateItemsSource() 
        {
            SellingDataGrid.ItemsSource = SaleProducts;
            SellingDataGrid.Items.Refresh();

            if (SaleProducts.Count > 0)
            {
                printButton.IsEnabled = true;
            }
            else
            {
                printButton.IsEnabled = false;
            }
        }
        public SellingListControl()
        {
            InitializeComponent();
            SaleProducts = new ObservableCollection<SaleProductData>();
            SaleProducts.CollectionChanged += (object sender, NotifyCollectionChangedEventArgs e) => { UpdateItemsSource(); };
            
            //Set points of sale combobox items
            storeDecideCombobox.ItemsSource = ProductDatabase.GetPointsOfSales();
        }

        private void CloseButtonClicked(object sender, RoutedEventArgs e)
        {
            
        }

        private void PrintButtonClicked(object sender, RoutedEventArgs e)
        {;
            new Product.Receipt(0, DateTime.Today, storeDecideCombobox.SelectedValue.ToString(), SaleProducts.ToList());
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

        private SaleProductData(Int32 id, String title, Int32 numberToSell)
        {
            this.Id = id;
            this.Title = title;
            this.NumberToSell = numberToSell;
        }
        public SaleProductData(ProductData productData, Int32 numberToSell)
        {
            this.Id = productData.Id;
            this.Title = productData.Title;
            this.NumberToSell = numberToSell;
        }
    }
}