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
        public List<string> PossibleMeasurments 
        { 
            get { return Unit.GetPossibleValues(); } 
        }

        public EditProductPanel(int? productId)
        {
            InitializeComponent();
            Init(productId);
        }

        private string AutoincrementArticle
        {
            get
            {
                int val = 0;
                var products = Product.Table.ReadAll();
                while (products.Any(x => x.Article == val.ToString()))
                {
                    val += 1;
                }
                return val.ToString();
            }
        }
        public override void ReceiveMessage(object sender, object message)
        {
            if (sender is ProductDictionaryViewer)
            {
                Init((int)message);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void Init(int? productId)
        {
            if (productId.HasValue)
            {
                productData = Product.Table.Read(productId.Value);
                TitleAT.Text = productData.Name;
                AmountAT.Text = productData.Measurement.GetFormattedValue();
                MeasurementCB.SelectedItem = productData.Measurement.GetPostfix();
                BuyPriceAT.Text = productData.PurchasePrice.GetFormattedValue();
                SalePriceAT.Text = productData.SalePrice.GetFormattedValue();
                ArticleAT.Text = productData.Article.ToString();
            }
            else
            {
                productData = null;
            }
            ArticleAT.Watermark = AutoincrementArticle;
        }

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
        private void PackingTypeChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MeasurementCB.SelectedIndex == 0)
            {
                AmountAT.Watermark = "1.00";
                AmountAT.Validation = Util.ValidationEnum.Weight;
            }
            else
            {
                AmountAT.Watermark = "1";
                AmountAT.Validation = Util.ValidationEnum.Integer;
            }
        }
    }
}
