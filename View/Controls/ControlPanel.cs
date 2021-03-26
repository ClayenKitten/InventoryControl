using System;
using System.ComponentModel;
using System.Windows.Controls;

namespace InventoryControl.View.Controls
{
    public class ControlPanel : UserControl, IDisposable
    {
        public virtual void Dispose() 
        {
            if(DataContext is IDisposable && DataContext.GetType() != this.GetType())
            {
                ((IDisposable)this.DataContext).Dispose();
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"Datacontext of {this.GetType().Name} is not IDisposable!");
            }
        }
    }
}
