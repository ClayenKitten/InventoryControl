using InventoryControl.Model;
using InventoryControl.Model.Product;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace InventoryControl.Panel
{
    /// <summary>
    /// Interaction logic for EditProductPanel.xaml
    /// </summary>
    public partial class EditProductPanel : UserControl, IPanel
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

            var presenter = new ProductPresenter(this.productData);

            TitleAT.Value = presenter.Title;
            AmountAT.Value = presenter.Packing;
            MeasurementCB.SelectedItem = productData.Measurement.GetPostfix();
            BuyPriceAT.Value = presenter.PurchasePrice;
            SalePriceAT.Value = presenter.SalePrice;
            WeightAT.Value = presenter.Packing;
        }

        int IPanel.MinWidth { get { return 250; } }

        public bool Close()
        {
            var r = MessageBox.Show("Закрыть панель?", "Изменения не сохранены", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            return r == MessageBoxResult.No;
        }

        public bool Collapse()
        {
            throw new NotImplementedException();
        }

        private void ConfirmClick(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
