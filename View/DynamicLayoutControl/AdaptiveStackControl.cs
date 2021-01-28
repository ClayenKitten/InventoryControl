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

        const double RESIZERLENGTH = 5.0;

        private Grid mainGrid { get { return Template.FindName("MainGrid", this) as Grid; } }

        public AdaptiveStackControl(AdaptiveStackScheme scheme, params UIElement[] content)
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AdaptiveStackControl), new FrameworkPropertyMetadata(typeof(AdaptiveStackControl)));
            this.Scheme = scheme;
            this.Content = new List<UIElement>(content);
        }
        protected override Size MeasureOverride(Size constraint)
        {
            mainGrid.Measure(constraint);
            return mainGrid.DesiredSize;
        }
        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            mainGrid.Arrange(new Rect(arrangeBounds));
            return arrangeBounds;
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            //Generate grid structure
            if (Scheme.Orientation == Orientation.Horizontal)
            {
                for (int i = 0; i < Scheme.Count; i++)
                {
                    mainGrid.ColumnDefinitions.Add(
                        new ColumnDefinition()
                        {
                            Width = new GridLength(Scheme[i].LengthRatio, GridUnitType.Star)
                        }
                    );
                    if(Scheme[i].HasResizer && i != Scheme.Count-1)
                        mainGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(RESIZERLENGTH) });
                }
            }
            else if (Scheme.Orientation == Orientation.Vertical)
            {
                for (int i = 0; i < Scheme.Count; i++)
                {
                    mainGrid.RowDefinitions.Add(
                        new RowDefinition()
                        {
                            Height = new GridLength(Scheme[i].LengthRatio, GridUnitType.Star),
                        }
                    );
                    if (Scheme[i].HasResizer && i != Scheme.Count - 1)
                        mainGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(RESIZERLENGTH) });
                }
            }
            //Add content
            var property = Scheme.Orientation == Orientation.Horizontal ?
                            Grid.ColumnProperty : Grid.RowProperty;
            int resizableElementsCount = 0;
            int contentIndex = 0;
            foreach(var schemeElem in Scheme)
            {
                //Check if element in content exists
                if (contentIndex < Content.Count)
                {
                    Content[contentIndex].SetValue(property, contentIndex + resizableElementsCount);
                    mainGrid.Children.Add(Content[contentIndex]);
                }
                //Add resizers
                if (schemeElem.HasResizer && contentIndex != Scheme.Count-1)
                {
                    resizableElementsCount++;
                    var splitter = new GridSplitter();
                    splitter.SetValue(property, contentIndex + resizableElementsCount);
                    mainGrid.Children.Add(splitter);
                }
                contentIndex++;
            }
        }
    }
}
