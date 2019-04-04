# Build a Mobile App on Windows

!!! tip "Enable Android Emulator on Hyper-V"
    Before getting started with development on the front end, you may want to set up an Android emulator.  This requires [setting up Hyper-V on your Windows system][1].  Since this will likely require a reboot of your system, it's best to configure Hyper-V ahead of time.

Now that the mobile backend is created, we can move to the client side of things.  If you have previously closed your project, re-open the project in Visual Studio now.  The first step is to add a new project to the solution that will represent our mobile app.

> **One solution or two?**  Code organization is important and it generates a lot of opinions.  Some people like to keep the backend separate from the frontend code (two solutions), while others like to keep everything together (one solution).  There are pros and cons for each method, which you will need to discover for yourself.  For the purposes of code organization within this book, I always keep the backend with the frontend code in one solution.  You can find solutions for all the projects within this book on the [Github project](https://github.com/adrianhall/develop-mobile-apps-with-csharp-and-azure).

1. In the solution explorer, right-click the solution and select **Add** > **New Project...**.
2. In the search box, enter _Mobile_, then press Enter.  Select the **Mobile App (Xamarin.Forms)** project template, then click **Next**.

    ![](./img/frontend-pc-image1.png)


3. Give the project a good name (like _Frontend_), then click **Create**.

    ![](./img/frontend-pc-image2.png)

4. In the **New Cross Platform App** window, select **Blank** and then click **OK**
    * Select **Blank**.
    * Ensure **Shared Code** is selected as the code sharing strategy.
    * Click **OK**.

    ![](./img/frontend-pc-image3.png)

Visual Studio will now scaffold three projects: a shared project where your business logic is stored, plus a platform-specific project for each platform you selected (in this case, iOS and Android).  

![](./img/frontend-pc-image4.png)

Visual Studio allows you to build iOS applications directly from Visual Studio using a Mac.  I've got a Mac Mini under my desk for this purpose. We'll work on setting up the mac and compiling the iOS application later.  For now, let's continue with just the Android app.  Before you begin, set the **Frontend.Android** project as the startup project (right-click on the project and select **Set as StartUp Project**).  Then click the **Android Emulator** button to start an emulator.  If this is the first time you have created an Android app with Visual Studio, it will walk you through creating an emulated device.  Click **Start** to start the emulator you just created.  

!!! tip "Keep the emulator running"
    While you are developing your app, keep the emulator running.  You will skip the device startup delay (which can be considerable) when you do this.

Now go back to Visual Studio.  The run button will now be displayed as the device you have started:

![](./img/frontend-pc-image5.png)

Click the run button to start your app.  The app will be compiled, built into an APK, signed, and deployed to your emulated device.  Eventually, the app will display a nice screen with "Welcome to Xamarin Forms!" on it.  This shows that your build and deployment environment for development work is working.  Click the **stop** button within Visual Studio to shut the app down.

## Design your app

The application we are going to build together is a simple task list.  The mobile client will have three screens - an entry screen, a task list, and a task details page.  I have mocked these pages up using [MockingBot][2].

!!! tip
    Mocking your screens before you start coding is a great habit to get into.  There are some great tools available including free tools like [MockingBot][3].  Doing mockups before you start coding is a good way to prevent wasted time later on.

![](./img/frontend-mockingbot.png)

!!! tip 
    If you are using iOS, then you may want to remove the back button as the style guides suggest you don't need one.  Other platforms will need it though, so it's best to start with the least common denominator.

My ideas for this app include:

* Tapping on a task title in the task list will bring up the details page.
* Toggling the completed link in the task list will set (or clear) the completed flag.
* Tapping the spinner will initiate a network refresh.

Now that the client screens are planned out, we can move onto coding.

## Build the common library

There are generally two parts to any cloud-connected app.  The first is the connection to the cloud, and the second is the UI that users of the app will interact with.  I like to put as much as possible in the common library.  Xamarin.Forms apps can share up to 90% of their code, which saves valuable time.  Let's start with the cloud service.

I use interfaces for almost everything.  An interface allows me to abstract the concrete implementation of something so that I can mock it for testing purposes later on.  This isolates the code changes to just the concrete implementation - anything else within the app will use the interface.  Consider an interface as a contract between the concrete implementation and the rest of your app.  In this case, I want two different interfaces:

* `ICloudServiceClient` represents the connection to the Azure Cloud.
* `IDataTable<T>` represents the CRUDL operations on a specific table.

The `ICloudServiceClient` is the first one and it is fairly simple:

```csharp
namespace Frontend.Services
{
    public interface ICloudServiceClient
    {
        IDataTable<T> GetTable<T>() where T : TableData;
    }
}
```

Any operation that deals with the Azure cloud service will be represented here.  In this case, we have one operation - dealing with a table.  That returns `IDataTable<T>`:

```csharp
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.Services
{
    public interface IDataTable<T> where T : TableData
    {
        Task<T> CreateItemAsync(T item);
        Task<T> ReadItemAsync(string id);
        Task<T> UpdateItemAsync(T item);
        Task DeleteItemAsync(T item);
        Task<ICollection<T>> ReadAllItemsAsync();
    }
}
```

This is a generic that takes the model of the data.  The model must conform to the `TableData` shape:

```csharp
namespace Frontend.Services
{
    public abstract class TableData
    {
        public string Id { get; set; }
    }
}
```

Taken together, our cloud service is a collection of tables, where each table allows me to perform CRUDL operations.  The model for the data within each table must have a string `Id` field.  The model actually looks like this:

```csharp
using Frontend.Services;

namespace Frontend.Models
{
    public class TodoItem : TableData
    {
        public string Title { get; set; }
        public bool IsComplete { get; set; }
    }
}
```

Note that this matches what we wrote in the backend, but the `Id` field is moved to the `TableData` class.  It is especially important that the model name matches the controller name in the backend.  If the model in the client is named `X`, then you should have a controller called `XsController` that listens on `/api/xs` over HTTP.

!!! tip Share your models, but be careful
    You can share your models between the frontend and backend code.  However, you must be careful.  All the classes necessary to implement the models must be placed in a shared project.  In addition, you need to make sure that the data is completely duplicated between the frontend and backend.  It is normal to have "extras" in the backend.  For example, you may have an extra deleted flag in the backend that is not exposed in the frontend code.

We can now move to the concrete implementations of the `ICloudServiceClient` and `ITableData` interfaces.  The `AzureCloudServiceClient` class is my concrete reprensentation of an `ICloudServiceClient`.  




[1]: https://docs.microsoft.com/en-us/xamarin/android/get-started/installation/android-emulator/hardware-acceleration?tabs=vswin&pivots=windows#hyper-v
[2]: https://mockingbot.com/app/RQe0vlW0Hs8SchvHQ6d2W8995XNe8jK#screen=s8BD92432F11467855027824
[3]: https://mockingbot.com/

