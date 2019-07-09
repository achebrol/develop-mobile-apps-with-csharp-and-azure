using Xamarin.Forms;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Tailwind.Photos.Pages;
using System.Collections.Generic;
using Tailwind.Photos.Services;

namespace Tailwind.Photos
{
    public class App : Application
    {
        private readonly string iosAppId     = "cff85ae5-452c-4e12-a258-1c525c6f0b4f";
        private readonly string androidAppId = "ad7b66de-74ea-4f10-b61f-2d1b2fb31ea4";

        public App(object parent = null)
        {
            IdentityManager.ParentWindow = parent;
            MainPage = new NavigationPage(new SplashScreen());
        }

        protected override void OnStart()
        {
            AppCenter.Start(
                $"ios={iosAppId};android={androidAppId}",
                typeof(Analytics), typeof(Crashes)
            );
        }

        protected override void OnSleep()
        {
            base.OnSleep();
            Analytics.TrackEvent("LIFECYCLE", new Dictionary<string, string> {
                { "Event", "Sleep" }
            });
        }

        protected override void OnResume()
        {
            base.OnResume();
            Analytics.TrackEvent("LIFECYCLE", new Dictionary<string, string>
            {
                { "Event", "Resume" }
            });
        }
    }
}
