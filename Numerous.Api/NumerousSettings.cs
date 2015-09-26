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
        #region Fields

        /// <summary>
        /// The API key.
        /// </summary>
        public string ApiKey;

        /// <summary>
        /// The quota exceeded retry count.
        /// </summary>
        /// <remarks>Default is <see cref="Retry.Infinite"/>.</remarks>
        public Retry QuotaExceededRetry = Retry.Infinite;

        /// <summary>
        /// The error retry count.
        /// </summary>
        /// <remarks>Default is <see cref="Retry.Never"/>.</remarks>
        public Retry ErrorRetry = Retry.Never;

        /// <summary>
        /// The error retry delay.
        /// </summary>
        /// <remarks>Default is 30 seconds.</remarks>
        public TimeSpan ErrorRetryDelay = TimeSpan.FromSeconds(30);

        #endregion
    }
}