using InventoryControl.Model;
using InventoryControl.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using InventoryControl.View.Controls;

namespace InventoryControl.View
{
    /// <summary>
    /// Interaction logic for EditProductPanel.xaml
    /// </summary>
    public partial class EditProductPanel : ControlPanel, INotifyPropertyChanged
    {
        private Product productData;
        public string Header { get { return productData != null ? "Изменение записи товара" : "Создание записи товара"; } }
        public List<string> PossibleMeasurments 
        { 
            get { return Unit.GetPossibleValues(); } 
        }
        public bool IsConfirmButtonEnabled
        {
            get => InputsContainer.Children.OfType<AdvancedTextbox>().All((x) => { return x.IsValid; });
        }

        public EditProductPanel()
        {
            productData = null;
            InitializeComponent();
        }
        public EditProductPanel(int productId)
        {
            productData = Product.Table.Read(productId);
            InitializeComponent();

            TitleAT.Text = productData.Name;
            AmountAT.Text = productData.Measurement.GetFormattedValue();
            MeasurementCB.SelectedItem = productData.Measurement.GetPostfix();
            BuyPriceAT.Text = productData.PurchasePrice.GetFormattedValue();
            SalePriceAT.Text = productData.SalePrice.GetFormattedValue();
            ArticleAT.Text = productData.Article.ToString();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void FormConfirmed(object sender, EventArgs e)
        {
            if (productData == null)
            {
                Product.Table.Create(
                    new Product
                    (
                        id: -1,
                        name: TitleAT.Text,
                        purchasePrice: double.Parse(BuyPriceAT.Text, CultureInfo.InvariantCulture),
                        salePrice: double.Parse(SalePriceAT.Text, CultureInfo.InvariantCulture),
                        unitValue: double.Parse(AmountAT.Text, CultureInfo.InvariantCulture),
                        unit: new Unit(MeasurementCB.SelectedIndex).value,
                        article: ArticleAT.Text,
                        manufacturerId: -1,
                        category: ""
                    )
                );
            }
            else
            {
                Product.Table.Update(
                    new Product
                    (
                        id: productData.Id,
                        name: TitleAT.Text,
                        purchasePrice: double.Parse(BuyPriceAT.Text, CultureInfo.InvariantCulture),
                        salePrice: double.Parse(SalePriceAT.Text, CultureInfo.InvariantCulture),
                        unitValue: double.Parse(AmountAT.Text, CultureInfo.InvariantCulture),
                        unit: new Unit(MeasurementCB.SelectedIndex).value,
                        article: ArticleAT.Text,
                        manufacturerId: -1,
                        category: ""
                    )
                );
            }

            GlobalCommands.ModelUpdated.Execute(null);
            var PM = new PanelManager();
            PM.OpenProductView.Execute();
        }
    }
}
