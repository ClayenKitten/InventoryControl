using System;
using MahApps.Metro.Controls;
using System.Windows;
using System.Windows.Controls;
using InventoryControl.UserControls;
using InventoryControl.UserControls.OrderControl;
using InventoryControl.Panel;

namespace InventoryControl
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public IPanel OpenedPanel { get; private set; }
        public MainWindow()
        {
            InitializeComponent();
            this.SetPanel(new EmptyPanel());
        }

        private const int OrderControlColumnIndex = 3;
        private void MakeSearch(string searchString)
        {
            throw new NotImplementedException();
            /*DataGridContent.Clear();
            foreach (ProductData product in Database.GetProductData())
            {
                String search = Searchbox.Text.ToLower().Replace('ё', 'е').Trim();
                String title = product.Title.ToLower().Replace('ё', 'е').Trim();

                if (title.Contains(search))
                {
                    DataGridContent.Add(product);
                }
            }*/
        }
        public void SetOrderControl(bool? isBuying) {}
        public OrderControl GetOrderControl() 
        {
            return null;
        }

        public void SetPanel<T>(T panel) where T : UIElement, IPanel
        {
            if (OpenedPanel != null && OpenedPanel.Close())
                return;
            PanelContainer.Children.Clear();
            PanelContainer.Children.Add(panel);
            PanelColumn.MinWidth = panel.MinWidth;
            OpenedPanel = panel;
        }
    }
}