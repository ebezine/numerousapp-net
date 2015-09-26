using System;
using Newtonsoft.Json;

namespace Numerous.Api
{
    public class Event
    {
        [JsonProperty("id")]
        public long Id { get; internal set; }

        [JsonProperty("metricId")]
        public long MetricId { get; internal set; }

        [JsonProperty("authorId")]
        public long AuthorId { get; internal set; }

        [JsonProperty("updated")]
        public DateTime Updated { get; internal set; }

        [JsonProperty("value")]
        public decimal Value { get; internal set; }

        [JsonProperty("onlyIfChanged")]
        public bool OnlyIfChanged { get; internal set; }

        [JsonIgnore]
        public EventAction Action
        {
            get { return ActionName == "ADD" ? EventAction.Add : EventAction.Set; }
        }

        [JsonProperty("action")]
        internal string ActionName { get; set; }

        public class Edit
        {
            public DateTime? Updated { get; set; }
            public decimal Value { get; set; }
            public bool OnlyIfChanged { get; set; }

            [JsonIgnore]
            public EventAction Action
            {
                get { return ActionName == "ADD" ? EventAction.Add : EventAction.Set; }

                set { ActionName = value == EventAction.Add ? "ADD" : null; }
            }

            [JsonProperty("action")]
            internal string ActionName { get; set; }
        }
    }
}