using System;
using Newtonsoft.Json;

namespace Numerous.Api
{
    public class Metric
    {
        [JsonProperty("label")]
        public string Label { get; internal set; }

        [JsonProperty("description")]
        public string Description { get; internal set; }

        [JsonProperty("photoId")]
        public string PhotoId { get; internal set; }

        [JsonProperty("kind")]
        public MetricKind Kind { get; internal set; }

        [JsonProperty("currencySymbol")]
        public string CurrencySymbol { get; internal set; }

        [JsonProperty("value")]
        public decimal Value { get; internal set; }

        [JsonProperty("precision")]
        public int Precision { get; internal set; }

        [JsonProperty("units")]
        public string Units { get; internal set; }

        [JsonProperty("unit")]
        public string Unit { get; internal set; }

        [JsonProperty("visibility")]
        public Visibility Visibility { get; internal set; }

        [JsonProperty("moreUrl")]
        public string MoreUrl { get; internal set; }

        [JsonProperty("channelId")]
        public long ChannelId { get; internal set; }

        [JsonProperty("sourceKey")]
        public string SourceKey { get; internal set; }

        [JsonProperty("sourceClass")]
        public string SourceClass { get; internal set; }

        //public PhotoTreatment PhotoTreatment { get; internal set; }
        //public GraphingOptions GraphingOptions { get; internal set; }

        [JsonProperty("id")]
        public long Id { get; internal set; }

        [JsonProperty("isOwnedByChannel")]
        public bool IsOwnedByChannel { get; internal set; }

        [JsonProperty("ownerId")]
        public long OwnerId { get; internal set; }

        [JsonProperty("updated")]
        public DateTime Updated { get; internal set; }

        [JsonProperty("photoURL")]
        public string PhotoUrl { get; internal set; }

        [JsonProperty("subscriberCount")]
        public int SubscriberCount { get; internal set; }

        [JsonProperty("links")]
        public MetricLinks Links { get; internal set; }

        public class Edit
        {
            public string Label { get; set; }
            public string Description { get; set; }
            public string PhotoId { get; set; }
            public MetricKind? Kind { get; set; }
            public string MoreUrl { get; set; }
            public string CurrencySymbol { get; set; }
            public int? Precision { get; set; }
            public string Units { get; set; }
            public string Unit { get; set; }
            public Visibility? Visibility { get; set; }
            public string SourceKey { get; set; }
            public string SourceClass { get; set; }
            //public PhotoTreatment PhotoTreatment { get; set; }
            //public GraphingOptions GraphingOptions { get; set; }
            //public decimal? Value { get; set; }

            internal Edit DefaultTo(Metric existingMetric)
            {
                var temperatureUnit = (Kind ?? existingMetric.Kind) == MetricKind.Temperature ? "°F" : null;

                return new Edit
                {
                    Label = Label ?? existingMetric.Label,
                    Description = Description ?? existingMetric.Description,
                    PhotoId = PhotoId ?? existingMetric.PhotoId,
                    Kind = Kind ?? existingMetric.Kind,
                    MoreUrl = MoreUrl ?? existingMetric.MoreUrl,
                    CurrencySymbol = CurrencySymbol ?? existingMetric.CurrencySymbol,
                    Precision = Precision ?? existingMetric.Precision,
                    Units = temperatureUnit ?? Units ?? Unit ?? existingMetric.Units,
                    Unit = temperatureUnit ?? Unit ?? Units ?? existingMetric.Unit,
                    Visibility = Visibility ?? existingMetric.Visibility,
                    SourceKey = SourceKey ?? existingMetric.SourceKey,
                    SourceClass = SourceClass ?? existingMetric.SourceClass,
                    //Value = Value //?? existingMetric.Value
                };
            }
        }
    }
}