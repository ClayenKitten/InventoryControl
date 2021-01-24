using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace InventoryControl.View
{
    public class DividedPanel : System.Windows.Controls.Panel
    {
        protected override Size MeasureOverride(Size availableSize)
        {
            double width = 0;
            double height = 0;
            foreach(UIElement child in InternalChildren)
            {
                child.Measure(new Size(availableSize.Width/InternalChildren.Count, availableSize.Height));
                var childSize = child.DesiredSize;
                width += childSize.Width;
                height = Math.Max(height, childSize.Height);
            }
            return new Size(width, height);
        }
        protected override Size ArrangeOverride(Size finalSize)
        {
            double offset = 0.0d;
            foreach (UIElement child in InternalChildren)
            {
                var width = finalSize.Width / InternalChildren.Count;
                child.Arrange(new Rect(offset, 0, width, finalSize.Height));
                offset += width;
            }
            return finalSize;
        }

        public void SetControl(UIElement elem)
        {
            InternalChildren.Clear();
            InternalChildren.Add(elem);
        }

        public void SetControl(UIElement elem, bool isSecond)
        {
            //Check if empty
            if(InternalChildren.Count==0)
            {
                SetControl(elem);
                return;
            }

            //Check if mult
            while (InternalChildren.Count > 2)
            {
                InternalChildren.RemoveAt(2);
            }
            //Set value
            if(!isSecond)
            {
                InternalChildren[0] = elem;
            }
            else
            {
                if (InternalChildren.Count == 2)
                    InternalChildren[1] = elem;
                else
                    InternalChildren.Add(elem);
            }
        }
        public void SetControl(UIElement elem1, UIElement elem2)
        {
            InternalChildren.Clear();
            InternalChildren.Add(elem1);
            InternalChildren.Add(elem2);
        }
    }
}
