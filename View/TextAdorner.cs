using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace InventoryControl.View
{
    public class TextAdorner : Adorner
    {
        string text = "";
        public string Text
        {
            get => text;
            set
            {
                text = value;
                InvalidateVisual();
            }
        }
        public TextAdorner(UIElement elem) : base(elem)
        {
            SetValue(IsHitTestVisibleProperty, false);
        }
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            var text = new FormattedText
            (
                Text,
                CultureInfo.InvariantCulture, FlowDirection.LeftToRight,
                new Typeface(new FontFamily("Arial"), FontStyles.Normal, FontWeights.Bold, FontStretches.Normal),
                15, new SolidColorBrush(Colors.Black), VisualTreeHelper.GetDpi(this).PixelsPerDip
            );
            var x = AdornedElement.RenderSize.Width / 2 - text.Width / 2;
            var y = AdornedElement.RenderSize.Height / 2 - text.Height / 2;
            drawingContext.PushClip(new RectangleGeometry(new Rect(AdornedElement.RenderSize)));
            drawingContext.DrawText(text, new Point(x, y));
        }
    }
}
