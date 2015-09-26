# Numerous.NET

Numerous follows the most important numbers in your life and keeps them up to date, all in one place. 

----------

Numerous.NET is an unofficial wrapper for [Numerous App](http://numerousapp.com/) [API](https://developer.numerousapp.com/).
Numerous.NET is based on .NET 4.5 framework.

## Getting Started

Create an instance of the `NumerousClient` class, providing your Numerous API key (you can find your API key in the Numerous mobile apps under Settings > Developer Info).
`NumerousClient` implements `IDisposable` interface. You may use the `using` construct or use the `Close` method to release resources associated with the client.

```
using Numerous.Api;

var apiKey = "nmrs_123456789012"; // Replace with your Numerous API key

using (var client = new NumerousClient(apiKey))
{
	var currentUser = await client.GetUser();

	[...]
}

```

Numerous.NET is designed to work in asynchronous mode : methods that use the `NumerousClient` object must be marked with the `async` modifier. In order to use it in a synchronous fashion, use the `AsSync` extensions methods, as follow:

```
using Numerous.Api.Sync;

[...]

var currentUser = client.GetUser().AsSync();

[...]

```

## Features
`NumerousClient` supports the following operations :

### Users
`userId` parameter is always optional on methods involving users : if omitted (or set to `0`, operation apply to the current user).

 - **GetUser** : Retrieve information about a user
 - **UpdateUser** : Update profile of a user
 - **GetUserImage** : Retrieve the photo of a user
 - **UpdateUserImage** : Update profile photo of a user
 - **DeleteUserImage** : Clear profile photo of a user

### Metrics
 - **GetMetric** : Retrieve information about a metric
 - **GetUserMetrics **: Retrieve information about metrics owned by a user
 - **GetPopularMetrics** : Retrieve information about popular public metrics
 - **AddMetric** : Create a new metric
 - **UpdateMetric** : Update a metric configuration
 - **DeleteMetric** : Delete a metric
 - **GetMetricImage** : Retrieve the image of a metric
 - **UpdateMetricImage** : Update metric image
 - **DeleteMetricImage** : Clear metric image

### Subscriptions
 - **GetSubscription** : Retrieve information about a subscription
 - **GetUserSubscriptions** : Retrieve subscriptions made by a user
 - **GetMetricSubscriptions** : Retrieve subscriptions made to a metric
 - **AddSubscription** : Add a subscription for a user to a metric
 - **UpdateSubscription** : Update the subscription made by a user to a metric
 - **DeleteSubscription** : Remove the subscription made by a user to a metric

### Events
 - **GetMetricEvent** : Get event details 
 - **GetMetricEvents** : Retrieve events for a metric
 - **GetMetricEventAt** : Retrieve the nearest event to a date
 - **AddMetricEvent** : Add new event
 - **DeleteMetricEvent** : Delete an event

## Implementation Notes

### Paging
**Paging is currently not supported by Numerous.NET. Only the first page of results is returned by `NumerousClient`.**

### Quota Handling
Numerous API calls are limited to a quota of 300 calls/minute. 

When quota is exceeded, Numerous.NET waits for the required amount of time before further attempt.

Quota handling is transparent for the `NumerousClient` caller.

### Error Handling
Under certain conditions, `NumerousClient` transparently make further attempts to the Numerous API.

This occur when HTTP status code returned by API is one of the following:
 - **408** : Request Timeout
 - **500** : Internal Server Error
 - **502** : Bad Gateway
 - **503** : Service Unavailable
 - **504** : Gateway Timeout

Interval between successive attempts is of 30 seconds.

**There is currently no limit in the number of successive attempts.**
