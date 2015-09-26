using System.Collections.Generic;
using Newtonsoft.Json;

namespace Numerous.Api
{
    internal class SubscriptionResultPage
    {
        [JsonProperty("subscriptions")]
        public List<Subscription> All { get; set; }

        [JsonProperty("nextURL")]
        public string NextUrl { get; set; }
    }
}