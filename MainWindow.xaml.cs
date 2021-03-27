using System;
using MahApps.Metro.Controls;
using System.Windows;
using System.Windows.Controls;
using InventoryControl.View;
using System.Windows.Shapes;
using System.Windows.Media;
using InventoryControl.ViewModel;
using InventoryControl.View.Controls;

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

            Panel = Panel; //set init panel

            GlobalCommands.EditProduct.Executed += (productId) =>
            {
                Panel = new DualControlPanelContainer(
                    new ProductDictionaryViewer(),
                    new EditProductPanel((int)productId)
                );
            };
        }

        public ControlPanelContainer Panel 
        {
            get
            {
                foreach (var child in MainWindowGrid.Children)
                {
                    if (child is ControlPanelContainer)
                    {
                        return (ControlPanelContainer)child;
                    }
                }
                return new SingleControlPanelContainer(new StorageViewer(0));
            }
            set 
            {
                //Dispose if container already set
                var cur = MainWindowGrid.Children[MainWindowGrid.Children.Count - 1];
                if (cur is ControlPanelContainer adaptiveStackControl)
                {
                    adaptiveStackControl.Dispose();
                }
                //Set new container
                value.SetValue(Grid.RowProperty, 1);
                foreach (var child in MainWindowGrid.Children)
                {
                    if (child is ControlPanelContainer)
                    {
                        MainWindowGrid.Children.Remove((UIElement)child);
                        MainWindowGrid.Children.Add(value);
                        return;
                    }
                }
                MainWindowGrid.Children.Add(value);
            }
        }
    }
}