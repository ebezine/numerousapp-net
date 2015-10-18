#region References

using System;
using Newtonsoft.Json;

#endregion

namespace Numerous.Api
{
    /// <summary>
    /// Represents a metric.
    /// </summary>
    /// <remarks>This object is read-only. Use <see cref="Metric.Edit"/> to create or update a metric.</remarks>
    public class Metric
    {
        #region Properties

        /// <summary>
        /// Gets the metric identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [JsonProperty("id")]
        public long Id { get; internal set; }

        /// <summary>
        /// Gets the metric name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [JsonProperty("label")]
        public string Name { get; internal set; }

        /// <summary>
        /// Gets the metric description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        [JsonProperty("description")]
        public string Description { get; internal set; }

        /// <summary>
        /// Gets the owner identifier.
        /// </summary>
        /// <value>
        /// The owner identifier.
        /// </value>
        [JsonProperty("ownerId")]
        public long OwnerId { get; internal set; }

        /// <summary>
        /// Gets the metric kind.
        /// </summary>
        /// <value>
        /// The kind.
        /// </value>
        [JsonProperty("kind")]
        public MetricKind Kind { get; internal set; }

        /// <summary>
        /// Gets the currency symbol, if <c>Kind</c> is <see cref="MetricKind.Currency"/>.
        /// </summary>
        /// <value>
        /// The currency symbol.
        /// </value>
        [JsonProperty("currencySymbol")]
        public string CurrencySymbol { get; internal set; }

        /// <summary>
        /// Gets the precision.
        /// </summary>
        /// <value>
        /// The precision.
        /// </value>
        /// <remarks>Precision correspond to the number of decimals. Use <c>-1</c> for maximum precision.</remarks>
        [JsonProperty("precision")]
        public int Precision { get; internal set; }

        /// <summary>
        /// Gets the plural form of the metric unit.
        /// </summary>
        /// <value>
        /// The plural form of unit.
        /// </value>
        [JsonProperty("units")]
        public string Units { get; internal set; }

        /// <summary>
        /// Gets the singular form of the metric unit.
        /// </summary>
        /// <value>
        /// The singular form of unit.
        /// </value>
        [JsonProperty("unit")]
        public string Unit { get; internal set; }

        /// <summary>
        /// Gets the current metric value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        [JsonProperty("value")]
        public decimal Value { get; internal set; }

        /// <summary>
        /// Gets the metric visibility.
        /// </summary>
        /// <value>
        /// The visibility.
        /// </value>
        [JsonProperty("visibility")]
        public Visibility Visibility { get; internal set; }

        /// <summary>
        /// Gets the url of the metric details page.
        /// </summary>
        /// <value>
        /// The details page url.
        /// </value>
        [JsonProperty("moreUrl")]
        public string DetailsUrl { get; internal set; }

        /// <summary>
        /// Gets the metric channel identifier.
        /// </summary>
        /// <value>
        /// The channel identifier.
        /// </value>
        [JsonProperty("channelId")]
        public long ChannelId { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether this instance is owned by channel.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is owned by channel; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("isOwnedByChannel")]
        public bool IsOwnedByChannel { get; internal set; }

        /// <summary>
        /// Gets the metric source class.
        /// </summary>
        /// <value>
        /// The source class.
        /// </value>
        [JsonProperty("sourceClass")]
        public string SourceClass { get; internal set; }

        /// <summary>
        /// Gets the metric source key.
        /// </summary>
        /// <value>
        /// The source key.
        /// </value>
        [JsonProperty("sourceKey")]
        public string SourceKey { get; internal set; }

        /// <summary>
        /// Gets the metric update date.
        /// </summary>
        /// <value>
        /// The update date.
        /// </value>
        [JsonProperty("updated")]
        public DateTime UpdateDate { get; internal set; }

        /// <summary>
        /// Gets the metric last event date.
        /// </summary>
        /// <value>
        /// The last event date.
        /// </value>
        [JsonProperty("latestEventUpdated")]
        public DateTime LastEventDate { get; internal set; }

        /// <summary>
        /// Gets the metric photo identifier, if photo is from Numerous presets.
        /// </summary>
        /// <value>
        /// The photo identifier.
        /// </value>
        [JsonProperty("photoId")]
        public string PhotoId { get; internal set; }

        /// <summary>
        /// Gets the photo URL.
        /// </summary>
        /// <value>
        /// The photo URL.
        /// </value>
        [JsonProperty("photoURL")]
        public string PhotoUrl { get; internal set; }

        //TODO: implement PhotoTreatment
        //public PhotoTreatment PhotoTreatment { get; internal set; }

        //TODO: implement GraphingOptions
        //public GraphingOptions GraphingOptions { get; internal set; }

        /// <summary>
        /// Gets the metric subscriber count.
        /// </summary>
        /// <value>
        /// The subscriber count.
        /// </value>
        [JsonProperty("subscriberCount")]
        public int SubscriberCount { get; internal set; }

        /// <summary>
        /// Gets the metric links.
        /// </summary>
        /// <value>
        /// The links.
        /// </value>
        [JsonProperty("links")]
        public MetricLinks Links { get; internal set; }

        #endregion

        /// <summary>
        /// Represents a metric creation or update.
        /// </summary>
        public class Edit
        {
            #region Properties

            /// <summary>
            /// Gets or sets the owner identifier.
            /// </summary>
            /// <value>
            /// The owner identifier.
            /// </value>
            [JsonProperty("ownerId")]
            public long? OwnerId { get; set; }

            /// <summary>
            /// Gets or sets the metric name.
            /// </summary>
            /// <value>
            /// The name.
            /// </value>
            [JsonProperty("label")]
            public string Name { get; set; }

            /// <summary>
            /// Gets or sets the metric description.
            /// </summary>
            /// <value>
            /// The description.
            /// </value>
            [JsonProperty("description")]
            public string Description { get; set; }

            /// <summary>
            /// Gets or sets the metric kind.
            /// </summary>
            /// <value>
            /// The kind.
            /// </value>
            [JsonProperty("kind")]
            public MetricKind? Kind { get; set; }

            /// <summary>
            /// Gets or sets the currency symbol, if <c>Kind</c> is <see cref="MetricKind.Currency"/>.
            /// </summary>
            /// <value>
            /// The currency symbol.
            /// </value>
            [JsonProperty("currencySymbol")]
            public string CurrencySymbol { get; set; }

            /// <summary>
            /// Gets or sets the precision.
            /// </summary>
            /// <value>
            /// The precision.
            /// </value>
            /// <remarks>Precision correspond to the number of decimals. Use <c>-1</c> for maximum precision.</remarks>
            [JsonProperty("precision")]
            public int? Precision { get; set; }

            /// <summary>
            /// Gets or sets the plural form of the metric unit.
            /// </summary>
            /// <value>
            /// The plural form of unit.
            /// </value>
            /// <remarks>Unit should not be set on temperature</remarks>
            [JsonProperty("units")]
            public string Units { get; set; }

            /// <summary>
            /// Gets or sets the singular form of the metric unit.
            /// </summary>
            /// <value>
            /// The singular form of unit.
            /// </value>
            /// <remarks>Unit should not be set on temperature</remarks>
            [JsonProperty("unit")]
            public string Unit { get; set; }

            /// <summary>
            /// Gets or sets the metric visibility.
            /// </summary>
            /// <value>
            /// The visibility.
            /// </value>
            [JsonProperty("visibility")]
            public Visibility? Visibility { get; set; }

            /// <summary>
            /// Gets or sets the url of the metric details page.
            /// </summary>
            /// <value>
            /// The details page url.
            /// </value>
            [JsonProperty("moreUrl")]
            public string DetailsUrl { get; set; }

            /// <summary>
            /// Gets or sets the metric source class.
            /// </summary>
            /// <value>
            /// The source class.
            /// </value>
            [JsonProperty("sourceClass")]
            public string SourceClass { get; set; }

            /// <summary>
            /// Gets or sets the metric source key.
            /// </summary>
            /// <value>
            /// The source key.
            /// </value>
            [JsonProperty("sourceKey")]
            public string SourceKey { get; set; }

            /// <summary>
            /// Gets or sets the metric photo identifier, if photo is from Numerous presets.
            /// </summary>
            /// <value>
            /// The photo identifier.
            /// </value>
            [JsonProperty("photoId")]
            public string PhotoId { get; set; }

            //TODO: implement PhotoTreatment
            //public PhotoTreatment PhotoTreatment { get; set; }

            //TODO: implement GraphingOptions
            //public GraphingOptions GraphingOptions { get; set; }

            #endregion

            #region Internal Helpers

            internal Edit DefaultTo(Metric existingMetric)
            {
                var temperatureUnit = (Kind ?? existingMetric.Kind) == MetricKind.Temperature ? "°F" : null;

                return new Edit
                {
                    OwnerId = OwnerId ?? existingMetric.OwnerId,
                    Name = Name ?? existingMetric.Name,
                    Description = Description ?? existingMetric.Description,
                    PhotoId = PhotoId ?? existingMetric.PhotoId,
                    Kind = Kind ?? existingMetric.Kind,
                    DetailsUrl = DetailsUrl ?? existingMetric.DetailsUrl,
                    CurrencySymbol = CurrencySymbol ?? existingMetric.CurrencySymbol,
                    Precision = Precision ?? existingMetric.Precision,
                    Units = temperatureUnit ?? Units ?? Unit ?? existingMetric.Units,
                    Unit = temperatureUnit ?? Unit ?? Units ?? existingMetric.Unit,
                    Visibility = Visibility ?? existingMetric.Visibility,
                    SourceKey = SourceKey ?? existingMetric.SourceKey,
                    SourceClass = SourceClass ?? existingMetric.SourceClass,
                };
            }

            #endregion
        }
    }
}