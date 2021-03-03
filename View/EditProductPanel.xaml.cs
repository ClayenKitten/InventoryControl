using InventoryControl.Model;
using InventoryControl.Model.Product;
using InventoryControl.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace InventoryControl.View
{
    /// <summary>
    /// Interaction logic for EditProductPanel.xaml
    /// </summary>
    public partial class EditProductPanel : UserControl
    {
        private ProductData productData;
        public String Header { get { return productData != null ? "Изменение записи товара" : "Создание записи товара"; } }
        public List<String> PossibleMeasurments 
        { 
            get { return Unit.GetPossibleValues(); } 
        }

        public EditProductPanel()
        {
            InitializeComponent();
            this.productData = null;
        }
        public EditProductPanel(int productId)
        {
            InitializeComponent();
            this.productData = ProductDataMapper.Read(productId);

            TitleAT.Value = productData.Name;
            AmountAT.Value = productData.Measurement.GetFormattedValue();
            MeasurementCB.SelectedItem = productData.Measurement.GetPostfix();
            BuyPriceAT.Value = productData.PurchasePrice.GetFormattedValue();
            SalePriceAT.Value = productData.SalePrice.GetFormattedValue();
        }
        private void ConfirmClick(object sender, RoutedEventArgs e)
        {
            ProductDataMapper.Create(
                new ProductData
                (
                    id: -1,
                    name: TitleAT.Value,
                    purchasePrice: double.Parse(BuyPriceAT.Value, CultureInfo.InvariantCulture),
                    salePrice: double.Parse(SalePriceAT.Value, CultureInfo.InvariantCulture),
                    value: double.Parse(AmountAT.Value, CultureInfo.InvariantCulture),
                    unit: new Unit(MeasurementCB.SelectedIndex).value,
                    article: int.Parse(ArticleAT.Value, CultureInfo.InvariantCulture)
                )
            );
            GlobalCommands.ModelUpdated.Execute(null);            
        }
    }
}
