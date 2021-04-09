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
using System.Windows.Documents;

namespace InventoryControl.View
{
    /// <summary>
    /// Interaction logic for EditProductPanel.xaml
    /// </summary>
    public partial class EditProductPanel : ControlPanel, INotifyPropertyChanged
    {
        public RawProductAdapter ProductData
        {
            get => (RawProductAdapter)GetValue(ProductDataProperty);
            set => SetCurrentValue(ProductDataProperty, value);
        }
        static DependencyProperty ProductDataProperty =
            DependencyProperty.Register("ProductData", typeof(RawProductAdapter), typeof(EditProductPanel));

        public List<string> PossibleMeasurments 
        { 
            get { return Unit.GetPossibleValues(); } 
        }

        private Visibility visibility = Visibility.Collapsed;
        public Visibility ContentVisibility
        {
            get => visibility;
            set
            {
                visibility = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ContentVisibility"));
            }
        }
        public string AutoincrementArticle
        {
            get
            {
                int val = 0;
                var products = Product.Table.ReadAll().Where(x => x.Id != ProductData?.Id).ToList();
                while (products.Any(x => x.Article == val.ToString()))
                {
                    val += 1;
                }
                return val.ToString();
            }
        }

        private TextAdorner adorner;
        public EditProductPanel(long productId)
        {
            InitializeComponent();
            Init(Product.Table.ReadOr(productId, null));
            adorner = new TextAdorner(this);
            adorner.Text = "Выберите товар из списка";
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            AdornerLayer.GetAdornerLayer(this).Add(adorner);
        }
        public override void ReceiveMessage(object sender, object message)
        {
            if (sender is ProductDictionaryViewer)
            {
                Init(Product.Clone((Product)message));
                ContentVisibility = Visibility.Visible;
                adorner.Text = "";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void Init(Product product)
        {
            //ProductData = Product.Table.ReadOr(productId, null);
            ProductData = product;
        }

        private void FormConfirmed(object sender, RoutedEventArgs e)
        {
            if (ProductData.Article.Trim() == "")
                ProductData.Article = new Product().Article;
            if (ProductData.Category.Trim() == "")
                ProductData.Category = "Без категории";

            if (ProductData.Id == -1)
            {
                Product.Table.Create(ProductData);
            }
            else
            {
                Product.Table.Update(ProductData);
            }
            GlobalCommands.ModelUpdated.Execute(null);
            var PM = new PanelManager();
            PM.OpenProductView.Execute();
        }
    }
}
