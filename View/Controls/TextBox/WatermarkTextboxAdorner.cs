using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace InventoryControl.View.Controls
{
    public class WatermarkTextboxAdorner : Adorner
    {
        public TextBox Target { get; }

        private bool isError = false;
        private string watermark = "";
        private bool isStarShown = false;

        public WatermarkTextboxAdorner(UIElement adornedElement) : base(adornedElement)
        {
            Target = (TextBox)adornedElement;
            IsHitTestVisible = false;
            // Change color of watermark
            Target.LostFocus += (_, _1) => InvalidateVisual();
            Target.GotFocus += (_, _1) => InvalidateVisual();
            Target.TextChanged += (_, _1) => InvalidateVisual();
        }

        private Color WatermarkColor
        {
            get
            {
                if (isError)
                {
                    return Colors.Red;
                }
                if (Target.GetValue(TextBox.TextProperty).ToString() != "")
                {
                    return Colors.Transparent;
                }
                if ((bool)Target.GetValue(TextBox.IsFocusedProperty))
                {
                    return Colors.LightGray;
                }
                else
                {
                    return Colors.Gray;
                }
            }
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            var paddingLeft = Target.Padding.Left;
            var w = Target.RenderSize.Width;
            var h = Target.RenderSize.Height;

            var watermark = new FormattedText
            (
                this.watermark,
                CultureInfo.InvariantCulture,
                FlowDirection.LeftToRight,
                new Typeface(Target.FontFamily, Target.FontStyle, Target.FontWeight, Target.FontStretch),
                Target.FontSize,
                new SolidColorBrush(WatermarkColor),
                VisualTreeHelper.GetDpi(Target).PixelsPerDip
            );
            drawingContext.DrawText(watermark, new Point(paddingLeft + 5, (h - watermark.Height) / 2));

            if (isStarShown)
            {
                var star = new FormattedText
                (
                    "*",
                    CultureInfo.InvariantCulture,
                    FlowDirection.LeftToRight,
                    new Typeface
                    (
                        new FontFamily("Open Sans"),
                        FontStyles.Normal,
                        FontWeights.ExtraBold,
                        FontStretches.Normal
                    ),
                    15,
                    new SolidColorBrush(Colors.Red),
                    VisualTreeHelper.GetDpi(Target).PixelsPerDip
                );
                drawingContext.DrawText(star, new Point(w - star.Width - 10, (h / 2) - (star.Height / 2) + 4));
            }
        }

        public void SetWatermark(string watermark)
        {
            this.watermark = watermark;
            InvalidateVisual();
        }
        public void SetError(bool isError)
        {
            this.isError = isError;
            InvalidateVisual();
        }
        public void SetStarShown(bool isShown)
        {
            isStarShown = isShown;
            InvalidateVisual();
        }
    }
}
