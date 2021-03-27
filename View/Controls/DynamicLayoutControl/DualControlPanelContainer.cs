using System;
using System.Collections.Generic;

namespace InventoryControl.View.Controls
{
    public class DualControlPanelContainer : ControlPanelContainer
    {
        ControlPanel firstControlPanel;
        ControlPanel secondControlPanel;

        protected override IList<ControlPanel> Content
            => new List<ControlPanel>() { firstControlPanel, secondControlPanel };
        protected override void Send(ControlPanel sender, object item)
        {
            throw new NotImplementedException();
        }

        public DualControlPanelContainer(ControlPanel firstControlPanel, ControlPanel secondControlPanel)
        {
            this.firstControlPanel = firstControlPanel;
            this.secondControlPanel = secondControlPanel;
        }

    }
}
