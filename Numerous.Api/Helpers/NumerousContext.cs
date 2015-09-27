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
        #region Public Fields

        public HttpClient Client;
        public NumerousSettings Settings;

        #endregion

        #region Methods

        public bool AllowRetry(HttpResponseMessage response, int tryMade)
        {
            var nextTry = tryMade + 1;

            if (response == null)
                // Unhandled exception while calling API.
                return nextTry <= Settings.NetworkErrorRetry;

            switch (response.StatusCode)
            {
                case (HttpStatusCode) 429: // Quota exceeded
                    return nextTry <= Settings.QuotaExceededRetry;

                    // Error class 5xx is typically due to transient errors
                case HttpStatusCode.InternalServerError:
                case HttpStatusCode.BadGateway:
                case HttpStatusCode.GatewayTimeout:
                case HttpStatusCode.ServiceUnavailable:

                    // Error class 4xx is due to client error, just handle request timeout
                case HttpStatusCode.RequestTimeout:
                    return nextTry <= Settings.HttpErrorRetry;

                default:
                    return false;
            }
        }

        public void WaitRetry(HttpResponseMessage response)
        {
            var delay =
                response != null ?
                    response.Headers.GetValues("X-Rate-Limit-Reset")
                        .Select(v => TimeSpan.FromSeconds(int.Parse(v, CultureInfo.InvariantCulture) + 1))
                        .DefaultIfEmpty(Settings.ErrorRetryDelay)
                        .First()
                    : Settings.ErrorRetryDelay;

            Thread.Sleep(delay);
        }

        public static NumerousContext Create(NumerousSettings settings)
        {
            return new NumerousContext
            {
                Settings = settings,
                Client = CreateClient(settings)
            };
        }

        #endregion

        #region Private Helpers

        private static HttpClient CreateClient(NumerousSettings settings)
        {
            var credentials = new NetworkCredential(settings.ApiKey, string.Empty);
            var handler = new HttpClientHandler {Credentials = credentials};

            return new HttpClient(handler)
            {
                BaseAddress = settings.ApiEndPoint,
                Timeout = settings.Timeout
            };
        }

        #endregion
    }
}