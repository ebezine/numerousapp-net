using System.Collections.Generic;
using Newtonsoft.Json;

namespace Numerous.Api
{
    internal class SubscriptionResultPage : IResultPage<Subscription>
    {
        [JsonProperty("subscriptions")]
        public IEnumerable<Subscription> Values { get; set; }

        [JsonProperty("nextURL")]
        public string NextUrl { get; set; }
    }
}