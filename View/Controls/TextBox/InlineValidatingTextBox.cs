using InventoryControl.Util;
using MahApps.Metro.Controls;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace InventoryControl.View.Controls
{
    public class InlineValidatingTextBox : TextBox, INotifyPropertyChanged, IValidatable
    {
        public bool IsRequired { get; set; }
        ValidationEnum validation; public ValidationEnum Validation
        {
            get { return validation; }
            set
            {
                validation = value;
                UpdateValidation();
            }
        }

        private WatermarkTextboxAdorner adorner;
        private string errorHint;
        string watermark = ""; public string Watermark 
        {
            get => watermark;
            set
            {
                watermark = value;
                adorner?.SetWatermark(value);
            }
        }
        private bool showError = false;
        public bool IsValid
            => new StringValidator(IsRequired, Validation).Validate((string)GetValue(TextProperty), out errorHint);

        public void UpdateValidation()
        {
            adorner?.SetStarShown(IsRequired);
            if (IsValid || !showError)
            {
                adorner?.SetError(false);
                ClearValue(ToolTipProperty);
                ClearValue(ControlsHelper.FocusBorderBrushProperty);
                ClearValue(ControlsHelper.MouseOverBorderBrushProperty);
                ClearValue(BorderBrushProperty);
            }
            else if (showError)
            {
                adorner?.SetError(true);
                var toolTip = new ToolTip()
                {
                    Content = errorHint,
                    Background = new SolidColorBrush(Colors.Red),
                    Foreground = new SolidColorBrush(Colors.White),
                    Placement = PlacementMode.Left,
                    PlacementTarget = this,
                    VerticalOffset = -5,
                };
                SetCurrentValue(ToolTipProperty, toolTip);
                SetCurrentValue(ControlsHelper.FocusBorderBrushProperty, new SolidColorBrush(Color.FromRgb(210, 0, 0)));
                SetCurrentValue(ControlsHelper.MouseOverBorderBrushProperty, new SolidColorBrush(Color.FromRgb(225, 0, 0)));
                SetCurrentValue(BorderBrushProperty, new SolidColorBrush(Colors.Red));
            }
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (e.Property == TextProperty)
            {
                UpdateValidation();
            }
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            adorner = new WatermarkTextboxAdorner(this);
            AdornerLayer.GetAdornerLayer(this).Add(adorner);
            adorner.SetWatermark(Watermark);
            UpdateValidation();
        }
        protected override void OnTextInput(TextCompositionEventArgs e)
        {
            base.OnTextInput(e);
            showError = true;
        }

        static InlineValidatingTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(InlineValidatingTextBox), new FrameworkPropertyMetadata(typeof(InlineValidatingTextBox)));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
