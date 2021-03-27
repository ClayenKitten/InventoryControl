using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace InventoryControl.View.Controls
{
    public abstract class ControlPanelContainer : Control, IDisposable
    {
        protected Grid mainGrid { get { return Template.FindName("MainGrid", this) as Grid; } }
        static ControlPanelContainer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(ControlPanelContainer),
                new FrameworkPropertyMetadata(typeof(ControlPanelContainer))
            );
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

        protected abstract IList<ControlPanel> Content { get; }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            //Create columns
            for (int i = 0; i < Content.Count*2-1; i++)
            {
                mainGrid.ColumnDefinitions.Add(
                    new ColumnDefinition()
                    {
                        Width = (i % 2 == 0) ? new GridLength(1, GridUnitType.Star) : new GridLength(5)
                    }
                );
            }
            //Add ControlPanels and resizers
            foreach (var controlPanel in Content)
            {
                //Control panel
                controlPanel.SetValue(Grid.ColumnProperty, Content.IndexOf(controlPanel)*2);
                mainGrid.Children.Add(controlPanel);
                //Splitter
                var splitter = new GridSplitter();
                splitter.SetValue(Grid.ColumnProperty, Content.IndexOf(controlPanel)*2+1);
                mainGrid.Children.Add(splitter);
            }
            //Delete excess resizer
            mainGrid.Children.RemoveAt(mainGrid.Children.Count - 1);
        }

        /// <summary>
        /// Send data to another panel. Or don't if there are none.
        /// </summary>
        protected abstract void Send(ControlPanel sender, object item);

        public void Dispose()
        {
            foreach(var controlPanel in Content)
            {
                controlPanel.Dispose();
            }
        }
    }
}
