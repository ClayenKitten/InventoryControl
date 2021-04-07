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

        public EditProductPanel(int? productId)
        {
            InitializeComponent();
            Init(productId);
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
                ProductData = Product.Table.Read(productId.Value);
            }
            else
            {
                ProductData = new Product();
            }
        }

        private void FormConfirmed(object sender, RoutedEventArgs e)
        {
            Product.Table.Update(ProductData);
            GlobalCommands.ModelUpdated.Execute(null);
            var PM = new PanelManager();
            PM.OpenProductView.Execute();
        }
    }
}
