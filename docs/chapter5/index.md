# Accessing Data

We are now going to move on to accessing data from the cloud.  There are generally three techniques you can use to access data from the cloud:

* [RESTful services](rest.md)
* [OData services](odata.md)
* [GraphQL services](graphql.md)

RESTful services allow you to use standard HTTP techniques (like HTTP GET and POST) to access the data within a data source.  [OData](https://www.odata.org/) is generally for SQL data and allows you to do advanced queries against the data.  [GraphQL](https://graphql.org/) is a relative newcomer to the data scene and is specifically optimized for graph queries against structured data in a client setting (like a mobile app).  

In addition, there are multiple data stores you might want to look at.  For example, you may be looking at an Azure SQL database, or a Cosmos document database (also known as NoSQL), or a more esoteric database such as [Cassandra](https://cassandra.apache.org/) (for big data applications), [Redis](https://redis.io/) (an in-memory data structure store primarily for caching), [Apache Tinkerpop](https://tinkerpop.apache.org/) (for graph database), or [Etcd](https://github.com/etcd-io/etcd) (a distributed key-value store popular in Kubernetes).

In addition to these data stores that you control, you may also want to access data stores on the Internet.  These are generally provided via a RESTful service.  For an example, see [OpenWeatherMap](https://openweathermap.org/api).  These can be used by your app for supplementing your own data stores.  Some services have free versions that are limited, so you can get started developing quickly.
