using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InventoryControl.ViewModel
{
    public class OpenPanelCommand : ICommand
    {
        public string PanelName { get; set; }

        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            PanelManager.OpenPanel(PanelName);
        }
        
        public OpenPanelCommand(string panelName)
        {
            this.PanelName = panelName;
        }
    }
}
