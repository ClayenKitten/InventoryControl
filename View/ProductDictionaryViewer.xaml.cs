using System;
using System.Collections.Generic;
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
using InventoryControl.Model.Product;
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
                ((ProductDictionaryViewModel)this.DataContext).SearchString;

            string productName = ((ProductPresenter)e.Item).Name.ToLower().Replace('ё','е');
            searchString = searchString.ToLower().Replace('ё', 'е');

            e.Accepted = productName.Contains(searchString);
        }
    }
}
