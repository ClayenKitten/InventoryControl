using InventoryControl.View;
using InventoryControl.View.Controls;
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
        Func<ControlPanelContainer> GetPanelDel { get; }

        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute()
        {
            ((MainWindow)App.Current.MainWindow).Panel = GetPanelDel.Invoke();
        }
        public void Execute(object parameter) => Execute();

        public OpenPanelCommand(Func<ControlPanelContainer> getPanelDel)
        {
            this.GetPanelDel = getPanelDel;
        }
    }
}
