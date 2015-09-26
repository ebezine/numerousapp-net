#region References

using System.Collections.Generic;
using Newtonsoft.Json;

#endregion

namespace Numerous.Api
{
    internal class EventResultPage : IResultPage<Event>
    {
        #region Properties

        [JsonProperty("events")]
        public IEnumerable<Event> Values { get; set; }

        [JsonProperty("nextURL")]
        public string NextUrl { get; set; }

        #endregion
    }
}