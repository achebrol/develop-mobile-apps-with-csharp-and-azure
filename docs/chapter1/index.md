# Build a cloud app with Xamarin.Forms

In this chapter, we are going to connect an existing mobile app to a backend service.  Writing Xamarin code with Xamarin.Forms and Visual Studio is covered by several other tutorials and books, I'm using [an existing sample app][sample].  There are two reasons to go through this tutorial:

1. It shows that you have got your development environment set up correctly.
2. It provides a good road map for a complete beginner developer.

Since we establish that you have your development environment set up correctly,  I recommend you go through the tutorial code even if you are an experienced developer.

The sample I've chosen for this tutorial is a simple Todo app.  In its starting form, it uses a local SQLite database.  We will change that so that the data is read from a remote service instead of the local database.  Since we can write code on both a PC and a Mac, we are going to walk through the basics on both platforms.  Choose the platform that you want to write your code on:

* [Windows 10 on a PC](windows-intro.md)
* [Mac OSX on a Mac](mac-intro.md)

Which one should you choose if you have access to both?  Well, it depends on whether you prefer to operate on a PC or a Mac.  Personally, I am more productive on a PC since I use a PC for my day-to-day work.  I also find that extensions are more readily available for Visual Studio 2019 on a PC, allowing me increased productivity within Visual Studio.  However, if you use a Mac for your day-to-day work, or happen to prefer it when developing, use Visual Studio for Mac instead.  It's much more important that you be comfortable using your chosen environment.

<!-- Links -->
[sample]: https://github.com/xamarin/xamarin-forms-samples/tree/master/Todo