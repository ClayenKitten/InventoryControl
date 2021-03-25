using InventoryControl.View.Controls;
using System.Windows.Controls;
namespace InventoryControl.View
{
    /// <summary>
    /// Interaction logic for PointsOfSalesViewer.xaml
    /// </summary>
    public partial class PointsOfSalesViewer : ControlPanel
    {
        public PointsOfSalesViewer()
        {
            InitializeComponent();
        }

        private void DataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {

        }
    }
}
