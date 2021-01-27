using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace InventoryControl.View
{
    public class DynamicLayoutScheme : IEnumerable<DynamicLayoutSchemeElement>
    {
        public Orientation Orientation { get; }
        private List<DynamicLayoutSchemeElement> value { get; }

        public DynamicLayoutScheme(Orientation orientation, List<DynamicLayoutSchemeElement> value)
        {
            this.Orientation = orientation;
            this.value = value;
        }
        public DynamicLayoutScheme(Orientation orientation, params DynamicLayoutSchemeElement[] value)
            : this(orientation, new List<DynamicLayoutSchemeElement>(value)) { }

        public List<Size> GetSizes(List<UIElement> content, Size bounds)
        {
            List<Size> res = new List<Size>();
            for (int i=0; i < content.Count; i++)
            {
                res.Add(
                    Orientation == Orientation.Horizontal ?
                    new Size(value[i].WidthRatio*GetStarSize(bounds.Width), bounds.Height) :
                    new Size(bounds.Width, value[i].HeightRatio*GetStarSize(bounds.Height))
                );
            }
            return res;
        }
        private double GetStarSize(double arrangeBound)
        {
            int starsNumber = 0;
            foreach (var elem in value)
            {
                starsNumber += this.Orientation == Orientation.Horizontal ? elem.WidthRatio : elem.HeightRatio;
            }
            return arrangeBound / starsNumber;
        }

        public DynamicLayoutSchemeElement this[int i]
        {
            get { return value[i]; }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        public IEnumerator<DynamicLayoutSchemeElement> GetEnumerator()
        {
            return ((IEnumerable<DynamicLayoutSchemeElement>)value).GetEnumerator();
        }
    }
}
