using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace InventoryControl.View
{
    public class DynamicLayoutSchemeElement
    {
        public bool HasResizer { get; }
        public int WidthRatio { get; }
        public int HeightRatio { get; }
        public int MinWidth { get; }
        public int MinHeight { get; }
        public int MaxWidth { get; }
        public int MaxHeight { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hasResizer">Does element have resizer at the right or bottom side?</param>
        /// <param name="defaultSize">Default size of elements</param>
        /// <param name="minSize">Main size of element; Negative means unlimited</param>
        /// <param name="maxSize">Main size of element; Negative means unlimited</param>
        public DynamicLayoutSchemeElement(bool hasResizer, int widthRatio, int heightRatio, 
            int minWidth=0, int minHeight=0, int maxWidth=int.MaxValue, int maxHeight=int.MaxValue)
        {
            this.HasResizer = hasResizer;
            this.WidthRatio = widthRatio;
            this.HeightRatio= heightRatio;
            this.MinWidth   = minWidth;
            this.MinHeight  = minHeight;
            this.MaxWidth   = maxWidth;
            this.MaxHeight  = maxHeight;
        }
    }
}
