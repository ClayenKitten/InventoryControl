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
    public partial class EditProductPanel : UserControl, INotifyPropertyChanged
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

        private void Init()
        {
            UpdateValidness();
            //Go through inputs and update validness
            //TODO: Create container that will incapsulate this
            InputsContainer.Children.OfType<AdvancedTextbox>().ToList()
                .ForEach((x) => x.PropertyChanged += (sender, e) =>
                {
                    if (e.PropertyName == "IsValid")
                        UpdateValidness();
                });
        }
        private void UpdateValidness()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsConfirmButtonEnabled"));
        }

        public EditProductPanel()
        {
            productData = null;
            InitializeComponent();
            Init();
        }
        public EditProductPanel(int productId)
        {
            productData = ProductMapper.Read(productId);
            InitializeComponent();

            TitleAT.Value = productData.Name;
            AmountAT.Value = productData.Measurement.GetFormattedValue();
            MeasurementCB.SelectedItem = productData.Measurement.GetPostfix();
            BuyPriceAT.Value = productData.PurchasePrice.GetFormattedValue();
            SalePriceAT.Value = productData.SalePrice.GetFormattedValue();
            ArticleAT.Value = productData.Article.ToString();

            Init();
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
