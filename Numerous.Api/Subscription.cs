#region References

using Newtonsoft.Json;

#endregion

namespace Numerous.Api
{
    /// <summary>
    /// Represents a subscription of a user to a metric.
    /// </summary>
    /// <remarks>
    /// This object is read-only. Use <see cref="Subscription.Edit"/> to create or update a subscription.
    /// </remarks>
    public class Subscription
    {
        #region Properties

        /// <summary>
        /// Gets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        [JsonProperty("userId")]
        public long UserId { get; internal set; }

        /// <summary>
        /// Gets the metric identifier.
        /// </summary>
        /// <value>
        /// The metric identifier.
        /// </value>
        [JsonProperty("metricId")]
        public long MetricId { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether notifications are enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if notifications are enabled; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("notificationsEnabled")]
        public bool NotificationsEnabled { get; internal set; }

        /// <summary>
        /// Gets a value above what the user is notified.
        /// </summary>
        /// <value>
        /// The threshold value.
        /// </value>
        [JsonProperty("notifyWhenAbove")]
        public decimal NotifyWhenAbove { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether above threshold notification is enabled.
        /// </summary>
        /// <value>
        /// <c>true</c> if above threshold notification is enabled; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("notifyWhenAboveSet")]
        public bool NotifyWhenAboveEnabled { get; internal set; }

        /// <summary>
        /// Gets a value below what the user is notified.
        /// </summary>
        /// <value>
        /// The threshold value.
        /// </value>
        [JsonProperty("notifyWhenBelow")]
        public decimal NotifyWhenBelow { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether below threshold notification is enabled.
        /// </summary>
        /// <value>
        /// <c>true</c> if below threshold notification is enabled; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("notifyWhenBelowSet")]
        public bool NotifyWhenBelowEnabled { get; internal set; }

        /// <summary>
        /// Gets a percent change above when the user is notified.
        /// </summary>
        /// <value>
        /// The percent threshold value.
        /// </value>
        [JsonProperty("notifyOnPercentChange")]
        public decimal NotifyOnPercentChange { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether percent change notification is enabled.
        /// </summary>
        /// <value>
        /// <c>true</c> if percent change notification is enabled; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("notifyOnPercentChangeSet")]
        public bool NotifyOnPercentChangeEnabled { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether to notify on comment.
        /// </summary>
        /// <value>
        ///   <c>true</c> if notify on comment; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("notifyOnComment")]
        public bool NotifyOnComment { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether to notify on like.
        /// </summary>
        /// <value>
        ///   <c>true</c> if notify on like; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("notifyOnLike")]
        public bool NotifyOnLike { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether to notify on follow.
        /// </summary>
        /// <value>
        ///   <c>true</c> if notify on follow; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("notifyOnFollow")]
        public bool NotifyOnFollow { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether to notify on error.
        /// </summary>
        /// <value>
        ///   <c>true</c> if notify on error; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("notifyOnError")]
        public bool NotifyOnError { get; internal set; }

        #endregion

        public class Edit
        {
            #region Properties

            /// <summary>
            /// Gets a value indicating whether notifications are enabled.
            /// </summary>
            /// <value>
            ///   <c>true</c> if notifications are enabled; otherwise, <c>false</c>.
            /// </value>
            [JsonProperty("notificationsEnabled")]
            public bool? NotificationsEnabled { get; set; }

            /// <summary>
            /// Gets a value above what the user is notified.
            /// </summary>
            /// <value>
            /// The threshold value.
            /// </value>
            [JsonProperty("notifyWhenAbove")]
            public decimal? NotifyWhenAbove { get; set; }

            /// <summary>
            /// Gets a value indicating whether above threshold notification is enabled.
            /// </summary>
            /// <value>
            /// <c>true</c> if above threshold notification is enabled; otherwise, <c>false</c>.
            /// </value>
            [JsonProperty("notifyWhenAboveSet")]
            public bool? NotifyWhenAboveEnabled { get; set; }

            /// <summary>
            /// Gets a value below what the user is notified.
            /// </summary>
            /// <value>
            /// The threshold value.
            /// </value>
            [JsonProperty("notifyWhenBelow")]
            public decimal? NotifyWhenBelow { get; set; }

            /// <summary>
            /// Gets a value indicating whether below threshold notification is enabled.
            /// </summary>
            /// <value>
            /// <c>true</c> if below threshold notification is enabled; otherwise, <c>false</c>.
            /// </value>
            [JsonProperty("notifyWhenBelowSet")]
            public bool? NotifyWhenBelowEnabled { get; set; }

            /// <summary>
            /// Gets a percent change above when the user is notified.
            /// </summary>
            /// <value>
            /// The percent threshold value.
            /// </value>
            [JsonProperty("notifyOnPercentChange")]
            public decimal? NotifyOnPercentChange { get; set; }

            /// <summary>
            /// Gets a value indicating whether percent change notification is enabled.
            /// </summary>
            /// <value>
            /// <c>true</c> if percent change notification is enabled; otherwise, <c>false</c>.
            /// </value>
            [JsonProperty("notifyOnPercentChangeSet")]
            public bool? NotifyOnPercentChangeEnabled { get; set; }

            /// <summary>
            /// Gets a value indicating whether to notify on comment.
            /// </summary>
            /// <value>
            ///   <c>true</c> if notify on comment; otherwise, <c>false</c>.
            /// </value>
            [JsonProperty("notifyOnComment")]
            public bool? NotifyOnComment { get; set; }

            /// <summary>
            /// Gets a value indicating whether to notify on like.
            /// </summary>
            /// <value>
            ///   <c>true</c> if notify on like; otherwise, <c>false</c>.
            /// </value>
            [JsonProperty("notifyOnLike")]
            public bool? NotifyOnLike { get; set; }

            /// <summary>
            /// Gets a value indicating whether to notify on follow.
            /// </summary>
            /// <value>
            ///   <c>true</c> if notify on follow; otherwise, <c>false</c>.
            /// </value>
            [JsonProperty("notifyOnFollow")]
            public bool? NotifyOnFollow { get; set; }

            /// <summary>
            /// Gets a value indicating whether to notify on error.
            /// </summary>
            /// <value>
            ///   <c>true</c> if notify on error; otherwise, <c>false</c>.
            /// </value>
            [JsonProperty("notifyOnError")]
            public bool? NotifyOnError { get; set; }

            #endregion

            #region Internal Helpers

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
                    NotifyOnPercentChangeEnabled = NotifyOnPercentChangeEnabled ?? subscription.NotifyOnPercentChangeEnabled,
                    NotifyWhenAbove = NotifyWhenAbove ?? subscription.NotifyWhenAbove,
                    NotifyWhenAboveEnabled = NotifyWhenAboveEnabled ?? subscription.NotifyWhenAboveEnabled,
                    NotifyWhenBelow = NotifyWhenBelow ?? subscription.NotifyWhenBelow
                };
            }

            #endregion
        }
    }
}