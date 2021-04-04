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

        public abstract override void OnApplyTemplate();
        public abstract IList<ControlPanel> ControlPanels { get; }

        public virtual void Dispose()
        {
            foreach(var controlPanel in ControlPanels)
            {
                controlPanel.Dispose();
            }
        }
    }
}
