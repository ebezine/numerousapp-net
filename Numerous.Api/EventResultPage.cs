using System.Collections.Generic;
using Newtonsoft.Json;

namespace Numerous.Api
{
    internal class EventResultPage : IResultPage<Event>
    {
        [JsonProperty("events")]
        public IEnumerable<Event> Values { get; set; }

        [JsonProperty("nextURL")]
        public string NextUrl { get; set; }
    }
}