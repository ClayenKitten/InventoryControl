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
    public class AdvancedTextbox : TextBox, INotifyPropertyChanged, IValidatable
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
            GetBindingExpression(WatermarkProperty)?.UpdateTarget();
            advancedTextBoxAdorner?.SetWatermark((string)GetValue(WatermarkProperty));

            tb.TextChanged += (_, _1) => UpdateValidation();
            SetValue(TextProperty, GetValue(ValidValueProperty));
        }

        private WatermarkTextboxAdorner advancedTextBoxAdorner;

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            if (e.Property == TextProperty)
            {
                valueWasUpdated = true;
                UpdateValidation();
            }
            else if (e.Property == ValidValueProperty)
            {
                SetCurrentValue(TextProperty, GetValue(ValidValueProperty));
            }
            else if (e.Property == DataContextProperty)
            {
                GetBindingExpression(ValidValueProperty)?.UpdateTarget();
            }
            else if (e.Property == WatermarkProperty)
            {
                advancedTextBoxAdorner?.SetWatermark((string)e.NewValue);
            }
            else if (e.Property == ValidationProperty)
            {
                UpdateValidation();
            }
        }
        // ValidValue
        public string ValidValue { get; set; }
        static DependencyProperty ValidValueProperty =
            DependencyProperty.Register("ValidValue", typeof(string), typeof(AdvancedTextbox),
            new FrameworkPropertyMetadata("", ValidValueChanged, ValidValueCoerced) { BindsTwoWayByDefault = true });
        private static void ValidValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }
        private static object ValidValueCoerced(DependencyObject d, object baseValue)
        {
            var at = d as AdvancedTextbox;
            if (at.Validator.Validate(baseValue.ToString()))
            {
                return baseValue;
            }
            else
            {
                return d.GetValue(ValidValueProperty);
            }
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
            => string.IsNullOrWhiteSpace(Label) ? Visibility.Collapsed : Visibility.Visible;
        public string RequiredStar
        {
            get
            {
                return IsRequired ? "*" : string.Empty;
            }
        }
        // TextBox
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
        public string Watermark { get; set; }
        static DependencyProperty WatermarkProperty =
            DependencyProperty.Register("Watermark", typeof(string), typeof(AdvancedTextbox),
            new PropertyMetadata(""));
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
        private StringValidator Validator
            => new StringValidator(IsRequired, Validation);
        public bool IsValid
            => Validator.Validate(Text, out errorHint);

        public ValidationEnum Validation 
        {
            get => (ValidationEnum)GetValue(ValidationProperty);
            set => SetCurrentValue(ValidationProperty, value);
        }
        static DependencyProperty ValidationProperty =
            DependencyProperty.Register("Validation", typeof(ValidationEnum), typeof(AdvancedTextbox));

        public void UpdateValidation()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsValid"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ErrorTextBlockVisibility"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ErrorHint"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsErrorBorderShown"));
            if (IsValid)
            {
                SetCurrentValue(ValidValueProperty, GetValue(TextProperty));
            }
            GetBindingExpression(WatermarkProperty)?.UpdateTarget();
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
