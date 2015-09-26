using Newtonsoft.Json;
using Numerous.Api.Helpers;

namespace Numerous.Api
{
    [JsonConverter(typeof(LowerCaseStringEnumConverter))]
    public enum MetricKind
    {
        Number,
        Percent,
        Timer,
        Currency,
        Temperature
    }
}