using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace InventoryControl.View.Controls
{
    public class DataGridAdorner : Adorner
    {
        string text = "";
        public void SetText(string text)
        {
            this.text = text;
            InvalidateVisual();
        }

        public DataGridAdorner(DataGrid dataGrid, string text)
            : base(dataGrid)
        {
            SetValue(IsHitTestVisibleProperty, false);
            SetText(text);
        }
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            var text = new FormattedText
            (
                this.text,
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
