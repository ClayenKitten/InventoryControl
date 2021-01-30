using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InventoryControl.ViewModel
{
    public class EventCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public delegate void ExecutedHandler(object parameter);
        public event ExecutedHandler Executed;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Executed?.Invoke(parameter);
        }
    }
}
