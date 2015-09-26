#region References

using System.Collections.Generic;
using Newtonsoft.Json;

#endregion

namespace Numerous.Api
{
    internal class SubscriptionResultPage : IResultPage<Subscription>
    {
        #region Properties

        [JsonProperty("subscriptions")]
        public IEnumerable<Subscription> Values { get; set; }

        [JsonProperty("nextURL")]
        public string NextUrl { get; set; }

        #endregion
    }
}