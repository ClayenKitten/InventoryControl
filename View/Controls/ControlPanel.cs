using System;
using System.ComponentModel;
using System.Windows.Controls;

namespace InventoryControl.View.Controls
{
    public class ControlPanel : UserControl, IDisposable
    {
        public virtual event EventHandler<object> MessageSent;
        public virtual void ReceiveMessage(object sender, object message) { }
        public void SendMessage(object message)
        {
            MessageSent?.Invoke(this, message);
        }

        public virtual void Dispose()
        {
            if (MessageSent != null)
            {
                foreach (Delegate d in MessageSent.GetInvocationList())
                {
                    MessageSent -= (EventHandler<object>)d;
                }
            }

            if (DataContext is IDisposable && DataContext.GetType() != this.GetType())
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
