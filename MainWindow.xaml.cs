using System;
using MahApps.Metro.Controls;
using System.Windows;
using System.Windows.Controls;
using InventoryControl.UserControls;
using InventoryControl.UserControls.OrderControl;

namespace InventoryControl
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private const int OrderControlColumnIndex = 3;
        
        public void SetOrderControl(bool? isBuying) {}
        public OrderControl GetOrderControl() 
        {
            return null;
        }
    }
}