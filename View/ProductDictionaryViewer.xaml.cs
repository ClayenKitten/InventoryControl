using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Data;
using InventoryControl.Model;
using InventoryControl.ViewModel;

namespace InventoryControl.View
{
    /// <summary>
    /// Interaction logic for ProductDictionaryViewer.xaml
    /// </summary>
    public partial class ProductDictionaryViewer : UserControl
    {
        public ProductDictionaryViewer()
        {
            InitializeComponent();
        }

        private void CollectionViewSource_Filter(object sender, FilterEventArgs e)
        {
            var searchString = 
                ((ProductDictionaryViewerVM)this.DataContext).SearchString;

            string productName = ((ProductPresenter)e.Item).Name.ToLower().Replace('ё','е');
            searchString = searchString.ToLower().Replace('ё', 'е');

            e.Accepted = productName.Contains(searchString);
        }
    }
}
