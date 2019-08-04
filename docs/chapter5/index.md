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

* The [OData](odata.md) implementation.
* The [GraphQL](graphql.md) implementation.
