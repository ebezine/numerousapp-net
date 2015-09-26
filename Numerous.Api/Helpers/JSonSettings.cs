using System.Collections.Generic;
using System.Net.Http.Formatting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Numerous.Api.Helpers
{
    internal static class JSonSettings
    {
        private static readonly JsonSerializerSettings serializerSettings = new JsonSerializerSettings
        {
            DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore,
            DateTimeZoneHandling = DateTimeZoneHandling.Utc,
            DateFormatString = "yyyy-MM-ddTHH:mm:ss.fffZ",
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        private static readonly IEnumerable<MediaTypeFormatter> formatter = new[] { new JsonMediaTypeFormatter { SerializerSettings = serializerSettings } };

        public static JsonSerializerSettings SerializerSettings
        {
            get { return serializerSettings; }
        }

        public static IEnumerable<MediaTypeFormatter> Formatter
        {
            get { return formatter; }
        }
    }
}