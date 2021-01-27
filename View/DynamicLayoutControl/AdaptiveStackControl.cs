using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace InventoryControl.View
{
    /// <summary>
    /// Adapter of <see cref="Grid"/> with adaptive and optionally resizable StackPanel-like behaviour
    /// </summary>
    public class AdaptiveStackControl : Control
    {
        public AdaptiveStackScheme Scheme { get; set; }
        public List<UIElement> Content { get; } = new List<UIElement>();

        public AdaptiveStackControl(AdaptiveStackScheme scheme, params UIElement[] content)
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AdaptiveStackControl), new FrameworkPropertyMetadata(typeof(AdaptiveStackControl)));
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
            //Generate grid structure
            if (Scheme.Orientation == Orientation.Horizontal)
            {
                foreach (var schemeElem in Scheme)
                {
                    MainGrid.ColumnDefinitions.Add(
                        new ColumnDefinition()
                        {
                            Width = new GridLength(schemeElem.LengthRatio, GridUnitType.Star),
                            MinWidth = schemeElem.MinLength,
                            MaxWidth = schemeElem.MaxLength
                        }
                    );
                }
            }
            else if (Scheme.Orientation == Orientation.Vertical)
            {
                foreach (var schemeElem in Scheme)
                {
                    MainGrid.RowDefinitions.Add(
                        new RowDefinition()
                        {
                            Height = new GridLength(schemeElem.LengthRatio, GridUnitType.Star),
                            MinHeight = schemeElem.MinLength,
                            MaxHeight = schemeElem.MaxLength
                        }
                    );
                }
            }
            //Add content
            for (int i=0; i < Content.Count; i++)
            {
                if (Scheme.Orientation == Orientation.Horizontal)
                    Content[i].SetValue(Grid.ColumnProperty, i);
                else if (Scheme.Orientation == Orientation.Vertical)
                    Content[i].SetValue(Grid.RowProperty, i);
                MainGrid.Children.Add(Content[i]);
                
            }
        }
    }
}
