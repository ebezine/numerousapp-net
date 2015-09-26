#region References

using System.Collections.Generic;
using Newtonsoft.Json;

#endregion

namespace Numerous.Api
{
    internal class MetricResultPage : IResultPage<Metric>
    {
        #region Properties

        [JsonProperty("metrics")]
        public IEnumerable<Metric> Values { get; set; }

        [JsonProperty("nextURL")]
        public string NextUrl { get; set; }

        #endregion
    }
}