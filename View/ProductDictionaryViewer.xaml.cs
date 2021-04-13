using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using InventoryControl.Model;
using InventoryControl.View.Controls;
using InventoryControl.ViewModel;

namespace InventoryControl.View
{
    /// <summary>
    /// Interaction logic for ProductDictionaryViewer.xaml
    /// </summary>
    public partial class ProductDictionaryViewer : ControlPanel
    {
        public ProductDictionaryViewer()
        {
            InitializeComponent();
        }

        private void MainDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var product = (ProductPresenter)MainDataGrid.SelectedItem;
            if (product != null)
            {
                SendMessage(typeof(EditProductPanel), product.Product);
            }
        }

        private void MainDataGrid_RowClicked(object sender, MouseButtonEventArgs e, DataGridRow row)
        {
            var product = (ProductPresenter)row.Item;
            if (e.ClickCount >= 2 && product != null)
            {
                SendMessage(typeof(TransactionProductsViewer), product.Product.Id);
            }
        }
    }
}
