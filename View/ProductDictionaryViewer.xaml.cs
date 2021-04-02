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
        private void MainDataGrid_RowClicked(object sender, MouseButtonEventArgs e, DataGridRow row)
        {
            if (e.ClickCount == 2)
            {
                var panels = ((MainWindow)App.Current.MainWindow).Panel.ControlPanels;
                panels.Remove(this);
                foreach (var panel in panels)
                {
                    if (panel.DataContext is TransactionProductsViewerVM vm)
                    {
                        vm.AddProduct(((ProductPresenter)row.Item).Id);
                    }
                }
            }
        }
    }
}
