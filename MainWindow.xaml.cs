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

            var initPanel = new AdaptiveStackControl(AdaptiveStackScheme.SINGLE, new StorageViewer(0));
            initPanel.SetValue(Grid.RowProperty, 1);
            MainWindowGrid.Children.Add(initPanel);
        }
        public void SetPanel(AdaptiveStackControl content)
        {
            content.SetValue(Grid.RowProperty, 1);
            if (MainWindowGrid.Children.Count < 2)
                MainWindowGrid.Children.Add(content);
            else
            {
                MainWindowGrid.Children.RemoveAt(1);
                MainWindowGrid.Children.Add(content);
            }
        }
    }
}