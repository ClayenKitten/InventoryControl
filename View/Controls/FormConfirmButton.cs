using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace InventoryControl.View.Controls
{
    public class FormConfirmButton : Button
    {
        static FormConfirmButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FormConfirmButton), new FrameworkPropertyMetadata(typeof(FormConfirmButton)));
        }
        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            if (!(CurrentForm is null))
            {
                CurrentForm.PropertyChanged += (sender, e) =>
                {
                    if (e.PropertyName is "IsValid")
                    {
                        SetCurrentValue(Button.IsEnabledProperty, CurrentForm.IsValid);
                    }
                };
            }
        }

        private Form CurrentForm
        {
            get
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
        protected override void OnClick()
        {
            base.OnClick();
            CurrentForm?.Confirm();
        }
    }
}
