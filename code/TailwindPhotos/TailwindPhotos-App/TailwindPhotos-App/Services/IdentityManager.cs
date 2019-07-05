using Microsoft.AppCenter.Crashes;
using Microsoft.Identity.Client;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Tailwind.Photos.Services
{
    public class IdentityManager
    {
        /// <summary>
        /// The ClientID is the Application ID found in the portal (https://go.microsoft.com/fwlink/?linkid=2083908).
        /// </summary>
        private readonly string ClientID = "b7761b83-3295-4196-a0ee-ffb70408dd89";

        /// <summary>
        /// The TenantID is for the directory and found in the portal as well.
        /// </summary>
        private readonly string TenantID = "a2558001-c7b4-4773-a899-bab2b97f6868";

        /// <summary>
        /// The redirect URI - this must be registered within the Azure Portal
        /// </summary>
        private readonly string RedirectUri = "msal-tailwinds-photos://auth";

        /// <summary>
        /// The level of access that we are requesting.
        /// </summary>
        private readonly string[] Scopes = { "User.Read" };

        /// <summary>
        /// Lazy initializer for the identity manager (do not touch)
        /// </summary>
        private static readonly Lazy<IdentityManager> lazy
            = new Lazy<IdentityManager>(() => new IdentityManager());

        private static IPublicClientApplication idp = null;
        public static object ParentWindow { get; set; }

        /// <summary>
        /// Obtain a reference to the identity manager.
        /// </summary>
        public static IdentityManager Instance
        {
            get { return lazy.Value; }
        }

        private IdentityManager()
        {
            idp = PublicClientApplicationBuilder
                .Create(ClientID)
                .WithRedirectUri(RedirectUri)
                .WithTenantId(TenantID)
                .Build();
        }

        public string AccessToken
        {
            get;
            private set;
        }

        public string Username
        {
            get;
            private set;
        }

        public async Task<Boolean> Signin()
        {
            try
            {
                var currentAccounts = await idp.GetAccountsAsync();
                var account = currentAccounts.FirstOrDefault();
                var result = await idp.AcquireTokenSilent(Scopes, account).ExecuteAsync();
                AccessToken = result.AccessToken;
                Username = result.Account.Username;
                return true;
            }
            catch (MsalUiRequiredException)
            {
                // Ignore this error - it's used to fall-through
            }

            // If we can silently acquire the token, then this is never reached.
            // If we get here, we need to pop up a UI
            try
            {
                var uiResult = await idp
                    .AcquireTokenInteractive(Scopes)
                    .WithParentActivityOrWindow(IdentityManager.ParentWindow)
                    .ExecuteAsync();
                AccessToken = uiResult.AccessToken;
                Username = uiResult.Account.Username;
                return true;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                return false;
            }
        }

        public async void Signout()
        {
            try
            {
                var accounts = await idp.GetAccountsAsync();
                while (accounts.Any())
                {
                    await idp.RemoveAsync(accounts.FirstOrDefault());
                    accounts = await idp.GetAccountsAsync();
                }
                AccessToken = null;
                Username = null;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
    }
}
