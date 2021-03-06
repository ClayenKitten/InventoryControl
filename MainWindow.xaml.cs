using System;
using MahApps.Metro.Controls;
using System.Windows;
using System.Windows.Controls;
using InventoryControl.View;
using System.Windows.Shapes;
using System.Windows.Media;
using InventoryControl.ViewModel;

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
            this.SetPanel(initPanel);

            GlobalCommands.EditProduct.Executed += (productId) =>
            {
                this.SetPanel
                (
                    new AdaptiveStackControl
                    (
                        AdaptiveStackScheme.PRIORITIZED,
                        new ProductDictionaryViewer(),
                        new EditProductPanel((int)productId)
                    )
                );
            };
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