# Accessing Data

We are now going to move on to accessing data from the cloud.  There are generally three techniques you can use to access data from the cloud:

* [RESTful services](rest.md)
* [OData services](odata.md)
* [GraphQL services](graphql.md)

RESTful services allow you to use standard HTTP techniques (like HTTP GET and POST) to access the data within a data source.  [OData](https://www.odata.org/) is generally for SQL data and allows you to do advanced queries against the data.  [GraphQL](https://graphql.org/) is a relative newcomer to the data scene and is specifically optimized for graph queries against structured data in a client setting (like a mobile app).  

In addition, there are multiple data stores you might want to look at.  For example, you may be looking at an Azure SQL database, or a Cosmos document database (also known as NoSQL), or a more esoteric database such as [Cassandra](https://cassandra.apache.org/) (for big data applications), [Redis](https://redis.io/) (an in-memory data structure store primarily for caching), [Apache Tinkerpop](https://tinkerpop.apache.org/) (for graph database), or [Etcd](https://github.com/etcd-io/etcd) (a distributed key-value store popular in Kubernetes).

In addition to these data stores that you control, you may also want to access data stores on the Internet.  These are generally provided via a RESTful service.  For an example, see [OpenWeatherMap](https://openweathermap.org/api).  These can be used by your app for supplementing your own data stores.  Some services have free versions that are limited, so you can get started developing quickly.

## OData or GraphQL for data services?

There are several competing data protocols for accessing and updating data in databases that you own.  Among the more popular are [OData](https://www.odata.org/) and [GraphQL](https://graphql.org/).  OData is an OASIS standard, originally conceived by Microsoft.  GraphQL is a relative newcomer, originally conceived by Facebook.  Both protocols allow you to limit the amount of data that you retrieve (the so-called "over-fetching" problem typical in REST based protocols).  However, OData makes specifying the fields you want optional and returns everything by default.  GraphQL requires you to specify the fields you need. This requirement means you are immune from model creep and versioning requirements - something you have to consider with OData.

The OData standard has built-in primitives for filtering, paging through data, and sorting data.  GraphQL does not have those primitives, so you have to build it yourself.  However, the OData protocol is bad at dealing with multiple data tables.  GraphQL makes dealing with multiple data tables easier (even if the data tables happen to be on disparate data stores).

GraphQL uses a mechanism (called resolvers) whereby you can easily connect disparate data stores (they don't even have to be of the same type!).  OData, by contrast, is heavily influenced by SQL and it is hard to use OData in other data stores or for more than one data store.

OData is more mature, which means there are client and server-side libraries for .NET that are robust.  However, none of them do caching or offline built-in.  On the GraphQL side of things, client libraries are a bit sparse.  The thinking is that the protocol is so simple, a `HttpClient` can be used.  This thinking is a little limited since the query language is not as straight-forward as one would think.  As with OData, caching and offline are not built-in.

Is caching and offline access a requirement?  For a mobile app, almost certainly.  That means you are going to have to build it yourself.  As soon as you are dealing with caching and offline access, you must be careful in designing your database schema.

It's important to separate the data stores from your mobile app.  This allows you to change data stores without changing your mobile app.  It also allows you to easily separate concerns (in this case, separating UI from data storage).  It's also important to deal with caching and offline access, irrespective of the wire protocol you use.  We'll look at both protocols, and their implementation.  I lean towards using GraphQL these days when developing my own apps, but that is really a personal preference.

## Mobile Ready Data Models

Handling database tables in a mobile context means you have to change some things about your models.  In a mobile context, two mobile devices may want to create a new record at the same time, or update the same record at the same time.  If you are in a non-mobile context, you let the database be the source of truth and all queries hit the database.  In a mobile context, that is not possible.  Mobile apps tend to run with a certain amount of offline data or caching because of slow links.  Data can also become stale because there is no network.  As a result, there are some rules you need to follow when designing your models.

### Rule 1: The ID is always a GUID

It's common to use an auto-incrementing integer for a unique ID.  When you are thinking about an environment where the database is the source of truth and all creations are done online, this is perfectly reasonable.  Sadly, this is not the environment mobile devices live in.  As a result, the mobile device normally creates the ID for the record.  Using a GUID for the ID ensures uniqueness across records, even if the mobile device is not connected to the network at the time of creation.

### Rule 2: Deletions are soft

In a similar vein, you have to account for disconnected devices when you delete records.  Normally, this involves adding a `deleted` boolean type to each record.  Records are not actually deleted immediately.  Instead, they are marked for deletion and kept around in the deleted state for a number of days.

This does have another side effect.  If your mobile app connects to the database after the number of days between "real deletes", then it will need to do a resync of all the data.  For example, let's say you "really delete" all records that have been deleted for 7 days.  If your mobile device is offline for more than 7 days, the app won't be able to tell if a record is deleted or not.  In this case, the data for the table must be thrown out of the offline cache and a full synchronization must happen.

### Rule 3: Record the last updated time

Finally, if you are intending on using an offline cache, you really need to do incremental synchronizations.  This is when your mobile app asks the backend for only the records that have changed since the last update.  In order to do that, you need to record the last update time with as granular a precision as possible.  Ideally, you should be able to store the last "update stamp" within your offline cache, and then query for "greater than the last update stamp" to get all the latest records.  

In an ideal situation, arrange for the following:

* The record ID is created by the service only if the app has not already set it.
* The last update time is maintained solely by the service and cannot be set by the app.
* Most online queries return only non-deleted records.  Deleted records are returned only if requested.

All three rules can be met by either GraphQL or OData protocols, so offline concerns should not be factored in to the decision process when deciding on a protocol to use.

## Offline First or Offline Maybe?

When your app wants data, does it use the offline cache first (offline-first), or does it use the network resources first (offline-maybe)?  This is an important decision in your app and changes the architecture you use within the mobile app.

If you use "offline-first", then all queries go against the offline cache.  Data that has not been synchronized yet is unavailable.  You get the ability to do random searches, sorting, and paging through in-built capabilities of the mobile platform and it will be as performant as required, but at the expense of the freshness of the data.  You can alleviate the freshness problem by using a real-time channel to be notified of new requests, and automatically doing a synchronization when coming online (either through an app restart or because the network came up).  Offline-first backends don't need to support a significant wire protocol, so you can just do a basic REST backend and ignore the complexities of implementing GraphQL or OData if you choose.

If you use "offline-maybe", then all queries go against the network and you only query the cache for records when you are offline.  This is more complex code and you lose the ability to do random searches, sorting, and paging if it isn't supported by the backend (making your backend more complex as well).  The network utilization and backend cost goes up since you will be using the network (and backend) more often.  The data is the freshest it can be.  You can alleviate the network and backend costs by caching records.  Your backend queries will need to be "cache aware" to avoid a second query as well.  Without a cache-aware query, you will ask for just the ID and updated time, then check the data against the cache.  

I'm a big fan of offline-first apps.  They appear to be more responsive to the user, which gives your user a better experience.  They simplify your backend queries, allowing you to minimize costs of the backend.  The freshness of the data can be ensured by using a well-known pattern through the utilization of a real-time channel.

This book only considers offline-first apps.  However, you may want to expand your own knowledge of the chosen wire-protocol if you also provide a companion web application.  Web applications are, in general, not offline-aware and hit the backend for every request.  In most cases, you can provide a single backend for both the web application and the mobile app by providing different queries for each use case.

## Serverless or Serverful

The final consideration for your app is whether to make it serverless or to use some sort of server. 

With a serverless model:

* You write less code.
* Your backend only runs when you needed, saving you money.
* You don't have to deal with scaling your backend (but you have to scale the data store).
* You have a "cold-start" issue (more on that later).

With an App Service model:

* You write full ASP.NET Core applications, so more code.
* Your backend runs all the time, which may cost you more money.
* You have to deal with scaling your backend and your data store.
* You don't have a "cold-start" issue.

What is "cold-start".  When a typical app starts, it first needs to load all the dependencies and then establish connections to the backend data store.  This takes time and it's time during which your application cannot be handling new requests that come in.  It's normally described as having an impact on the first request, but really it's having an effect on every request during this start up phase.

All serverless applications suffer from "cold-start" right now.  The cloud providers keep the underlying resources that are running your serverless function around for a while (anywhere from 5 to 30 minutes), but eventually, the cloud provider will stop your function and require a new cold start.  This isn't the only time a cold-start is required.  If the scaling requirements show a need for starting an additional instance of the runtime to handle load, then this is done in the background and you don't know about it.  However, the new instance also requires a cold-start.  This is less noticeable since there are other instances able to handle the request.

App services (or containers) do not suffer from this problem since they are running all the time (and you are paying for that running time irrespective of whether they are handling requests or not).

The worst offenders for cold-start are those that either have a lot of dependencies (in our case, DLL files or .NET assemblies) that need to be loaded, or apps that have a persistent connection requirement such as a SQL service connection.

If you are doing "offline-first" development, the user experience is not impacted by cold-start.  For this reason, I don't worry about the cold-start problem in terms of dependencies.  I do, however, worry about it from a connection point of view.  If the service needs a persistent connection, I write the backend as an ASP.NET Core application and run it in Azure App Service.  If the service is connectionless (for example, allowing HTTP access), then I write the backend as a serverless application and run it on Azure Functions.

!!! tip "Avoid cold-start with Azure Functions"
    Azure Functions has a number of facilities that enable you to avoid cold-start, including non-consumption based [App Service Plans](https://docs.microsoft.com/en-us/azure/app-service/overview-hosting-plans).  This has a different pricing model and works around the cold start issue by establishing your function in an always-on service.  This allows you to write your code in a single architecture, but get the capabilities of the app service.
    
## Next steps

You may have already formulated an opinion as to which data protocol to use.  If you have, jump to the chosen protocol:

* [The OData Implementation](odata.md)
* [The GraphQL Implementation](graphql.md)

If you haven't decided, try both!  Start with [the OData implementation](odata.md), but be aware that the final application will use [the GraphQL implementation](graphql.md).  