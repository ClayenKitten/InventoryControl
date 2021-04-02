using InventoryControl.Model;
using InventoryControl.View.Controls;
using InventoryControl.ViewModel;
using System.ComponentModel;
using System.Windows;
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
            InputForm.Confirmed += (_, _1) =>
            {
                AddButtonClick();
            };
        }

        private void DataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {

        }

        private void AddButtonClick()
        {
            PointOfSales.Table.Create(new PointOfSales
            (
                id: -1,
                name: NameTB.Text,
                address: AddressTB.Text
            ));
            InputForm.ClearAllTextBoxes();

            GlobalCommands.ModelUpdated.Execute(null);
        }
    }
}
