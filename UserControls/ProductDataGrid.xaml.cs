using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InventoryControl.UserControls
{
    /// <summary>
    /// Interaction logic for ProductDataGrid.xaml
    /// </summary>
    public partial class ProductDataGrid : UserControl
    {

        public ProductDataGrid()
        {
            InitializeComponent();

            
        }
        

        private void SendButtonClicked(object sender, RoutedEventArgs e)
        {
            var t = (MainWindow)this.Parent;
            t.SendButtonClicked(sender, e);
        }
    }
}
