using Newtonsoft.Json;

namespace Numerous.Api
{
    public class Subscription
    {
        [JsonProperty("userId")]
        public long UserId { get; internal set; }

        [JsonProperty("metricId")]
        public long MetricId { get; internal set; }

        [JsonProperty("notificationsEnabled")]
        public bool NotificationsEnabled { get; internal set; }

        [JsonProperty("notifyWhenAbove")]
        public decimal NotifyWhenAbove { get; internal set; }

        [JsonProperty("notifyWhenAboveSet")]
        public bool NotifyWhenAboveSet { get; internal set; }

        [JsonProperty("notifyWhenBelow")]
        public decimal NotifyWhenBelow { get; internal set; }

        [JsonProperty("notifyWhenBelowSet")]
        public bool NotifyWhenBelowSet { get; internal set; }

        [JsonProperty("notifyOnPercentChange")]
        public decimal NotifyOnPercentChange { get; internal set; }

        [JsonProperty("notifyOnPercentChangeSet")]
        public bool NotifyOnPercentChangeSet { get; internal set; }

        [JsonProperty("notifyOnComment")]
        public bool NotifyOnComment { get; internal set; }

        [JsonProperty("notifyOnLike")]
        public bool NotifyOnLike { get; internal set; }

        [JsonProperty("notifyOnFollow")]
        public bool NotifyOnFollow { get; internal set; }

        [JsonProperty("notifyOnError")]
        public bool NotifyOnError { get; internal set; }

        public class Edit
        {
            //public long? UserId { get; set; }
            //public long? MetricId { get; set; }

            public bool? NotificationsEnabled { get; set; }
            public decimal? NotifyWhenAbove { get; set; }
            public bool? NotifyWhenAboveSet { get; set; }
            public decimal? NotifyWhenBelow { get; set; }
            public bool? NotifyWhenBelowSet { get; set; }
            public decimal? NotifyOnPercentChange { get; set; }
            public bool? NotifyOnPercentChangeSet { get; set; }
            public bool? NotifyOnComment { get; set; }
            public bool? NotifyOnLike { get; set; }
            public bool? NotifyOnFollow { get; set; }
            public bool? NotifyOnError { get; set; }

            internal Edit DefaultTo(Subscription subscription)
            {
                return new Edit
                {
                    NotificationsEnabled = NotificationsEnabled ?? subscription.NotificationsEnabled,
                    NotifyOnComment = NotifyOnComment ?? subscription.NotifyOnComment,
                    NotifyOnError = NotifyOnError ?? subscription.NotifyOnError,
                    NotifyOnFollow = NotifyOnFollow ?? subscription.NotifyOnFollow,
                    NotifyOnLike = NotifyOnLike ?? subscription.NotifyOnLike,
                    NotifyOnPercentChange = NotifyOnPercentChange ?? subscription.NotifyOnPercentChange,
                    NotifyOnPercentChangeSet = NotifyOnPercentChangeSet ?? subscription.NotifyOnPercentChangeSet,
                    NotifyWhenAbove = NotifyWhenAbove ?? subscription.NotifyWhenAbove,
                    NotifyWhenAboveSet = NotifyWhenAboveSet ?? subscription.NotifyWhenAboveSet,
                    NotifyWhenBelow = NotifyWhenBelow ?? subscription.NotifyWhenBelow
                };
            }
        }
    }
}