using System;
using System.ComponentModel;
using System.Windows.Controls;

namespace InventoryControl.View.Controls
{
    public delegate void ControlPanelMessageHandler(ControlPanel sender, Type targetType, object message);
    public class ControlPanel : UserControl, IDisposable
    {
        public virtual event ControlPanelMessageHandler MessageSent;
        public virtual void ReceiveMessage(object sender, object message) { }
        public void SendMessage(Type targetType, object message)
        {
            MessageSent?.Invoke(this, targetType, message);
        }

        public virtual void Dispose()
        {
            if (MessageSent != null)
            {
                foreach (Delegate d in MessageSent.GetInvocationList())
                {
                    MessageSent -= (ControlPanelMessageHandler)d;
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
