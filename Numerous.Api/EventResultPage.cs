using System.Collections.Generic;
using Newtonsoft.Json;

namespace Numerous.Api
{
    internal class EventResultPage
    {
        [JsonProperty("events")]
        public List<Event> All { get; set; }

        [JsonProperty("nextURL")]
        public string NextUrl { get; set; }
    }
}