using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace InventoryControl.View.Controls
{
    /// <summary>
    /// Adapter of <see cref="Grid"/> with adaptive and optionally resizable StackPanel-like behaviour
    /// </summary>
    public class AdaptiveStackControl : Control, IDisposable
    {
        public AdaptiveStackScheme Scheme { get; set; }
        public List<ControlPanel> Content { get; } = new List<ControlPanel>();

        const double RESIZERLENGTH = 5.0;

        private Grid mainGrid { get { return Template.FindName("MainGrid", this) as Grid; } }

        //Constructors
        public AdaptiveStackControl(AdaptiveStackScheme scheme, params ControlPanel[] content)
            : this(scheme, new List<ControlPanel>(content)) { }
        public AdaptiveStackControl(AdaptiveStackScheme scheme, List<ControlPanel> content)
        {
            this.Scheme = scheme;
            this.Content = new List<ControlPanel>(content);
        }
        static AdaptiveStackControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AdaptiveStackControl), new FrameworkPropertyMetadata(typeof(AdaptiveStackControl)));
        }
        //Layout
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
        //Disposing
        public void Dispose()
        {
            foreach (var elem in Content)
            {
                elem.Dispose();
            }
        }
    }
}
