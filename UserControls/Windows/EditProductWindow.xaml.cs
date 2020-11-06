using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MahApps.Metro.Controls;

namespace InventoryControl.UserControls.Windows
{
    /// <summary>
    /// Interaction logic for CreateProduct.xaml
    /// </summary>
    public partial class EditProductWindow : MetroWindow
    {
        ProductData productData;
        public EditProductWindow(ProductData changedProduct)
        {
            InitializeComponent();

            if(changedProduct == null)
            {
                productData = ProductDatabase.CreateProduct();
            }
        }
    }
}
