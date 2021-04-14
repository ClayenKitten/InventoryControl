using InventoryControl.Model;
using InventoryControl.View.Controls;
using InventoryControl.ViewModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
namespace InventoryControl.View
{
    /// <summary>
    /// Interaction logic for StorageListViewer.xaml
    /// </summary>
    public partial class StorageListViewer : ControlPanel
    {
        public StorageListViewer()
        {
            InitializeComponent();
            InputForm.Confirmed += (_, _1) =>
            {
                AddButtonClick();
            };
        }
        private void AddButtonClick()
        {
            Storage.Table.Create(new Storage
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
