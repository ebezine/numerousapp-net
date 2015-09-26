#region References

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json;
using Numerous.Api.Helpers;

#endregion

namespace Numerous.Api
{
    public class NumerousClient : IDisposable
    {
        #region Fields

        private static readonly Uri baseUrl = new Uri("https://api.numerousapp.com/v2/");

        private readonly NumerousContext context;

        #endregion

        #region Instance Management

        public NumerousClient(string apiKey) : this(new NumerousSettings{ApiKey = apiKey}) {}

        public NumerousClient(NumerousSettings settings)
        {
            var credentials = new NetworkCredential(settings.ApiKey, string.Empty);
            var handler = new HttpClientHandler { Credentials = credentials };

            var client = new HttpClient(handler) { BaseAddress = baseUrl };
            context = new NumerousContext
            {
                Client = client,
                Settings = settings
            };
        }

        void IDisposable.Dispose()
        {
            context.Client.Dispose();
        }

        #endregion

        #region Methods

        public void Close()
        {
            ((IDisposable)this).Dispose();
        }

        #region Metrics

        public async Task<Metric> GetMetric(long metricId)
        {
            var metricsUrl = string.Format(CultureInfo.InvariantCulture, "metrics/{0}", metricId);
            var response = await context.GetWithRetry(metricsUrl);
            return await response.Content.ReadAsAsync<Metric>(JSonSettings.Formatter);
        }
        
        public async Task<ResultPage<Metric>> GetUserMetrics(long userId = 0)
        {
            var metricsUrl = string.Format(CultureInfo.InvariantCulture, "users/{0}/metrics", GetUserIdParameter(userId));
            return await GetResultView<MetricResultPage, Metric>(metricsUrl);
        }

        public async Task<ResultPage<Metric>> GetPopularMetrics(int count = 10)
        {
            var metricsUrl = string.Format(CultureInfo.InvariantCulture, "metrics/popular?count={0}", count);
            var response = await context.GetWithRetry(metricsUrl);
            
            var results = await response.Content.ReadAsAsync<IEnumerable<Metric>>(JSonSettings.Formatter);

            return ResultPage<Metric>.SinglePage(results);
        }

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
                
        public async Task<Metric> AddMetric(Metric.Edit metric)
        {
            var jsonMetric = JsonConvert.SerializeObject(metric, JSonSettings.SerializerSettings);

            var response = await context.PostWithRetry("metrics", jsonMetric);
            return await response.Content.ReadAsAsync<Metric>(JSonSettings.Formatter);
        }

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

        public async Task<Metric> UpdateMetricImage(long metricId, Image.Edit image)
        {
            var photoUrl = string.Format(CultureInfo.InvariantCulture, "metrics/{0}/photo", metricId);
            var response = await context.PostWithRetry(photoUrl, image);
            return await response.Content.ReadAsAsync<Metric>(JSonSettings.Formatter);
        }
        
        public async Task DeleteMetric(long metricId)
        {
            var metricUrl = string.Format(CultureInfo.InvariantCulture, "metrics/{0}", metricId);
            await context.DeleteWithRetry(metricUrl);
        }
        
        public async Task DeleteMetricImage(long metricId)
        {
            var metricUrl = string.Format(CultureInfo.InvariantCulture, "metrics/{0}/photo", metricId);
            await context.DeleteWithRetry(metricUrl);
        }

        #endregion

        #region Events

        public async Task<Event> GetEvent(long metricId, long eventId)
        {
            var eventsUrl = string.Format(CultureInfo.InvariantCulture, "metrics/{0}/events/{1}", metricId, eventId);

            var response = await context.GetWithRetry(eventsUrl);
            return await response.Content.ReadAsAsync<Event>(JSonSettings.Formatter);
        }

        public async Task<ResultPage<Event>> GetEvents(long metricId)
        {
            var eventsUrl = string.Format(CultureInfo.InvariantCulture, "metrics/{0}/events", metricId);
            return await GetResultView<EventResultPage, Event>(eventsUrl);
        }

        public async Task<Event> GetNearestEvent(long metricId, DateTime date)
        {
            var eventsUrl = string.Format(CultureInfo.InvariantCulture, "metrics/{0}/events?t={1}", metricId, date.ToString(JSonSettings.SerializerSettings.DateFormatString, CultureInfo.InvariantCulture));

            var response = await context.GetWithRetry(eventsUrl);
            return await response.Content.ReadAsAsync<Event>(JSonSettings.Formatter);
        }

        public async Task<Event> AddEvent(long metricId, Event.Edit evnt)
        {
            var eventsUrl = string.Format(CultureInfo.InvariantCulture, "metrics/{0}/events", metricId);

            var jsonEvent = JsonConvert.SerializeObject(evnt, JSonSettings.SerializerSettings);
            var response = await context.PostWithRetry(eventsUrl, jsonEvent);
            return await response.Content.ReadAsAsync<Event>(JSonSettings.Formatter);
        }

        public async Task DeleteEvent(long metricId, long eventId)
        {
            var eventsUrl = string.Format(CultureInfo.InvariantCulture, "metrics/{0}/events/{1}", metricId, eventId);
            await context.DeleteWithRetry(eventsUrl);
        }
        
        #endregion

        #region Subscriptions

        public async Task<Subscription> GetSubscription(long metricId, long userId = 0)
        {
            var subscriptionsUrl = string.Format(CultureInfo.InvariantCulture, "metrics/{0}/subscriptions/{1}", metricId, GetUserIdParameter(userId));
            var response = await context.GetWithRetry(subscriptionsUrl);
            return await response.Content.ReadAsAsync<Subscription>(JSonSettings.Formatter);
        }

        public async Task<ResultPage<Subscription>> GetUserSubscriptions(long userId = 0)
        {
            var subscriptionsUrl = string.Format(CultureInfo.InvariantCulture, "users/{0}/subscriptions", GetUserIdParameter(userId));
            return await GetResultView<SubscriptionResultPage, Subscription>(subscriptionsUrl);
        }

        public async Task<ResultPage<Subscription>> GetMetricSubscriptions(long metricId)
        {
            var subscriptionsUrl = string.Format(CultureInfo.InvariantCulture, "metrics/{0}/subscriptions", metricId);
            return await GetResultView<SubscriptionResultPage, Subscription>(subscriptionsUrl);
        }

        public async Task<Subscription> AddSubscription(long metricId, long userId = 0, Subscription.Edit subscription = null)
        {
            var subscriptionUrl = string.Format(CultureInfo.InvariantCulture, "metrics/{0}/subscriptions/{1}", metricId, GetUserIdParameter(userId));

            var jsonSubscription = JsonConvert.SerializeObject(subscription, JSonSettings.SerializerSettings);
            var response = await context.PutWithRetry(subscriptionUrl, jsonSubscription);
            return await response.Content.ReadAsAsync<Subscription>(JSonSettings.Formatter);
        }

        public async Task DeleteSubscription(long metricId, long userId = 0)
        {
            var subscriptionUrl = string.Format(CultureInfo.InvariantCulture, "metrics/{0}/subscriptions/{1}", metricId, GetUserIdParameter(userId));
            await context.DeleteWithRetry(subscriptionUrl);
        }

        #endregion

        #region User

        public async Task<User> GetUser(long userId = 0)
        {
            var eventsUrl = string.Format(CultureInfo.InvariantCulture, "users/{0}", GetUserIdParameter(userId));

            var response = await context.GetWithRetry(eventsUrl);
            return await response.Content.ReadAsAsync<User>(JSonSettings.Formatter);
        }

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

        public async Task<User> UpdateUser(User.Edit user)
        {
            return await UpdateUser(0, user);
        }

        public async Task<User> UpdateUser(long userId, User.Edit user)
        {
            var existingUser = await GetUser(userId);
            var actualUserUpdate = user.DefaultTo(existingUser);

            var jsonMetric = JsonConvert.SerializeObject(actualUserUpdate, JSonSettings.SerializerSettings);

            var usersUrl = string.Format(CultureInfo.InvariantCulture, "users/{0}", GetUserIdParameter(userId));
            var response = await context.PutWithRetry(usersUrl, jsonMetric);
            return await response.Content.ReadAsAsync<User>(JSonSettings.Formatter);
        }

        public async Task<User> UpdateUserImage(Image.Edit image)
        {
            return await UpdateUserImage(0, image);
        }

        public async Task<User> UpdateUserImage(long userId, Image.Edit image)
        {
            var photoUrl = string.Format(CultureInfo.InvariantCulture, "users/{0}/photo", GetUserIdParameter(userId));
            var response = await context.PostWithRetry(photoUrl, image);
            return await response.Content.ReadAsAsync<User>(JSonSettings.Formatter);
        }

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
            return userId != 0 ? XmlConvert.ToString(userId) : "me";
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