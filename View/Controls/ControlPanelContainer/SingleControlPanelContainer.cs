using System;
using System.Collections.Generic;

namespace InventoryControl.View.Controls
{
    public class SingleControlPanelContainer : ControlPanelContainer
    {
        private ControlPanel controlPanel;

        public override IReadOnlyList<ControlPanel> ControlPanels
            => new List<ControlPanel>() { controlPanel };

        public override void OnApplyTemplate()
        {
            mainGrid.Children.Add(controlPanel);
        }


        public SingleControlPanelContainer(ControlPanel controlPanel)
        {
            this.controlPanel = controlPanel;
        }
        public override void Dispose() => controlPanel.Dispose();
    }
}
