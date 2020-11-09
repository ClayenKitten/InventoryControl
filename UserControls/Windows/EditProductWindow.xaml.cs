using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MahApps.Metro.Controls;

namespace InventoryControl.UserControls.Windows
{
    /// <summary>
    /// Interaction logic for CreateProduct.xaml
    /// </summary>
    public partial class EditProductWindow : MetroWindow
    {
        int? Id;
        public EditProductWindow(int? id)
        {
            InitializeComponent();

            packingCB.ItemsSource = Measurement.GetPossibleValues();
            titleTB.Focus();
            if(id.HasValue)
            {
                var productData = ProductDatabase.GetProductData(id.Value);
                titleTB.Text = productData.Title;
                weightTB.Text = productData.weight.ToString();
                packingCB.SelectedIndex = productData.packing;
                purchasePriceTB.Text = productData.purchasePrice.ToString();
                salePriceTB.Text = productData.salePrice.ToString();
            }
            Id = id;
        }

        private void Confirm()
        {
            
        }

        private void IntValidation(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text) & !e.Text.StartsWith("0");
        }
        private void DoubleValidation(object sender, TextCompositionEventArgs e)
        {
            var text = ((TextBox)sender).Text+e.Text;
            try
            {
                if ((text.Contains(".") & text.Contains(",")))
                {
                    e.Handled = true;
                }
                else
                {
                    Double.Parse(text, CultureInfo.InvariantCulture);
                    e.Handled = false;
                }
            }
            catch(FormatException)
            {
                e.Handled = true;
            }
        }
        private void RemoveSpacesKeyUp(object sender, KeyEventArgs e)
        {
            ((TextBox)sender).Text = ((TextBox)sender).Text.Replace(" ", "");
        }
        private void Confirm_Click(object sender, RoutedEventArgs e)
        {

            //Check for bad options
            if
            (
                titleTB.Text == "" ||
                packingCB.SelectedIndex < 0 ||
                packingCB.SelectedIndex > 1
            )
            {
                return;
            }

            try
            {
                ProductDatabase.CreateOrEditProduct
                (
                    Id, 
                    titleTB.Text, 
                    weightTB.Text, 
                    packingCB.SelectedIndex, 
                    purchasePriceTB.Text, 
                    salePriceTB.Text
                );
            }
            catch(ArgumentException)
            {
                MessageBox.Show("Не все обязательные поля заполнены","Невозможно сохранить изменения", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            DialogResult = true;
        }
    }
}
