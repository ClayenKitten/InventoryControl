using InventoryControl.Model;
using InventoryControl.ViewModel;
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

namespace InventoryControl.View
{
    /// <summary>
    /// Interaction logic for CounterpartyViewer.xaml
    /// </summary>
    public partial class CounterpartyViewer : UserControl
    {
        public CounterpartyViewer(CounterpartyType type)
        {
            InitializeComponent();
            ((CounterpatyViewerVM)DataContext).ShowPurchasers = type switch
            {
                CounterpartyType.Purchaser => true,
                CounterpartyType.Supplier => false,
                _ => throw new NotImplementedException(),
            };
        }

        private void DataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
            => ((CounterpatyViewerVM)DataContext).RowEditHandler(e);
    }
}
