using System;
using System.Windows;
using System.Windows.Controls;

namespace InventoryControl.UserControls
{
    /// <summary>
    /// Interaction logic for AdvancedTextbox.xaml
    /// </summary>
    public partial class AdvancedTextbox : UserControl
    {
        private String label;
        public String Label 
        { 
            get { return label; }
            set
            {
                label = value;
                if (label == "")
                    LabelTextBlock.Visibility = Visibility.Collapsed;
                else
                    LabelTextBlock.Visibility = Visibility.Visible;
            }
        }
        private static DependencyProperty LabelProperty =
            DependencyProperty.Register("Label", typeof(String), typeof(AdvancedTextbox), new PropertyMetadata(""));

        private String textboxValue;
        public String Value
        { 
            get { return textboxValue; }
            set 
            {
                textboxValue = value;
                InnerTextbox.Text = value;
            }
        }
        private static DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(String), typeof(AdvancedTextbox), new PropertyMetadata(""));

        public ValidationRule ValidationRule { get; set; }
        private static DependencyProperty ValidationRuleProperty =
            DependencyProperty.Register("ValidationRule", typeof(ValidationRule), typeof(AdvancedTextbox));

        public bool IsRequired { get; set; }
        private static DependencyProperty IsRequiredProperty =
            DependencyProperty.Register("IsRequired", typeof(bool), typeof(AdvancedTextbox), new PropertyMetadata(false));

        public AdvancedTextbox()
        {
            InitializeComponent();
            this.validationRule.NotNull = IsRequired;
        }
    }
}
