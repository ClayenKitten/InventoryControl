using InventoryControl.Util;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;

namespace InventoryControl.View.Controls
{
    public class HighlightTextBlock : TextBlock
    {
        public new string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public new static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string),
            typeof(HighlightTextBlock), new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.AffectsRender,
                new PropertyChangedCallback(UpdateHighlighting)));

        public string HighlightPhrase
        {
            get { return (string)GetValue(HighlightPhraseProperty); }
            set { SetValue(HighlightPhraseProperty, value); }
        }

        public static readonly DependencyProperty HighlightPhraseProperty =
            DependencyProperty.Register("HighlightPhrase", typeof(string),
            typeof(HighlightTextBlock), new FrameworkPropertyMetadata(String.Empty, FrameworkPropertyMetadataOptions.AffectsRender,
                new PropertyChangedCallback(UpdateHighlighting)));

        public Brush HighlightBrush
        {
            get { return (Brush)GetValue(HighlightBrushProperty); }
            set { SetValue(HighlightBrushProperty, value); }
        }

        public static readonly DependencyProperty HighlightBrushProperty =
            DependencyProperty.Register("HighlightBrush", typeof(Brush),
            typeof(HighlightTextBlock), new FrameworkPropertyMetadata(Brushes.Yellow, FrameworkPropertyMetadataOptions.AffectsRender,
                new PropertyChangedCallback(UpdateHighlighting)));

        public bool IsCaseSensitive
        {
            get { return (bool)GetValue(IsCaseSensitiveProperty); }
            set { SetValue(IsCaseSensitiveProperty, value); }
        }

        public static readonly DependencyProperty IsCaseSensitiveProperty =
            DependencyProperty.Register("IsCaseSensitive", typeof(bool),
            typeof(HighlightTextBlock), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsRender,
                new PropertyChangedCallback(UpdateHighlighting)));

        private static void UpdateHighlighting(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ApplyHighlight(d as HighlightTextBlock);
        }

        private static void ApplyHighlight(HighlightTextBlock tb)
        {
            string highlightPhrase = tb.HighlightPhrase.Normalized();
            string text = tb.Text;

            if (String.IsNullOrEmpty(highlightPhrase))
            {
                tb.Inlines.Clear();

                tb.Inlines.Add(text);
            }

            else
            {
                int index = text.StartIndexOf(highlightPhrase);

                tb.Inlines.Clear();
                //If no highlight
                if (index < 0)
                    tb.Inlines.Add(text);
                //If yes highlight
                else
                {
                    //Add non-highlight at start if needed
                    if (index > 0)
                        tb.Inlines.Add(text.Substring(0, index));
                    //Add highlight at start if needed
                    tb.Inlines.Add(new Run(text.Substring(index, highlightPhrase.Length))
                    {
                        Background = tb.HighlightBrush
                    });
                    //Add to the end
                    index += highlightPhrase.Length;
                    if (index < text.Length)
                        tb.Inlines.Add(text.Substring(index));
                }
            }
        }

        public HighlightTextBlock() : base() { }
        public HighlightTextBlock(TextBlock basement, BindingBase HighlightPhraseBindingBase) : this()
        {
            SetBinding(TextProperty, basement.GetBindingExpression(TextBlock.TextProperty).ParentBindingBase);
            SetBinding(HighlightPhraseProperty, HighlightPhraseBindingBase);
        }
    }
}
