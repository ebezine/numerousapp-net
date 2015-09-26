using System;

namespace Numerous.Api
{
    public class NumerousSettings
    {
        public string ApiKey;

        public Retry QuotaExceededRetry = Retry.Infinite;
        public Retry ErrorRetry = Retry.Never;
        public TimeSpan ErrorRetryDelay = TimeSpan.FromSeconds(30);
    }
}