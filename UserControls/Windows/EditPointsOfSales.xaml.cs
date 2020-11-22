using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
using System.Windows.Shapes;

namespace InventoryControl.UserControls.Windows
{
    /// <summary>
    /// Interaction logic for EditPointsOfSales.xaml
    /// </summary>
    public partial class EditPointsOfSales : MetroWindow
    {
        public class PointOfSale
        {
            public String Title { get; }
            public PointOfSale(String title)
            {
                Title = title;
            }
        }
        public ObservableCollection<PointOfSale> PointsOfSales { get; set; } = new ObservableCollection<PointOfSale>();
        
        public EditPointsOfSales()
        {
            InitializeComponent();

            foreach(var title in ProductDatabase.GetPointsOfSales())
            {
                PointsOfSales.Add(new PointOfSale(title));
            }

            PointsOfSales.CollectionChanged += (object sender, NotifyCollectionChangedEventArgs e) =>
            {
                if (e.Action == NotifyCollectionChangedAction.Remove)
                {
                    String DeletedPointOfSale = ((PointOfSale)e.OldItems[0]).Title;
                    ProductDatabase.DeletePointOfSales(DeletedPointOfSale);
                }
            };
        }
        private void ConfirmButtonClicked(object sender, RoutedEventArgs e)
        {
            ProductDatabase.AddPointOfSales(AddValueTextBox.Text);
            PointsOfSales.Add(new PointOfSale(AddValueTextBox.Text));
            AddValueTextBox.Clear();
        }
    }
}
