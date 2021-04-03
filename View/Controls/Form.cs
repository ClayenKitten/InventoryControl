using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace InventoryControl.View.Controls
{
    public class Form : Grid, INotifyPropertyChanged
    {
        public bool IsValid
        {
            get
            {
                bool areAllValid = true;
                foreach (var elem in Children)
                {
                    if (elem is IValidatable)
                    {
                        areAllValid &= (elem as IValidatable).IsValid;
                    }
                }
                return areAllValid;
            }
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            foreach (var elem in Children)
            {
                if (elem is INotifyPropertyChanged && elem is IValidatable)
                {
                    (elem as INotifyPropertyChanged).PropertyChanged += (_, e) =>
                    {
                        if (e.PropertyName == "IsValid")
                        {
                            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsValid"));
                        }
                    };
                }
            }
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsValid"));
        }

        public void ClearAllTextBoxes()
        {
            foreach (var elem in Children)
            {
                if(elem is TextBox)
                {
                    (elem as TextBox).SetCurrentValue(TextBox.TextProperty, "");
                }
                else if(elem is AdvancedTextbox)
                {
                    (elem as AdvancedTextbox).Text = "";
                }
            }
        }
        public void Confirm()
        {
            Confirmed?.Invoke(this, EventArgs.Empty);
        }

        static Form()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Form), new FrameworkPropertyMetadata(typeof(Form)));
        }

        public event EventHandler Confirmed;
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
