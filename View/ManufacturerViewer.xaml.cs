using InventoryControl.Model;
using InventoryControl.View.Controls;
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
    /// Interaction logic for ManufacturerViewer.xaml
    /// </summary>
    public partial class ManufacturerViewer : ControlPanel
    {
        public ManufacturerViewer()
        {
            InitializeComponent();
            InputForm.Confirmed += (_, _1) => ConfirmForm();
        }

        private void ConfirmForm()
        {
            Manufacturer.Table.Create(new Manufacturer
            (
                id: -1,
                name: NameTB.Text
            ));
            InputForm.ClearAllTextBoxes();
            GlobalCommands.ModelUpdated.Execute(null);
        }
    }
}
