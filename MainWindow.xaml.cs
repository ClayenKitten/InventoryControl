using System;
using MahApps.Metro.Controls;
using System.Windows;
using System.Windows.Controls;
using InventoryControl.View;
using System.Windows.Shapes;
using System.Windows.Media;

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

            var initPanel = new DynamicLayoutControl(
                new DynamicLayoutScheme(Orientation.Horizontal,
                    new DynamicLayoutSchemeElement(true, 1, 1)
                ),
                new UIElement(),
                new StorageViewer(0)
            );
            initPanel.SetValue(Grid.RowProperty, 1);
            MainWindowGrid.Children.Add(initPanel);
        }
        public DividedPanel Panel { get; set; }
    }
}