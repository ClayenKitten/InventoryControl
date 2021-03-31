using InventoryControl.Util;
using MahApps.Metro.Controls;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace InventoryControl.View.Controls
{
    public class AdvancedTextbox : Control, INotifyPropertyChanged
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
                Binding = new Binding("IsErrorShown") { RelativeSource = RelativeSource.TemplatedParent },
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

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

        }

        private string label;
        public string Label
        {
            get { return label; }
            set
            {
                label = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Label"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("LabelVisibility"));
            }
        }
        public Visibility LabelVisibility => string.IsNullOrWhiteSpace(Label)?Visibility.Collapsed:Visibility.Visible;
        private string textboxValue = "";
        public string Text
        {
            get { return textboxValue; }
            set
            {
                textboxValue = value;
                valueWasUpdated = true;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Value"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsValid"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsErrorShown"));
            }
        }
        private bool isRequired;
        public bool IsRequired
        {
            get { return isRequired; }
            set
            {
                isRequired = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsRequired"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsValid"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsErrorShown"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("RequiredStar"));
            }
        }
        public string RequiredStar
        {
            get
            {
                return IsRequired ? "*" : string.Empty;
            }
        }
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
        private bool valueWasUpdated = false;
        public bool IsValid
        {
            get
            {
                if (IsRequired && Text.Trim() == "")
                {
                    ErrorHint = "Поле обязательно для заполнения";
                    return false;
                }
                else if (!IsRequired && Text.Trim() == "")
                {
                    if (Text.Trim() == "")
                    {
                        ErrorHint = "";
                        return true;
                    }
                }

                bool valid;
                switch (Validation)
                {
                    case ValidationEnum.Integer:
                        valid = int.TryParse(Text, out _);
                        ErrorHint = valid ? "" : "Ожидается целое число";
                        break;
                    case ValidationEnum.Real:
                        valid = double.TryParse(Text, NumberStyles.Any, CultureInfo.InvariantCulture, out _);
                        ErrorHint = valid ? "" : "Ожидается число";
                        break;
                    default:
                        valid = true;
                        ErrorHint = "";
                        break;
                }
                return valid;
            }
        }
        public bool IsErrorShown
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

        private ValidationEnum validation;
        public ValidationEnum Validation
        {
            get { return validation; }
            set
            {
                validation = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsValid"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsErrorShown"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
