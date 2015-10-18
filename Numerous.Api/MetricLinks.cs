using Newtonsoft.Json;

namespace Numerous.Api
{
    /// <summary>
    /// Represents links available on metric.
    /// </summary>
    public class MetricLinks
    {
        /// <summary>
        /// Gets the API url of the metric.
        /// </summary>
        /// <value>
        /// The API url of the metric.
        /// </value>
        [JsonProperty("self")]
        public string Self { get; internal set; }

        /// <summary>
        /// Gets or sets url of the web page of the metric.
        /// </summary>
        /// <value>
        /// The url of the web page of the metric.
        /// </value>
        [JsonProperty("web")]
        public string Web { get; internal set; }

        /// <summary>
        /// Gets or sets url of the embeddable web page of the metric.
        /// </summary>
        /// <value>
        /// The url of the embeddable web page of the metric.
        /// </value>
        [JsonProperty("embed")]
        public string Embeddable { get; internal set; }
    }
}