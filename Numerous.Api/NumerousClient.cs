#region References

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Numerous.Api.Helpers;

#endregion

namespace Numerous.Api
{
    public class NumerousClient : IDisposable
    {
        #region Fields
        
        private readonly NumerousContext context;

        #endregion

        #region Instance Management

        /// <summary>
        /// Initializes a new instance of the <see cref="NumerousClient"/> class.
        /// </summary>
        /// <param name="apiKey">The API key.</param>
        public NumerousClient(string apiKey) : this(new NumerousSettings{ApiKey = apiKey}) {}

        /// <summary>
        /// Initializes a new instance of the <see cref="NumerousClient"/> class.
        /// </summary>
        /// <param name="settings">The settings.</param>
        public NumerousClient(NumerousSettings settings)
        {
            context = NumerousContext.Create(settings);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        void IDisposable.Dispose()
        {
            context.Client.Dispose();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Closes this instance.
        /// </summary>
        public void Close()
        {
            ((IDisposable)this).Dispose();
        }

        #region Metrics

        /// <summary>
        /// Gets the metric.
        /// </summary>
        /// <param name="metricId">The metric identifier.</param>
        /// <returns>The metric.</returns>
        public async Task<Metric> GetMetric(long metricId)
        {
            var metricsUrl = string.Format(CultureInfo.InvariantCulture, "metrics/{0}", metricId);
            var response = await context.GetWithRetry(metricsUrl);
            return await response.Content.ReadAsAsync<Metric>(JSonSettings.Formatter);
        }

        /// <summary>
        /// Gets the user metrics.
        /// </summary>
        /// <param name="userId">The user identifier. If omitted, metrics owned by the current user are returned.</param>
        /// <returns>The user metrics.</returns>
        public async Task<ResultPage<Metric>> GetUserMetrics(long userId = 0)
        {
            var metricsUrl = string.Format(CultureInfo.InvariantCulture, "users/{0}/metrics", GetUserIdParameter(userId));
            return await GetResultView<MetricResultPage, Metric>(metricsUrl);
        }

        /// <summary>
        /// Gets the popular metrics.
        /// </summary>
        /// <param name="count">The count of requested metrics. Default value is <c>10</c>.</param>
        /// <returns>The metrics</returns>
        public async Task<ResultPage<Metric>> GetPopularMetrics(int count = 10)
        {
            var metricsUrl = string.Format(CultureInfo.InvariantCulture, "metrics/popular?count={0}", count);
            var response = await context.GetWithRetry(metricsUrl);
            
            var results = await response.Content.ReadAsAsync<IEnumerable<Metric>>(JSonSettings.Formatter);

            return ResultPage<Metric>.SinglePage(results);
        }

        /// <summary>
        /// Gets the metric image.
        /// </summary>
        /// <param name="metricId">The metric identifier.</param>
        /// <returns>The image.</returns>
        public async Task<Image> GetMetricImage(long metricId)
        {
            var photoUrl = string.Format(CultureInfo.InvariantCulture, "metrics/{0}/photo", metricId);
            var response = await context.GetWithRetry(photoUrl);
            
            var data = await response.Content.ReadAsByteArrayAsync();

            return new Image
            {
                MediaType = response.Content.Headers.ContentType.MediaType,
                Data = data
            };
        }

        /// <summary>
        /// Adds the specified metric.
        /// </summary>
        /// <param name="metric">The metric.</param>
        /// <returns>The added metric.</returns>
        public async Task<Metric> AddMetric(Metric.Edit metric)
        {
            var jsonMetric = JsonConvert.SerializeObject(metric, JSonSettings.SerializerSettings);

            var response = await context.PostWithRetry("metrics", jsonMetric);
            return await response.Content.ReadAsAsync<Metric>(JSonSettings.Formatter);
        }

        /// <summary>
        /// Updates the metric.
        /// </summary>
        /// <param name="metricId">The metric identifier.</param>
        /// <param name="metric">The metric.</param>
        /// <returns>The updated metric.</returns>
        public async Task<Metric> UpdateMetric(long metricId, Metric.Edit metric)
        {
            // All information seem to be required in order to update a metric definition
            var existingMetric = await GetMetric(metricId);
            var actualMetricUpdate = metric.DefaultTo(existingMetric);

            var jsonMetric = JsonConvert.SerializeObject(actualMetricUpdate, JSonSettings.SerializerSettings);

            var metricsUrl = string.Format(CultureInfo.InvariantCulture, "metrics/{0}", metricId);
            var response = await context.PutWithRetry(metricsUrl, jsonMetric);
            return await response.Content.ReadAsAsync<Metric>(JSonSettings.Formatter);
        }

        /// <summary>
        /// Updates the metric image.
        /// </summary>
        /// <param name="metricId">The metric identifier.</param>
        /// <param name="image">The image.</param>
        /// <returns>The updated metric.</returns>
        public async Task<Metric> UpdateMetricImage(long metricId, Image.Edit image)
        {
            var photoUrl = string.Format(CultureInfo.InvariantCulture, "metrics/{0}/photo", metricId);
            var response = await context.PostWithRetry(photoUrl, image);
            return await response.Content.ReadAsAsync<Metric>(JSonSettings.Formatter);
        }

        /// <summary>
        /// Deletes the metric.
        /// </summary>
        /// <param name="metricId">The metric identifier.</param>
        /// <returns>The deletion task.</returns>
        public async Task DeleteMetric(long metricId)
        {
            var metricUrl = string.Format(CultureInfo.InvariantCulture, "metrics/{0}", metricId);
            await context.DeleteWithRetry(metricUrl);
        }

        /// <summary>
        /// Deletes the metric image.
        /// </summary>
        /// <param name="metricId">The metric identifier.</param>
        /// <returns>The deletion task.</returns>
        public async Task DeleteMetricImage(long metricId)
        {
            var metricUrl = string.Format(CultureInfo.InvariantCulture, "metrics/{0}/photo", metricId);
            await context.DeleteWithRetry(metricUrl);
        }

        #endregion

        #region Channel Metrics

        public async Task<Metric> AddChannelMetric(long channelId, string sourceClass, string sourceKey, Metric.Edit metric)
        {
            metric.SourceClass = sourceClass;
            metric.SourceKey = sourceKey;

            var jsonMetric = JsonConvert.SerializeObject(metric, JSonSettings.SerializerSettings);
            var metricsUrl = string.Format(CultureInfo.InvariantCulture, "channels/{0}/metrics/{1}/{2}", channelId, sourceClass, sourceKey);

            var response = await context.PostWithRetry(metricsUrl, jsonMetric);
            return await response.Content.ReadAsAsync<Metric>(JSonSettings.Formatter);
        }

        public async Task<Metric> GetChannelMetric(long channelId, string sourceClass, string sourceKey)
        {
            var metricsUrl = string.Format(CultureInfo.InvariantCulture, "channels/{0}/metrics/{1}/{2}", channelId, sourceClass, sourceKey);
            var response = await context.GetWithRetry(metricsUrl);
            return await response.Content.ReadAsAsync<Metric>(JSonSettings.Formatter);
        }

        public async Task<ResultPage<Metric>> GetChannelMetrics(long channelId, string sourceClass = null)
        {
            var metricsUrl = string.IsNullOrEmpty(sourceClass)
                ? string.Format(CultureInfo.InvariantCulture, "channels/{0}/metrics", channelId)
                : string.Format(CultureInfo.InvariantCulture, "channels/{0}/metrics/{1}", channelId, sourceClass);
            return await GetResultView<MetricResultPage, Metric>(metricsUrl);
        }

        #endregion

        #region Events

        /// <summary>
        /// Gets the event.
        /// </summary>
        /// <param name="metricId">The metric identifier.</param>
        /// <param name="eventId">The event identifier.</param>
        /// <returns>The event.</returns>
        public async Task<Event> GetEvent(long metricId, long eventId)
        {
            var eventsUrl = string.Format(CultureInfo.InvariantCulture, "metrics/{0}/events/{1}", metricId, eventId);

            var response = await context.GetWithRetry(eventsUrl);
            return await response.Content.ReadAsAsync<Event>(JSonSettings.Formatter);
        }

        /// <summary>
        /// Gets the events.
        /// </summary>
        /// <param name="metricId">The metric identifier.</param>
        /// <returns>The events.</returns>
        public async Task<ResultPage<Event>> GetEvents(long metricId)
        {
            var eventsUrl = string.Format(CultureInfo.InvariantCulture, "metrics/{0}/events", metricId);
            return await GetResultView<EventResultPage, Event>(eventsUrl);
        }

        /// <summary>
        /// Gets the nearest event.
        /// </summary>
        /// <param name="metricId">The metric identifier.</param>
        /// <param name="date">The date.</param>
        /// <returns>The event.</returns>
        public async Task<Event> GetNearestEvent(long metricId, DateTime date)
        {
            var eventsUrl = string.Format(CultureInfo.InvariantCulture, "metrics/{0}/events?t={1}", metricId, date.ToString(JSonSettings.SerializerSettings.DateFormatString, CultureInfo.InvariantCulture));

            var response = await context.GetWithRetry(eventsUrl);
            return await response.Content.ReadAsAsync<Event>(JSonSettings.Formatter);
        }

        /// <summary>
        /// Adds the event.
        /// </summary>
        /// <param name="metricId">The metric identifier.</param>
        /// <param name="event">The event.</param>
        /// <returns>The added event.</returns>
        public async Task<Event> AddEvent(long metricId, Event.Edit @event)
        {
            var eventsUrl = string.Format(CultureInfo.InvariantCulture, "metrics/{0}/events", metricId);

            var jsonEvent = JsonConvert.SerializeObject(@event, JSonSettings.SerializerSettings);
            var response = await context.PostWithRetry(eventsUrl, jsonEvent);
            return await response.Content.ReadAsAsync<Event>(JSonSettings.Formatter);
        }

        /// <summary>
        /// Deletes the event.
        /// </summary>
        /// <param name="metricId">The metric identifier.</param>
        /// <param name="eventId">The event identifier.</param>
        /// <returns>The deletion task.</returns>
        public async Task DeleteEvent(long metricId, long eventId)
        {
            var eventsUrl = string.Format(CultureInfo.InvariantCulture, "metrics/{0}/events/{1}", metricId, eventId);
            await context.DeleteWithRetry(eventsUrl);
        }
        
        #endregion

        #region Subscriptions

        /// <summary>
        /// Gets the subscription.
        /// </summary>
        /// <param name="metricId">The metric identifier.</param>
        /// <param name="userId">The user identifier. If omitted, subscription owned by the current user is returned.</param>
        /// <returns>The subscription.</returns>
        public async Task<Subscription> GetSubscription(long metricId, long userId = 0)
        {
            var subscriptionsUrl = string.Format(CultureInfo.InvariantCulture, "metrics/{0}/subscriptions/{1}", metricId, GetUserIdParameter(userId));
            var response = await context.GetWithRetry(subscriptionsUrl);
            return await response.Content.ReadAsAsync<Subscription>(JSonSettings.Formatter);
        }

        /// <summary>
        /// Gets the user subscriptions.
        /// </summary>
        /// <param name="userId">The user identifier. If omitted, subscription owned by the current user are returned.</param>
        /// <returns>The subscriptions.</returns>
        public async Task<ResultPage<Subscription>> GetUserSubscriptions(long userId = 0)
        {
            var subscriptionsUrl = string.Format(CultureInfo.InvariantCulture, "users/{0}/subscriptions", GetUserIdParameter(userId));
            return await GetResultView<SubscriptionResultPage, Subscription>(subscriptionsUrl);
        }

        /// <summary>
        /// Gets the metric subscriptions.
        /// </summary>
        /// <param name="metricId">The metric identifier.</param>
        /// <returns>The subscriptions.</returns>
        public async Task<ResultPage<Subscription>> GetMetricSubscriptions(long metricId)
        {
            var subscriptionsUrl = string.Format(CultureInfo.InvariantCulture, "metrics/{0}/subscriptions", metricId);
            return await GetResultView<SubscriptionResultPage, Subscription>(subscriptionsUrl);
        }

        /// <summary>
        /// Adds the specified subscription.
        /// </summary>
        /// <param name="metricId">The metric identifier.</param>
        /// <param name="userId">The user identifier. If omitted, subscription is added to the current user.</param>
        /// <param name="subscription">The subscription.</param>
        /// <returns>The added subscription.</returns>
        public async Task<Subscription> AddSubscription(long metricId, long userId = 0, Subscription.Edit subscription = null)
        {
            var subscriptionUrl = string.Format(CultureInfo.InvariantCulture, "metrics/{0}/subscriptions/{1}", metricId, GetUserIdParameter(userId));

            var jsonSubscription = JsonConvert.SerializeObject(subscription, JSonSettings.SerializerSettings);
            var response = await context.PutWithRetry(subscriptionUrl, jsonSubscription);
            return await response.Content.ReadAsAsync<Subscription>(JSonSettings.Formatter);
        }

        /// <summary>
        /// Deletes the subscription.
        /// </summary>
        /// <param name="metricId">The metric identifier.</param>
        /// <param name="userId">The user identifier. If omitted, subscription owned by the current user is deleted.</param>
        /// <returns>The deletion task.</returns>
        public async Task DeleteSubscription(long metricId, long userId = 0)
        {
            var subscriptionUrl = string.Format(CultureInfo.InvariantCulture, "metrics/{0}/subscriptions/{1}", metricId, GetUserIdParameter(userId));
            await context.DeleteWithRetry(subscriptionUrl);
        }

        #endregion

        #region User

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="userId">The user identifier. If omitted, the current user is returned.</param>
        /// <returns>The user.</returns>
        public async Task<User> GetUser(long userId = 0)
        {
            var eventsUrl = string.Format(CultureInfo.InvariantCulture, "users/{0}", GetUserIdParameter(userId));

            var response = await context.GetWithRetry(eventsUrl);
            return await response.Content.ReadAsAsync<User>(JSonSettings.Formatter);
        }

        /// <summary>
        /// Gets the user image.
        /// </summary>
        /// <param name="userId">The user identifier. If omitted, the current user avatar is returned.</param>
        /// <returns>The user image.</returns>
        public async Task<Image> GetUserImage(long userId = 0)
        {
            var photoUrl = string.Format(CultureInfo.InvariantCulture, "users/{0}/photo", GetUserIdParameter(userId));
            var response = await context.GetWithRetry(photoUrl);

            var data = await response.Content.ReadAsByteArrayAsync();

            return new Image
            {
                MediaType = response.Content.Headers.ContentType.MediaType,
                Data = data
            };
        }

        /// <summary>
        /// Updates the current user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>The updated user.</returns>
        public async Task<User> UpdateUser(User.Edit user)
        {
            return await UpdateUser(0, user);
        }

        /// <summary>
        /// Updates the user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="user">The user.</param>
        /// <returns>The updated user.</returns>
        public async Task<User> UpdateUser(long userId, User.Edit user)
        {
            var existingUser = await GetUser(userId);
            var actualUserUpdate = user.DefaultTo(existingUser);

            var jsonMetric = JsonConvert.SerializeObject(actualUserUpdate, JSonSettings.SerializerSettings);

            var usersUrl = string.Format(CultureInfo.InvariantCulture, "users/{0}", GetUserIdParameter(userId));
            var response = await context.PutWithRetry(usersUrl, jsonMetric);
            return await response.Content.ReadAsAsync<User>(JSonSettings.Formatter);
        }

        /// <summary>
        /// Updates the current user avatar.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <returns>The updated user.</returns>
        public async Task<User> UpdateUserImage(Image.Edit image)
        {
            return await UpdateUserImage(0, image);
        }

        /// <summary>
        /// Updates the user avatar.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="image">The image.</param>
        /// <returns>The updated user.</returns>
        public async Task<User> UpdateUserImage(long userId, Image.Edit image)
        {
            var photoUrl = string.Format(CultureInfo.InvariantCulture, "users/{0}/photo", GetUserIdParameter(userId));
            var response = await context.PostWithRetry(photoUrl, image);
            return await response.Content.ReadAsAsync<User>(JSonSettings.Formatter);
        }

        /// <summary>
        /// Deletes the user avatar.
        /// </summary>
        /// <param name="userId">The user identifier. If omitted, avatar of the current user is deleted.</param>
        /// <returns>The deletion task.</returns>
        public async Task DeleteUserImage(long userId = 0)
        {
            var photoUrl = string.Format(CultureInfo.InvariantCulture, "users/{0}/photo", GetUserIdParameter(userId));
            await context.DeleteWithRetry(photoUrl);
        }

        #endregion

        #endregion

        #region Private Helpers

        private static object GetUserIdParameter(long userId)
        {
            return userId != 0 
                ? Convert.ToString(userId, CultureInfo.InvariantCulture) 
                : "me";
        }

        private async Task<ResultPage<TResult>> GetResultView<TPage, TResult>(string url) where TPage : IResultPage<TResult>
        {
            if (string.IsNullOrEmpty(url))
                return ResultPage<TResult>.Empty;

            var response = await context.GetWithRetry(url);
            var result = await response.Content.ReadAsAsync<TPage>(JSonSettings.Formatter);

            return new ResultPage<TResult>
            {
                Values = result.Values,
                HasMoreResults = !string.IsNullOrEmpty(result.NextUrl),
                NextEvaluator = () => GetResultView<TPage, TResult>(result.NextUrl)
            };
        }

        #endregion
    }
}