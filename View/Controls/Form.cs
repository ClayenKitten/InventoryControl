using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
                    if (elem is AdvancedTextbox)
                    {
                        areAllValid &= (elem as AdvancedTextbox).IsValid;
                    }
                }
                return areAllValid;
            }
        }

        static Form()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Form), new FrameworkPropertyMetadata(typeof(Form)));
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            foreach (var elem in Children)
            {
                if (elem is INotifyPropertyChanged)
                {
                    (elem as INotifyPropertyChanged).PropertyChanged += (_, e) =>
                    {
                        if (elem is AdvancedTextbox && e.PropertyName == "Text")
                        {
                            OnChildControlUpdated();
                        }
                    };
                }
            }
            OnChildControlUpdated();
        }

        public void OnChildControlUpdated()
        {
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

        public event EventHandler Confirmed;
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
