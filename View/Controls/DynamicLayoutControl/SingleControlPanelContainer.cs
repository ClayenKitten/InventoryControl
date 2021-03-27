using System;
using System.Collections.Generic;

namespace InventoryControl.View.Controls
{
    public class SingleControlPanelContainer : ControlPanelContainer
    {
        private ControlPanel controlPanel;

        protected override IList<ControlPanel> Content
            => new List<ControlPanel>() { controlPanel };
        protected override void Send(ControlPanel sender, object item)
        {
            throw new NotImplementedException();
        }

        public SingleControlPanelContainer(ControlPanel controlPanel)
        {
            this.controlPanel = controlPanel;
        }
    }
}
