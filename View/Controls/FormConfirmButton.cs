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
        protected override void OnClick()
        {
            base.OnClick();
            DependencyObject control = this;
            while (control != null && control.GetType() != typeof(Form))
            {
                control = VisualTreeHelper.GetParent(this);
            }
            if (control.GetType() == typeof(Form))
            {
                (control as Form).Confirm();
            }
        }
    }
}
