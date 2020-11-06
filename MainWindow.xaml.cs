using System;
using System.Collections.ObjectModel;
using MahApps.Metro.Controls;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using System.IO;
using Microsoft.Win32;
using System.Linq;

namespace InventoryControl
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {

        public MainWindow()
        {
            InitializeComponent();

            
        }

        private void MakeSearch(string searchString, List<ProductData> products)
        {
            var dataGrid = (DataGrid)MainDataGrid.Content;
            
            int sortIndex = 0;
            ListSortDirection? direction = null;
            for (int i = 0; i < dataGrid.Columns.Count; i++)
            {
                if (dataGrid.Columns[i].SortDirection != null)
                {
                    sortIndex = i;
                    direction = dataGrid.Columns[i].SortDirection;
                    break;
                }
            }

            var filtered =  new List<ProductData>();
            foreach (ProductData product in products)
            {
                String search = Searchbox.Text.ToLower().Replace('ё', 'е').Trim();
                String title = product.Title.ToLower().Replace('ё', 'е').Trim();

                if (title.Contains(search)) { filtered.Add(product); }
            }
            dataGrid.ItemsSource = filtered;
            dataGrid.Columns[sortIndex].SortDirection = direction;
        }


        private void SearchBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            
        }
        private void LoadBackupButtonClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = AppContext.BaseDirectory+"Backups\\";
            openFileDialog.Filter = "Database (*.db)|*.db|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                SaveBackupButtonClick();
                File.Copy(openFileDialog.FileName, "Database.db", true);
            }
        }
        private void SaveBackupButtonClick(object sender=null, RoutedEventArgs e=null)
        {
            
            System.IO.Directory.CreateDirectory("Backups");
            File.Copy("Database.db", "Backups\\"+DateTime.Now.ToString("YY-MM-dd HH-mm-dd") + ".db", true);
        }
        private void AddProductClick(object sender, RoutedEventArgs e)
        {
            //new UserControls.Windows.CreateProduct(null).ShowDialog();
        }
        public void SendButtonClicked(object sender, RoutedEventArgs e)
        {
            Int32 Id = (Int32)((Button)sender).CommandParameter;

            foreach (var saleProduct in SellingList.SaleProducts)
            {
                if (saleProduct.Id == Id)
                    return;
            }
            SellingList.SaleProducts.Add(new UserControls.SaleProductData(ProductDatabase.GetProductData(Id), 1));
        }
    }
}