using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace InventoryControl.View.Controls
{
    public class FormConfirmButton : Button
    {
        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            if (!(CurrentForm is null))
            {
                CurrentForm.PropertyChanged += (sender, e) =>
                {
                    if (e.PropertyName is "IsValid")
                    {
                        SetCurrentValue(IsEnabledProperty, CurrentForm.IsValid);
                    }
                };
            }
            SetCurrentValue(IsEnabledProperty, CurrentForm.IsValid);
        }

        public Form Form { get; set; }
        public static DependencyProperty FormProperty =
            DependencyProperty.Register("Form", typeof(Form), typeof(FormConfirmButton));

        private Form CurrentForm
        {
            get
            {
                if (GetValue(FormProperty) != null)
                {
                    return (Form)GetValue(FormProperty);
                }
                else
                {
                    DependencyObject control = this;
                    while (control != null && control.GetType() != typeof(Form))
                    {
                        control = VisualTreeHelper.GetParent(this);
                    }
                    if (control is Form)
                    {
                        return control as Form;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
        protected override void OnClick()
        {
            base.OnClick();
            CurrentForm?.Confirm();
        }
    }
}
