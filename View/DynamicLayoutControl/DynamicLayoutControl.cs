using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace InventoryControl.View
{
    public class DynamicLayoutControl : Control
    {
        public DynamicLayoutScheme Scheme { get; set; }
        public List<UIElement> Content { get; } = new List<UIElement>();

        public DynamicLayoutControl(DynamicLayoutScheme scheme, params UIElement[] content)
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DynamicLayoutControl), new FrameworkPropertyMetadata(typeof(DynamicLayoutControl)));
            this.Scheme = scheme;
            this.Content = new List<UIElement>(content);
        }

        protected override Size MeasureOverride(Size constraint)
        {
            double width = 0;
            double height = 0;
            foreach(var elem in Content)
            {
                elem.Measure(constraint);
                width += elem.DesiredSize.Width;
                height += elem.DesiredSize.Height;
            }
            return new Size(width, height);
        }
        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            Grid MainGrid = Template.FindName("MainGrid", this) as Grid;
            MainGrid.Arrange(new Rect(arrangeBounds));
            return arrangeBounds;
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            Grid MainGrid = Template.FindName("MainGrid", this) as Grid;
            foreach (var schemeElem in Scheme)
            {
                MainGrid.ColumnDefinitions.Add(
                    new ColumnDefinition() 
                    {
                        Width = new GridLength(schemeElem.WidthRatio, GridUnitType.Star),
                        MinWidth = schemeElem.MinWidth,
                        MaxWidth = schemeElem.MaxWidth
                    }
                );
            }
            foreach (var elem in Content)
            {
                if (!MainGrid.Children.Contains(elem))
                    MainGrid.Children.Add(elem);
            }
        }
    }
}
