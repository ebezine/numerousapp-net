#region References

using System;

#endregion

namespace Numerous.Api
{
    /// <summary>
    /// Represents settings for <see cref="NumerousClient"/>.
    /// </summary>
    public class NumerousSettings
    {
        public static readonly Uri DefaultApiEndpoint = new Uri("https://api.numerousapp.com/v2/");
        public static TimeSpan DefaultTimeout = TimeSpan.FromSeconds(100);

        #region Fields

        /// <summary>
        /// The API end point.
        /// </summary>
        public Uri ApiEndPoint = DefaultApiEndpoint;

        /// <summary>
        /// The API key.
        /// </summary>
        public string ApiKey;

        /// <summary>
        /// The retry count for quota issues.
        /// </summary>
        /// <remarks>Default is <see cref="Retry.Infinite"/>.</remarks>
        public Retry QuotaExceededRetry = Retry.Infinite;

        /// <summary>
        /// The retry count for HTTP errors.
        /// </summary>
        /// <remarks>Default is <c>1</c>.</remarks>
        public Retry HttpErrorRetry = 1;

        /// <summary>
        /// The retry count for non-HTTP network errors.
        /// </summary>
        /// <remarks>Default is <c>1</c>.</remarks>
        public Retry NetworkErrorRetry = 1;

        /// <summary>
        /// The error retry delay.
        /// </summary>
        /// <remarks>Default is 30 seconds.</remarks>
        public TimeSpan ErrorRetryDelay = TimeSpan.FromSeconds(30);

        /// <summary>
        /// The timeout for API requests.
        /// </summary>
        public TimeSpan Timeout = DefaultTimeout;

        #endregion
    }
}