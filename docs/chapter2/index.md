# Deployment

When you have finished your mobile backend, you will want to deploy it to the cloud to run it.  So why are we considering deployment first, before we even start touching code?  Deployment matters because how you intend to deploy your code changes the way you develop your backend.  Let's first take a look at some of the considerations you need to think about when you think about a mobile backend, and then look at the options available to you in the light of those considerations.  

## What to consider

When considering deployment options, here is what you need to consider:

* Security, particularly underlying operating system patches
* Availability of your mobile backend
* Scalability of your mobile backend
* The failure model

Let's take each of these in turn.

### Security

Every single system on the Internet is built in the same way.  You have the physical layer - a computer with memory and local disk, connected to the network and some sort of the network storage.  On top of this is a virtualization layer, followed by the operating system, any critical services you need to run the service you really care about, and then your application.

How much of this do you want to manage?  You care about it all functioning, of course.  But how much do you really want to deal with?  If you are like me, the answer is "as little as possible and ideally just my application".  You want the rest of it to "just work".  Of course, that may be asking a lot and there is always a trade off between how much control you have over the environment and how much management you must do.

So, how does this matter when it comes to security?  Every day, many security patches come out - for the operating system, web servers, database services, and so on.  The more of the stack you control, the more you have to care about security.  However, security patching does not add to your product.  It takes away from time you could be developing new features or fixing bugs in your application code.

One option is to delegate as much as possible to the cloud provider.  However, doing this comes with its own set of drawbacks.  You no longer have control over when and which security patches are applied.  How much you care about this is, of course, dependent on how much you want to manage.

### Availability

Some services run all the time, and thus are available all the time.  You can architect a cloud service with rolling deployments so that there is no single time when the service is not available.  Other services run on demand.  There is almost always a startup cost associated with spinning up a service so that it is ready to answer queries.  The most notable startup cost is the amount of time it takes to connect to the database.  This is passed on to your users in the form of application latency.

The flip side of availability is cost.  In the cloud, you are billed for every second your backend is running, irrespective of whether it is handling requests or not.  If your service is available all the time, you are paying for that availability all the time.  If your service starts and stops on demand, then you pay less money to run the service, but have a greater risk of latency issues.

If you architect your application well (using deferred authentication and local caching), then you can alleviate most of the latency issues.  However, you should understand what, if any, concerns you may have when using on demand vs. always on services.

### Scalability

How busy will your service be?  That's an impossible question to answer.  Sometimes, you know that your app will run from 8am-6pm during the week and be accessed by only the employees of your company.  This is the profile of a typical enterprise application.  Other times, you are writing a consumer application and hope that your app will be successful - leading to millions of users.   How do you determine the right scaling options for your app?  If your app is successful but you haven't planned for enough resources, your service will suffer latency and failed connections.  If your app is not successful and you had planned for capacity to support many more users, you are paying extra money for the service to be idle.

### Failure model

What happens when a node in your service goes down?  A few users will get failed requests, which may pop up annoying alerts that the user can't do anything about.  What about if a country connection goes down?  How about if a user moves between wifi and LTE coverage?  What about if they live in Africa and don't even have access to GPRS speeds?  In short, how do you deal with two failure modes: a failure to connect to the cloud and a speed (or latency) issue? 

The answers to these questions drive the expectations of the service and the expections of the users.  You have to plan for failure.  Quite how you do that will drive how you think about your backend service.

## Deployment Options

Let's look at the four options you could use:

### Virtual Machines

### Azure Container Service

### Azure App Service

### Azure Functions

## What do I use?

This is the ultimate "it depends" answer.  If I am running a service that requires access to a service that has a long startup cost (like Azure SQL) or I am running a service whose response may take a long time, then I will create an ASP.NET Core based service and deploy it on Azure App Service.  I try and architect my service so that I am not dependent on long-running operations.  I use Azure Functions when possible to minimize costs yet gain the maximum scalability and availability from my service.

Let's take a look at what a typical deployment pipeline looks like by [transitioning our REST backend from Chapter 1 to Azure Functions](./functions.md).

