using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace InventoryControl.UserControls.OrderControl
{
    public delegate void ConfirmTurnoverHandler(bool IsBuying, string endpoint);
    public interface IBottomControl
    {
        void SetButtonsEnabled(bool isEnabled);
        UIElement GetUIElement();

        event ConfirmTurnoverHandler ConfirmTurnover;
    }
}
