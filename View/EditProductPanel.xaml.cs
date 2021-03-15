using InventoryControl.Model;
using InventoryControl.ViewModel;
using SharedControls.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace InventoryControl.View
{
    /// <summary>
    /// Interaction logic for EditProductPanel.xaml
    /// </summary>
    public partial class EditProductPanel : UserControl, INotifyPropertyChanged
    {
        private Product productData;
        public String Header { get { return productData != null ? "Изменение записи товара" : "Создание записи товара"; } }
        public List<String> PossibleMeasurments 
        { 
            get { return Unit.GetPossibleValues(); } 
        }
        public bool IsConfirmButtonEnabled
        {
            get => InputsContainer.Children.OfType<AdvancedTextbox>().All((x) => { return x.IsValid; });
        }

        private void BindNotifier()
        {
            InputsContainer.Children.OfType<AdvancedTextbox>().ToList().ForEach((x) 
                => { x.PropertyChanged += InputPropertyChanged; });
        }
        private void InputPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsValid")
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsConfirmButtonEnabled"));
        }

        public EditProductPanel()
        {
            InitializeComponent();
            this.productData = null;
            BindNotifier();
        }
        public EditProductPanel(int productId)
        {
            InitializeComponent();
            this.productData = ProductMapper.Read(productId);

            TitleAT.Value = productData.Name;
            AmountAT.Value = productData.Measurement.GetFormattedValue();
            MeasurementCB.SelectedItem = productData.Measurement.GetPostfix();
            BuyPriceAT.Value = productData.PurchasePrice.GetFormattedValue();
            SalePriceAT.Value = productData.SalePrice.GetFormattedValue();

            BindNotifier();
        }

        private void ConfirmClick(object sender, RoutedEventArgs e)
        {
            ProductMapper.Create(
                new Product
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
            var PM = new PanelManager();
            PM.OpenProductView.Execute();
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
