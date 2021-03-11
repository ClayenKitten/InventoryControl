namespace InventoryControl.View
{
    /// <summary>
    /// Simple ValueObject for <seealso cref="AdaptiveStackScheme"/>
    /// </summary>
    public class AdaptiveStackSchemeElement
    {
        public bool HasResizer { get; }
        public int LengthRatio { get; }

        /// <summary>
        /// Basic constructor
        /// </summary>
        /// <param name="hasResizer">Does element have resizer at the right or bottom side?</param>
        /// <param name="lengthRatio">Ratio of adaptive side</param>
        /// <param name="minLength">Min length of adaptive side</param>
        /// <param name="maxLength">Max length of adaptive side</param>
        public AdaptiveStackSchemeElement(bool hasResizer, int lengthRatio)
        {
            this.HasResizer = hasResizer;
            this.LengthRatio = lengthRatio;
        }
    }
}
