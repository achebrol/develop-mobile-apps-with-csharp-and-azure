# A simple REST backend

In this chapter, we are going to build and test a complete mobile app.  The app that we are going to build is a task list app.  This is one of several apps I recommend that a new mobile developer tackle because it covers many of the basics that you require without being overly complex.  We will be expanding the functionality of this task list over time, but for the purposes of this tutorial, we will be making the functionality as simple as possible.

How simple?

We will implement a highly scalable REST service in Microsoft Azure to store the tasks in our list.  This will include the following endpoints:

* `GET /tasks` will retrieve all the tasks.
* `GET /tasks/{id}` will retrieve a specific task.
* `POST /tasks` will store a new task.
* `POST /tasks/{id}` will update a task with new information.
* `DELETE /tasks/{id}` will delete a task.

The data will be transferred in JSON format.  For services, we will use Azure App Service to run our web API.  We will use an in-memory database to store the data.  This means that once we shut down our app, our data will disappear along with it.

This chapter is reasonably split into two sections.  First, we will develop the backend code and deploy it to Azure, then we will write the front end code and test it by connecting to the backend.

To start, select the platform you are writing your code on:

* [Windows](./windows-backend.md)
* [Mac](./mac-backend.md)

There are two reasons to go through this tutorial:

1. It shows that you have got your development environment set up correctly.
2. It provides a good road map for a complete beginner developer.

Since we establish that you have your development environment set up correctly,  I recommend you go through the tutorial code even if you are an experienced developer.
