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
        }

        private void DataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {

        }

        private void AddButtonClick(object sender, RoutedEventArgs e)
        {
            PointOfSales.Table.Create(new PointOfSales
            (
                id: -1,
                name: NameTB.Text,
                address: AddressTB.Text
            ));
            foreach (var elem in InputForm.Children)
            {
                if (elem is AdvancedTextbox)
                {
                    (elem as AdvancedTextbox).Text = "";
                }
            }

            GlobalCommands.ModelUpdated.Execute(null);
        }
    }
}
