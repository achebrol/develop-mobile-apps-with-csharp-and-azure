# Integrating social providers

One of the major security improvements you can make to your app is to delegate authentication to a trusted third party.  Instead of a new username and password to remember, the user uses one of their existing logins to authenticate to your app.  Social providers such as Facebook, Amazon, and Google are ubiquitous in this arena.  Linking to social providers also gives you the ability to use those social providers.  For example, you may want to automatically allow Facebook friends to view your photos, or post a photo to the Facebook news feed.

Ultimately, the social provider is using [OpenID Connect](https://openid.net/connect/) to do the authentication and obtain an ID token for the social provider.  This ID token is an assertion of who you are.  You can then pass that token to your service to get an access token for your resources.  In this section, we will look at the process of integrating two distinct social providers - Facebook and LinkedIn.  Facebook is perhaps the most widely used authentication mechanism on the planet, so it is a natural fit for most social apps.  LinkedIn is another widely used platform for professional connections, so it would be more appropriate for work experience-based apps.

## Integrating Facebook authentication

The process of integrating with a social provider is a three-step process:

1. Sign up and configure a new app on the social provider.
2. Register the social provider in Azure AD B2C as an identity provider.
3. Update the sign-in/sign-up flow to use the new identity provider.

Once completed, there is no source code changes in the app itself.  Everything is configured in the backend.

### Create a Facebook application

To create a Facebook application, you need a Facebook account and you need to register that Facebook account as a developer account.  If you are one of the dozen or so people in the world that doesn't have a Facebook account, create one on [the Facebook site](https://www.facebook.com).  If needed, register for a Facebook developer account by signing in to [Facebook for developers](https://developers.facebook.com/) with your Facebook account.

!!! tip "Create a Facebook for developers identity
    You don't have to use your personal account for your development activities, and many people don't.  Register a new email address for your developer account (using one of the many email providers out there), then use that to register a new Facebook account.  If you are a commercial app developer, this also allows you to maintain a presence on Facebook separate from your personal activities.

Now, let's create a Facebook app:

1. Sign in to [Facebook for developers](https://developers.facebook.com).
2. Select **My Apps** > **Create App**.
3. Enter a **Display Name**.  Your contact email should be set up already.

    ![](img/social-1.png)

4. Click **Create App ID**.  This may require an online security check.
5. Select **Settings** > **Basic** from the left-hand menu.
6. Choose a **Category**.  
7. As needed, fill in other information, such as the privacy policy URL.  If this will be a "real app" (and not one for your own education), then you will also need to provide GDPR contact information and verify your business.

    !!! tip "Provider a Privacy Policy URL"
        You must provide a privacy policy URL before taking your app live.  This can be any
        URL.  An example URL: [https://account.microsoft.com/account/privacy](https://account.microsoft.com/account/privacy)

8. At the bottom of the page, click **Add Platform**.
9. On the **Select Platform** page, click **Website**.

    ![](img/social-2.png)

10. In the **Site URL** box, enter `https://tenant-name.b2clogin.com/`.  Replace `tenant-name` with the name of your Azure AD B2C tenant.
11. Click **Save Changes**.
12. Click **Dashboard** in the left-hand menu.
13. Scroll down until you see **Add a Product** heading.  Click **Set up** on the **Facebook Login** panel.
14. Select **Facebook Login** > **Settings** in the left-hand menu.
15. In the **Valid OAuth Redirect URIs** box, enter `https://tenant-name.b2clogin/tenant-name.onmicrosoft.com/oauth2/authresp`.  Replace `tenant-name` with the name of your Azure AD B2C tenant.

    ![](img/social-3.png)

16. Click **Save Changes**.
17. Click the **OFF** slider in the top menu bar to enable your app.

    ![](img/social-4.png)

Facebook will ask you if you are sure and then make your app live.

Before leaving the Facebook site, make a note of two values from the **Settings** > **Basic** page:

* The App ID
* The App Secret (click the **Show** button)

You will need these in the next step.

### Register Facebook with Azure AD B2C

The next step is to tell Azure AD B2C about the app you have just created on the Facebook side.  This is called "registering a new identity provider".  First off, sign in to the [Azure portal](https://portal.azure.com) and select the Azure AD B2C tenant if required.  You can do this by clicking on your account in the top-right corner, then selecting **Switch directory** if needed.

Now, let's configure Azure AD B2C:

* Select **All services** > **Azure AD B2C**.  (Use search for this!)
* Select **Identity providers**.
* Click **Add**.
* Enter `Facebook` as the name.
* Click **Identity provider type** > **Facebook**, then **OK**.
* Click **Set up this identity provider**.
* Enter the App ID in the **Client ID** box, and the App Secret in the **Client Secret** box.
* Click **OK**.
* Click **Create**.

### Update the sign-in flow

Now that Azure AD B2C knows about your Facebook app, you can configure your user flow to use the identity provider:

* Select **User flows (policies)**.
* Select the `B2C_1_Signin` user flow.
* Click **Identity providers**.  There are two links with this name, but they both go to the same place.
* Ensure the check-box next to **Facebook** is checked, then click **Save**.

![](img/social-5.png)

Before you continue, you may need to access the Facebook Graph or get other information in the claims that the social provider gave back.  To do this:

* Select **User flows (policies)**.
* Select th `B2C_1_Signin` user flow.
* Select **Application claims**.
* Check the **Identity Provider Access Token** box.
* Click **Save**.

![](img/social-6.png)

We'll use this later on to access the name and email address of the user.

You can now run your mobile app and sign in.  This time through, you will see a Facebook login button in addition to the username and password login.





