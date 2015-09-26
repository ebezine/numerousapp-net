using System.Collections.Generic;
using Newtonsoft.Json;

namespace Numerous.Api
{
    internal class MetricResultPage : IResultPage<Metric>
    {
        [JsonProperty("metrics")]
        public IEnumerable<Metric> Values { get; set; }

        [JsonProperty("nextURL")]
        public string NextUrl { get; set; }
    }
}