using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;

namespace Numerous.Api.Helpers
{
    internal class NumerousContext
    {
        public HttpClient Client;
        public NumerousSettings Settings;

        public bool AllowRetry(HttpResponseMessage response, int retryCount)
        {
            switch (response.StatusCode)
            {
                case (HttpStatusCode)429:       // Quota exceeded
                    return retryCount <= Settings.QuotaExceededRetry;

                // Error class 5xx is typically due to transient errors
                case HttpStatusCode.InternalServerError:
                case HttpStatusCode.BadGateway:
                case HttpStatusCode.GatewayTimeout:
                case HttpStatusCode.ServiceUnavailable:

                // Error class 4xx is due to client error, just handle request timeout
                case HttpStatusCode.RequestTimeout:
                    return retryCount <= Settings.ErrorRetry;

                default:
                    return false;
            }
        }

        public void WaitRetry(HttpResponseMessage response)
        {
            var reset = response.Headers.GetValues("X-Rate-Limit-Reset")
                .Select(v => TimeSpan.FromSeconds(int.Parse(v, CultureInfo.InvariantCulture) + 1))
                .DefaultIfEmpty(Settings.ErrorRetryDelay)
                .First();

            Thread.Sleep(reset);
        }
    }
}