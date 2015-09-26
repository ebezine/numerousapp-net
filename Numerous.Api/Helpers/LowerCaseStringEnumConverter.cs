using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Numerous.Api.Helpers
{
    internal class LowerCaseStringEnumConverter : StringEnumConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var lowerCaseValue = Enum.GetName(value.GetType(), value).ToLowerInvariant();
            writer.WriteValue(lowerCaseValue);
        }
    }
}