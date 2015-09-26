using System.Collections.Generic;
using Newtonsoft.Json;

namespace Numerous.Api
{
    internal class MetricResultPage
    {
        [JsonProperty("metrics")]
        public List<Metric> All { get; set; }

        [JsonProperty("nextURL")]
        public string NextUrl { get; set; }
    }
}