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

namespace InventoryControl.Panel
{
    /// <summary>
    /// Interaction logic for EmptyPanel.xaml
    /// </summary>
    public partial class EmptyPanel : UserControl, IPanel
    {
        public EmptyPanel()
        {
            InitializeComponent();
        }

        int IPanel.MinWidth { get { return 250; } }

        public bool Close()
        {
            return false;
        }

        public bool Collapse()
        {
            throw new NotImplementedException();
        }
    }
}
