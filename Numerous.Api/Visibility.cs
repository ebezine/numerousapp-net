using Newtonsoft.Json;
using Numerous.Api.Helpers;

namespace Numerous.Api
{
    [JsonConverter(typeof(LowerCaseStringEnumConverter))]
    public enum Visibility
    {
        Public,
        Unlisted,
        Private
    }
}