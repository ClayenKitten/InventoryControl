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

        public EditProductPanel(long productId)
        {
            InitializeComponent();
            Init(productId);
        }
        public override void ReceiveMessage(object sender, object message)
        {
            if (sender is ProductDictionaryViewer)
            {
                Init((long)message);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void Init(long productId)
        {
            ProductData = Product.Table.ReadOr(productId, null);
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
