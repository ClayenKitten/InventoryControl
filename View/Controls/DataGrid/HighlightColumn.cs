using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;

namespace InventoryControl.View.Controls
{
    public class HighlightColumn : DataGridTextColumn
    {
        static HighlightColumn()
        {
            ElementStyleProperty.OverrideMetadata(typeof(HighlightColumn), new FrameworkPropertyMetadata(DefaultElementStyle));
            EditingElementStyleProperty.OverrideMetadata(typeof(HighlightColumn), new FrameworkPropertyMetadata(DefaultEditingElementStyle));
        }

        public string HighlightPhrase { get; set; }
        public static DependencyProperty HighlightPhraseProperty =
            DependencyProperty.Register("HighlightPhrase", typeof(string), typeof(HighlightColumn));

        protected override FrameworkElement GenerateElement(DataGridCell cell, object dataItem)
        {
            return new HighlightTextBlock(
                (TextBlock)base.GenerateElement(cell, dataItem),
                BindingOperations.GetBindingBase(this, HighlightPhraseProperty));
        }
    }
}