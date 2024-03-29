using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace InventoryControl.View.Controls
{
    public class DualControlPanelContainer : ControlPanelContainer
    {
        ControlPanel firstControlPanel;
        ControlPanel secondControlPanel;
        float relWidth = 1f;

        public override IList<ControlPanel> ControlPanels
           => new List<ControlPanel>() { firstControlPanel, secondControlPanel };

        public override void OnApplyTemplate()
        {
            //Specify columns
            mainGrid.ColumnDefinitions.Add(
                new ColumnDefinition() 
                {
                    Width = new GridLength(1f, GridUnitType.Star)
                }
            );
            mainGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(5) });
            mainGrid.ColumnDefinitions.Add(
                new ColumnDefinition() 
                { 
                    Width = new GridLength(relWidth, GridUnitType.Star)
                }
            );
            //Add first control
            firstControlPanel.SetValue(Grid.ColumnProperty, 0);
            mainGrid.Children.Add(firstControlPanel);
            //Add splitter
            var splitter = new GridSplitter();
            splitter.SetValue(Grid.ColumnProperty, 1);
            mainGrid.Children.Add(splitter);
            //Add second control
            secondControlPanel.SetValue(Grid.ColumnProperty, 2);
            mainGrid.Children.Add(secondControlPanel);
        }

        public DualControlPanelContainer(ControlPanel firstControlPanel, ControlPanel secondControlPanel)
        {
            firstControlPanel.MessageSent += (s, t, e) =>
            {
                if (secondControlPanel.GetType() == t)
                {
                    secondControlPanel.ReceiveMessage(s, e);
                }
            };
            secondControlPanel.MessageSent += (s, t, e) =>
            {
                if (firstControlPanel.GetType() == t)
                {
                    firstControlPanel.ReceiveMessage(s, e);
                }
            };

            this.firstControlPanel = firstControlPanel;
            this.secondControlPanel = secondControlPanel;
        }
        /// <param name="relWidth">Width of the second column relative to the first </param>
        public DualControlPanelContainer(ControlPanel firstControlPanel, ControlPanel secondControlPanel, float relWidth)
            : this(firstControlPanel, secondControlPanel)
        {
            this.relWidth = relWidth;
        }

    }
}
