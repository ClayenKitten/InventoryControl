using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace InventoryControl.View
{
    public class AdaptiveStackScheme : IEnumerable<AdaptiveStackSchemeElement>
    {
        public Orientation Orientation { get; }
        public int Count { get { return value.Count; } }
        private List<AdaptiveStackSchemeElement> value { get; }

        public AdaptiveStackScheme(Orientation orientation, List<AdaptiveStackSchemeElement> value)
        {
            this.Orientation = orientation;
            this.value = value;
        }
        public AdaptiveStackScheme(Orientation orientation, params AdaptiveStackSchemeElement[] value)
            : this(orientation, new List<AdaptiveStackSchemeElement>(value)) { }

        public AdaptiveStackSchemeElement this[int i]
        {
            get { return value[i]; }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        public IEnumerator<AdaptiveStackSchemeElement> GetEnumerator()
        {
            return ((IEnumerable<AdaptiveStackSchemeElement>)value).GetEnumerator();
        }
    }
}
