# Introduction

When I first started writing about mobile apps five years ago, the world was a different place.  Swift (for iOS) and Java (for Android) ruled the mobile development world. Windows Mobile was still a thing.  If you wanted to do cross-platform development, you had two choices. Xamarin allowed you to develop with the .NET framework, and Apache Cordova allowed you to do web development for a mobile form factor.  On the server side, the serverless revolution was in its infancy and most data transfer was done via REST or OData.  Backend developers tended to write monolithic web services to support the mobile ecosystem.

What has happened since then?

Let's first of all talk about the development of the front end.  We can ignore Windows as a platform now.  It is all but gone, leaving just iOS and Android as viable platforms.  The number of languages you can write in has exploded.  On the purely native side, you have a choice of Objective-C and Swift for iOS development and Java or Kotlin for Android development.  On the cross-platform side, you have Xamarin and C++ as before, but also a variety of JavaScript frameworks, including [React Native][1] and [NativeScript][2] that provide a bridge technology to native code, allowing you to leverage web development techniques but still produce a native app.  In addition, Google has released [Flutter][3], coded in [Dart][4].

On the backend side, most of the development is in serverless technologies.  Instead of paying continually for a web service instance, you code your application to use functions that are paid for per invocation.  Mobile developers take advantage of managed services where possible.  Mobile apps cre less about how the data is stored on the backend, preferring to use optimized data graphs provided over [GraphQL][5].

This book is one persons opinion, and it degrades over time.  Do not read this and consider it the last word on cross-platform mobile development with C#.  It can't be.  I hope that it can be a starting point for your own research.

## What are Cloud Connected Mobile Apps?

I guess I should define some of the terminology that I am going to use.  When I refer to a **mobile application** or **mobile app**, I mean every piece of software that is related to the application you want to use.  This includes, for example, the **mobile client**. This is the piece of code you run on your iPhone or Android phone.  It also includes the **mobile backend** which is the service that you run in the cloud to provide important services to your mobile client.

A **cloud connected mobile application** is a mobile client that connects to a mobile backend for shared services.  Quite a few of the apps on your phone are cloud connected already.  For example, Instagram uses the cloud for photo storage, and Facebook uses the cloud to store the news feeds of you and your friends.

## Why Cross-Platform Native Development is important?

It should come as no surprise that Apple and Google have pretty much won the mobile OS wars.  Over 90% of the smart phones being sold today run either iOS or Android.  However, these are two very different mobile operating systems, with different programming models.  iOS is based on either [Swift][6] or Objective-C.  Android is based on [Java][7] or [Kotlin][8].  If you want to develop for the 80% case (and you should), then you need to know both Swift and Java.  That's a tall order even for the most dedicated mobile developer.

However, there are alternatives out there.  Most notable, you can write your mobile application with one code-base and just write exceptions for when the platforms diverge.  You have to pick a single language and a tool set that supports cross-platform development to do this.  Not all cross-platform tool sets are created equal, however.  Some do not compile your code to native binaries, which means that you do not get access to all the functionality of the mobile platforms you are targeting.

[Xamarin][9], acquired by Microsoft, allows you to target all major platforms to gain greater than 95% coverage of the mobile smart phone market.  It does this by leveraging the .NET framework and compiling your code to a native binary for each platform.  [Xamarin Forms][10] is a cross-platform framework, based on XAML and .NET, that allows you to use common UI pages to develop your apps.

### Features of Cloud Connected Mobile Apps

A cloud connected mobile application will use one or more services in the following areas:

* Analytics
* Authentication
* Storage of structured data (like a task list)
* Storage of unstructured data (like photographs)
* Push notifications
* Invocation of Custom Code
* Crash Analytics

I am going to cover each of these in great detail.  In addition, I will also cover some common issues and solutions that developers run into while developing cloud connected mobile applications such as testing and going to production.

Aside from the actual features of mobile apps, there are other things to consider while developing your mobile application.  Here is my list, in no particular order:

* Scalability
* Security
* Continuous Deployment
* Blue/Green Deployments
* Development Deployments
* Beta Distributions
* Backups

The point here is that my intent is to write a production quality application. I need to be able to deploy my site with confidence without resorting to jumping through hoops.  I want to run multiple versions of the backend, so I can have zero down time when updating the site and roll back my production site to a previous version at a moments notice.  I want to be able to handle the load when my app is successful.  I want to ensure the security and privacy of customer data.

All of these capabilities are available within the Azure cloud using a variety of services.

## Who is This Book For?

This book is for intermediate to experienced C# developers who have already built a mobile app with Xamarin and want to take their mobile apps to the next level by utilizing cloud services.

This book is not for the beginner.  Explicitly, I already expect you to know how to develop mobile applications with C# and Xamarin technologies.  If you are unfamiliar with the C# language, you can get started with a free course on the Internet.  The basics of the language can be learned at [the Microsoft site][11]. Once you have the language basics under your belt, you can move on to building mobile applications with Xamarin. You can learn more about developing cross-platform mobile development with Xamarin at the [Microsoft][9] website and through training sites like [Pluralsight][12].  Although you do not need to understand ASP.NET to get value out of this book, be aware that the mobile back ends that I will be covering are written in C# and [ASP.NET Core][13].  A good understanding of .NET Core and ASP.NET Core will assist you.

### Things You Should Know!

Before you get started with development, spend some time learning the tools of the trade.  The command prompt on the Mac is [bash][14] and the command prompt on the PC is [PowerShell][15].  You should be proficient in the shell on the platforms that you use.

Additionally, you should become familiar with the source code control system that you will use.  For most, this means becoming familiar with [git][16].  Don't even think of developing without using source control.

## What You Will Need

The list of hardware and software for mobile development is longer than your typical development projects.  It is still, thankfully, relatively short and easy to acquire.

### Hardware

You will want a computer on which to develop code.  If you develop iOS applications, then you **MUST** have a Mac running the latest version of Mac OSX.  Android applications can be developed on either Windows or Mac.  It is no wonder, then, that the platform of choice for mobile development is a Mac.  My own experience has taught me that the tooling for developing mobile backends in C# and ASP.NET (our primary languages during the course of this book) are better on a PC running Windows 10.  Thus, my hardware choice is a rather beefy Windows 10 PC for my main development system.  In addition, I have a [Mac Mini][18] underneath my desk that I use to build the iOS portions of the applications.

### Software

All of the following software are freely available.  You should install each package and update it (if appropriate) so that it is fully patched.

#### On your Mac

* [XCode][19] (available on the Mac App Store)
* [Visual Studio for Mac][20]
* [Android Studio and Tools][21]
* [.NET Core 2.2][17]

You must run XCode at least once after installation so that you can accept the license agreement. 

#### On your Windows PC

* [Android Studio and Tools][22]
* [Visual Studio 2019][23]
* [.NET Core 2.2][17]

I use Visual Studio 2019 Enterprise for my development, but there is no difference between the editions for the individual developer.  Pick whichever edition of Visual Studio you like.  When installing Visual Studio, you will want to install the following workloads:

* Universal Windows Platform development.
* ASP.NET and web development.
* Azure development.
* Data storage and processing.
* Mobile development with .NET.

If you have already installed Visual Studio and did not install these components, run the installer again to add the components.  You can also use earlier versions of Visual Studio.  If you do use an earlier version, then the screen shots I provide will not match up.  

!!! tip
    Development Tools are big, multi-gigabyte installers.  If you are on a slow or restricted link, you may want to download the installers onto a thumb drive for local installation.

### Cloud Services

You will need an Azure account to complete most of the tutorials in this book.  In fact, you won't be able to get very far without one. If you have an MSDN account, you already have access to free Azure resources.  You just need to log into your [MSDN account][25] and activate your Azure benefit. If you don't have MSDN, then there is a [free trial][27] available. Once the trial period ends, you can move to a Pay-As-You-Go account and continue to use free services without incurring a charge.  There are other ways to get access to Azure for free, including educational and startup credits. I'll point out when you are going to incur charges on your Azure account, but I will be using free resources most of the time.

Aside from Azure resources, you will want some place to store your code.  This doesn't have to be in the cloud.  If you want to use the cloud, you can use GitHub or Visual Studio Team Services.  Both are free to use.  Visual Studio Team Services also includes other services that I will talk about during the course of the book, some of which may incur cost.  I will be publishing all my samples and tutorial code on GitHub so that you can easily download it.  You don't have to use one of these resources, but I won't be covering other service usage.

You will need a **Developer Account** for the appropriate app store if you intend to distribute your mobile clients or if you intend to use specific cloud services (such as push notifications).  I will point out when you need these accounts for development.

Now, [let's get developing][ch1]!

<!-- Link References -->
[1]: https://facebook.github.io/react-native/
[2]: https://www.nativescript.org/
[3]: https://flutter.io/
[4]: https://www.dartlang.org/
[5]: https://graphql.org/
[6]: https://swift.org/
[7]: https://developer.android.com/training/index.html
[8]: https://kotlinlang.org/
[9]: https://docs.microsoft.com/en-us/xamarin/#pivot=platforms&panel=Cross-Platform
[10]: https://docs.microsoft.com/en-us/xamarin/#pivot=platforms&panel=XamarinForms
[11]: https://docs.microsoft.com/en-us/dotnet/csharp/
[12]: https://www.pluralsight.com/browse/software-development/c-sharp
[13]: https://docs.microsoft.com/en-us/aspnet/core/?view=aspnetcore-2.2
[14]: http://guide.bash.academy/
[15]: https://mva.microsoft.com/en-us/training-courses/getting-started-with-powershell-3-0-jump-start-8276
[16]: http://try.github.io/
[17]: https://dotnet.microsoft.com/download/dotnet-core/2.2
[18]: https://www.apple.com/mac-mini/
[19]: https://developer.apple.com/xcode/
[20]: https://visualstudio.microsoft.com/vs/mac/
[21]: https://developer.android.com/studio/
[22]: https://developer.android.com/studio/
[23]: https://visualstudio.microsoft.com/downloads/
[25]: https://msdn.microsoft.com/en-us/
[27]: https://azure.microsoft.com/en-us/free/

[ch1]: chapter1/index.md
