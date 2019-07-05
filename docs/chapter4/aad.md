# Integrating with Azure Active Directory

Every single Azure account has an Azure Active Directory pre-configured.  This normally holds one account - the one you use to access the Azure Portal.  However, it can be used for other things.  In this walk-through, we are going to:

* Add a user to the existing Azure AD domain.
* Add an app registration to the domain.
* Write some code in our mobile app to sign us in.
* Take a look at the access token.

## Create a new user

Let's start on the Azure Portal.

* Select **All services**, then search for and select **Azure Active Directory**.
* Note your domain on the **Overview** screen.  Mine is `photoadrianoutlook.onmicrosoft.com`.
* Click **Users**.
* Click **New user**.
* Enter the following:
  * Name: "Damien Test"
  * User name: "damien@photoadrianoutlook.onmicrosoft.com"
  * Groups: Select "Users"
* Click **Create**.

Make sure you replace the domain in the user name field with your own domain.  This will create a user account that you can use to test your authentication test.  Make sure you click on "Show password" to show the temporary password that is assigned.

## Create an app registration

Now, let's create an app registration for our mobile app:

* Go back to the Overview of your Azure Active Directory.
* Click **App registrations**.
* Click **New registration**.
* Enter a friendly name (like `Tailwind Photos for Xamarin`).
* Click **Register**.

    ![](img/aad.png)

* Click **Add a Redirect URI**.
* For type, select **Public client (mobile & desktop)**.
* For Redirect URI, enter `msal-tailwind-photos://auth`.
* Click **Save**.

The redirect URI must be globally unique and follow the form given.  The Azure Active Directory team suggests using `msal{ClientID}://auth` as the redirect URI.  I prefer something a little more readable.

At this point, you need to make a record of three things:

1. The Application (client) ID.
2. The Directory (tenant) ID.
3. The Redirect URI you just configured.

These are all available in the application registration overview.  

## Integrate into the mobile app

## Run the app

## Decode the access token

## Next steps
