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
using InventoryControl.Model;

namespace InventoryControl.UserControls.Windows
{
    /// <summary>
    /// Interaction logic for CreateProduct.xaml
    /// </summary>
    public partial class EditProductWindow : MetroWindow
    {
        public EditProductWindow()
        {
            InitializeComponent();
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
            throw new NotImplementedException();
        }
    }
}
