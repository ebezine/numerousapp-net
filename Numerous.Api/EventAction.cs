namespace Numerous.Api
{
    /// <summary>
    /// Represent an <see cref="Event"/> action.
    /// </summary>
    public enum EventAction
    {
        /// <summary>
        /// The value is set.
        /// </summary>
        Set,

        /// <summary>
        /// The specified value is added to the current metric value
        /// </summary>
        Add
    }
}