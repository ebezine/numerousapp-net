#region References

using Newtonsoft.Json;
using Numerous.Api.Helpers;

#endregion

namespace Numerous.Api
{
    /// <summary>
    /// Represents the visibility of a metric.
    /// </summary>
    [JsonConverter(typeof(LowerCaseStringEnumConverter))]
    public enum Visibility
    {
        /// <summary>
        /// Metric is public and listed.
        /// </summary>
        Public,

        /// <summary>
        /// Metric is public but unlisted.
        /// </summary>
        Unlisted,

        /// <summary>
        /// Metric is private.
        /// </summary>
        Private
    }
}