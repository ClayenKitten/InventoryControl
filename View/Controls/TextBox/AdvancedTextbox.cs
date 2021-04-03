using InventoryControl.Util;
using MahApps.Metro.Controls;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;

namespace InventoryControl.View.Controls
{
    public class AdvancedTextbox : Control, INotifyPropertyChanged, IValidatable
    {
        static AdvancedTextbox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AdvancedTextbox),
                new FrameworkPropertyMetadata(typeof(AdvancedTextbox)));
        }
        public AdvancedTextbox()
        {
            //Create style for internal TextBox
            var style = new Style(typeof(TextBox), Application.Current.TryFindResource(typeof(TextBox)) as Style);
            var errorDataTrigger = new DataTrigger()
            {
                Binding = new Binding("IsErrorBorderShown") { RelativeSource = RelativeSource.TemplatedParent },
                Value = "False"
            };
            errorDataTrigger.Setters.Add(new Setter(BorderBrushProperty,
                new SolidColorBrush(Colors.Red)));
            errorDataTrigger.Setters.Add(new Setter(ControlsHelper.FocusBorderBrushProperty,
                new SolidColorBrush(Color.FromRgb(210, 0, 0))));
            errorDataTrigger.Setters.Add(new Setter(ControlsHelper.MouseOverBorderBrushProperty,
                new SolidColorBrush(Color.FromRgb(225, 0, 0))));
            style.Triggers.Add(errorDataTrigger);
            style.Seal();
            InnerTextBoxStyle = style;
            
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            var tb = ((TextBox)this.FindChild<TextBox>());
            advancedTextBoxAdorner = new WatermarkTextboxAdorner(tb);
            AdornerLayer.GetAdornerLayer(this).Add(advancedTextBoxAdorner);
            advancedTextBoxAdorner?.SetWatermark(watermark);

            tb.TextChanged += (_, _1) => UpdateValidation();
        }

        private WatermarkTextboxAdorner advancedTextBoxAdorner;

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
        }

        // Label
        string label; public string Label
        {
            get { return label; }
            set
            {
                label = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Label"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("LabelVisibility"));
            }
        }
        public Visibility LabelVisibility
            => string.IsNullOrWhiteSpace(Label)?Visibility.Collapsed:Visibility.Visible;
        public string RequiredStar
        {
            get
            {
                return IsRequired ? "*" : string.Empty;
            }
        }
        // TextBox
        string text = ""; public string Text
        {
            get { return text; }
            set
            {
                text = value;
                valueWasUpdated = true;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Text"));
                UpdateValidation();
            }
        }
        bool isRequired = false; public bool IsRequired
        {
            get { return isRequired; }
            set
            {
                isRequired = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsRequired"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("RequiredStar"));
                UpdateValidation();
            }
        }
        string watermark = ""; public string Watermark
        {
            get
            {
                return watermark;
            }
            set
            {
                watermark = value;
                advancedTextBoxAdorner?.SetWatermark(value);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Watermark"));
            }
        }
        // Error TextBlock
        private bool valueWasUpdated = false;
        public bool IsErrorTextBlockShown
        {
            get
            {
                if (!valueWasUpdated)
                {
                    return false;
                }
                else
                {
                    return !IsValid;
                }
            }
        }
        public bool IsErrorBorderShown
        {
            get
            {
                if (!valueWasUpdated)
                {
                    return true;
                }
                else
                {
                    return IsValid;
                }
            }
        }
        public Visibility ErrorTextBlockVisibility
            => IsErrorTextBlockShown ? Visibility.Visible : Visibility.Collapsed;

        public Style InnerTextBoxStyle { get; }

        //Validation
        private string errorHint; public string ErrorHint 
        { 
            get
            {
                return errorHint;
            }
            private set
            {
                errorHint = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ErrorHint"));
            }
        }
        public bool IsValid
            => new StringValidator(IsRequired, Validation).Validate(Text, out errorHint);

        ValidationEnum validation; public ValidationEnum Validation
        {
            get { return validation; }
            set
            {
                validation = value;
                UpdateValidation();
            }
        }

        public void UpdateValidation()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsValid"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ErrorTextBlockVisibility"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ErrorHint"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsErrorBorderShown"));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
