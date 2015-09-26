namespace Numerous.Api
{
    /// <summary>
    /// Represents a number of retries for <see cref="NumerousClient"/> operations.
    /// </summary>
    public struct Retry
    {
        #region Fields

        /// <summary>
        /// The count.
        /// </summary>
        public int Count;

        /// <summary>
        /// Retry indefinitely.
        /// </summary>
        public static readonly Retry Infinite = new Retry {Count = int.MaxValue};

        /// <summary>
        /// No retry.
        /// </summary>
        public static readonly Retry Never = new Retry {Count = 0};

        #endregion

        #region Operators

        /// <summary>
        /// Performs an implicit conversion from <see cref="Retry"/> to <see cref="System.Int32"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator int(Retry value)
        {
            return value.Count;
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="System.Int32"/> to <see cref="Retry"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator Retry(int value)
        {
            return new Retry {Count = value};
        }

        #endregion
    }
}