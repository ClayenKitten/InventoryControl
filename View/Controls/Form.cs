using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace InventoryControl.View.Controls
{
    public class Form : Grid
    {
        static Form()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Form), new FrameworkPropertyMetadata(typeof(Form)));
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
    }
}
