using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace InventoryControl.View
    {
    /// <summary>
    /// Simple ValueObject for <seealso cref="AdaptiveStackScheme"/>
    /// </summary>
    public class AdaptiveStackSchemeElement
    {
        public bool HasResizer { get; }
        public int LengthRatio { get; }
        public int MinLength { get; }
        public int MaxLength { get; }

        /// <summary>
        /// Basic constructor
        /// </summary>
        /// <param name="hasResizer">Does element have resizer at the right or bottom side?</param>
        /// <param name="lengthRatio">Ratio of adaptive side</param>
        /// <param name="minLength">Min length of adaptive side</param>
        /// <param name="maxLength">Max length of adaptive side</param>
        public AdaptiveStackSchemeElement(bool hasResizer, int lengthRatio, int minLength=0, int maxLength=int.MaxValue)
        {
            this.HasResizer = hasResizer;
            this.LengthRatio = lengthRatio;
            this.MinLength = minLength;
            this.MaxLength = maxLength;
        }
    }
}
