using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace InventoryControl.View.Controls
{
    public class Form : Grid, INotifyPropertyChanged
    {
        private List<IValidatable> GetValidatableChildren(DependencyObject elem)
        {
            List<IValidatable> res = new List<IValidatable>();
            foreach (var child in LogicalTreeHelper.GetChildren(elem))
            {
                if (child is IValidatable validatable)
                {
                    res.Add(validatable);
                }
                else if (child is DependencyObject childDO)
                {
                    res.AddRange(GetValidatableChildren(childDO));
                }
            }
            return res;
        }
        public bool IsValid => GetValidatableChildren(this).All(x => x.IsValid);

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            foreach (var elem in GetValidatableChildren(this).OfType<INotifyPropertyChanged>())
            {
                elem.PropertyChanged += (_, e) =>
                {
                    if (e.PropertyName == "IsValid")
                    {
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsValid"));
                    }
                };
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
