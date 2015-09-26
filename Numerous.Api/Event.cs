#region References

using System;
using Newtonsoft.Json;

#endregion

namespace Numerous.Api
{
    /// <summary>
    /// Represents an event.
    /// </summary>
    /// <remarks>
    /// This object is read-only. Use <see cref="Event.Edit"/> to create a new event.
    /// </remarks>
    public class Event
    {
        #region Properties

        /// <summary>
        /// Gets the event identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [JsonProperty("id")]
        public long Id { get; internal set; }

        /// <summary>
        /// Gets the metric identifier.
        /// </summary>
        /// <value>
        /// The metric identifier.
        /// </value>
        [JsonProperty("metricId")]
        public long MetricId { get; internal set; }

        /// <summary>
        /// Gets the author identifier.
        /// </summary>
        /// <value>
        /// The author identifier.
        /// </value>
        [JsonProperty("authorId")]
        public long AuthorId { get; internal set; }

        /// <summary>
        /// Gets the event date.
        /// </summary>
        /// <value>
        /// The event date.
        /// </value>
        [JsonProperty("updated")]
        public DateTime Date { get; internal set; }

        /// <summary>
        /// Gets the event action.
        /// </summary>
        /// <value>
        /// The event action.
        /// </value>
        [JsonIgnore]
        public EventAction Action
        {
            get { return ActionName == "ADD" ? EventAction.Add : EventAction.Set; }
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        [JsonProperty("value")]
        public decimal Value { get; internal set; }

        //[JsonProperty("onlyIfChanged")]
        //public bool OnlyIfChanged { get; internal set; }

        #endregion

        /// <summary>
        /// Represents an event creation.
        /// </summary>
        public class Edit
        {
            #region Properties

            /// <summary>
            /// Gets or sets the event date.
            /// </summary>
            /// <value>
            /// The date.
            /// </value>
            /// <remarks>Leave this value to <c>null</c> in order to record event at the current date.</remarks>
            [JsonProperty("updated")]
            public DateTime? Date { get; set; }

            /// <summary>
            /// Gets or sets the value.
            /// </summary>
            /// <value>
            /// The value.
            /// </value>
            public decimal Value { get; set; }

            /// <summary>
            /// Gets or sets a value indicating whether to record the event only whether value changed.
            /// </summary>
            /// <value>
            ///   <c>true</c> if event should be recorded only whether value changed; otherwise, <c>false</c>.
            /// </value>
            public bool OnlyIfChanged { get; set; }

            /// <summary>
            /// Gets or sets the action.
            /// </summary>
            /// <value>
            /// The action.
            /// </value>
            [JsonIgnore]
            public EventAction Action
            {
                get { return ActionName == "ADD" ? EventAction.Add : EventAction.Set; }

                set { ActionName = value == EventAction.Add ? "ADD" : null; }
            }

            #endregion

            #region Internal Properties

            [JsonProperty("action")]
            internal string ActionName { get; set; }

            #endregion
        }

        #region Internal Properties

        [JsonProperty("action")]
        internal string ActionName { get; set; }

        #endregion
    }
}