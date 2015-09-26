#region References

using Newtonsoft.Json;
using Numerous.Api.Helpers;

#endregion

namespace Numerous.Api
{
    /// <summary>
    /// Represents the various available kind of metrics
    /// </summary>
    [JsonConverter(typeof(LowerCaseStringEnumConverter))]
    public enum MetricKind
    {
        /// <summary>
        /// Metric value is a generic decimal number.
        /// </summary>
        Number,

        /// <summary>
        /// Metric value is a percentage.
        /// </summary>
        Percent,

        /// <summary>
        /// Metric is a timer.
        /// </summary>
        /// <remarks>Time value is a decimal representing Unix time.</remarks>
        Timer,

        /// <summary>
        /// Metric is a currency amount.
        /// </summary>
        Currency,

        /// <summary>
        /// Metric is a temperature expressed in Fahrenheit.
        /// </summary>
        Temperature
    }
}