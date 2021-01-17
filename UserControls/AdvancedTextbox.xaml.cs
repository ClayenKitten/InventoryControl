using System;
using System.Windows;
using System.Windows.Controls;

namespace InventoryControl.UserControls
{
    public enum TextboxValidationType
    {
        Text,
        Integer,
        Float,
    }
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

        public TextboxValidationType InputType { get; set; }
        private static DependencyProperty InputTypeProperty =
            DependencyProperty.Register("InputType", typeof(TextboxValidationType), typeof(AdvancedTextbox),
                new PropertyMetadata(TextboxValidationType.Text));

        public AdvancedTextbox()
        {
            InitializeComponent();
        }
    }
}
