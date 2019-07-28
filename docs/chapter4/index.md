# Authentication

One of the very first things you will want to do when developing a mobile app is to provide your users with a unique experience that is tailored to them.  For our example photos application, this can be as simple as ensuring that only the logged in user sees their own photos.  In more complex situations (which we can explore later), you may wish to allow sharing, group rules, role-based access controls, and more.  In all these cases, properly identifying your users using the phone is the starting point.

Authentication provides a process by which the user that is using the mobile device can be identified securely.  This is done (generally) by entering a username and password.  However, modern systems can also provide [multi-factor authentication](https://en.wikipedia.org/wiki/Multi-factor_authentication), send you a text message to a registered device, or use your fingerprint or face as the password.

## The OAuth Process

The de-facto standard for authenticating users these days is a process called OAuth.  OAuth is not an authentication mechanism in its own right.  It is used to route the authentication request to the right place and to verify that the authentication took place.  There are three actors in the OAuth protocol:

* The **Client** is the application attempting to get access to the resource - in most cases, this will be your mobile app.
* The **Resource** is the provider of information that your client is attempting to access - in most cases, this will be the mobile backend.
* The **Identity Provider** (or IdP) is the service that is responsible for authenticating the client.

At the end of the process, a cryptographically signed token is minted and passed to the client.  This token is added to every request made by the client to the resource to securely identify the user.  The token (also known as an **access token**) has information about the user (known as **claims**) that the resource can use to make authorization determinations.  There are two other types of token that may be produced.  An **identity token** provides information about the user, but is not used to access resources.  A **refresh token** is used to get another access token when the first access token expires.  Access tokens are generally short-lived (sometimes only for seconds), whereas refresh tokens are long-lived (Facebook refresh tokens last for months, for example).

To authenticate with the IdP, a client app (like your mobile app) will use a native SDK to provide the login flow.  The SDK is provided by the identity provider.  Thus, if you wish to enable Facebook authentication, you need to go looking for an SDK for Facebook Login produced by Facebook.

## Next Steps

Authentication is one of those areas with a lot of terminology and concepts that you need to understand in order to diagnose what is happening and why it is happening when things go wrong.  There are [some great articles](https://oauth.net/2/native-apps/) on the internet along with [all the specifications](https://oauth.net/2/).  Because you are dealing with user secrets, you want to be especially careful so you don't inadvertently create an insecure environment.

In this chapter, we are going to look at various authentication methods and see how we can integrate them in such a way that any service that you write that is hosted within Azure can use the authentication.  However, it is likely that your mobile app will fall into one of four buckets:

1. [Enterprise credentials via Azure Active Directory](aad.md).
2. [Username and password authentication via Azure Active Directory B2C](b2c.md).
3. [Social media authentication via Facebook, Twitter, Google, or LinkedIn](social.md).
4. Custom authentication methods

Custom authentication methods tend to be more advanced topics.  They include:

* Using biometrics for authentication.
* Custom (native) UI.
* Using social provider SDKs.

We'll take a look at these advanced topics later in the book.

