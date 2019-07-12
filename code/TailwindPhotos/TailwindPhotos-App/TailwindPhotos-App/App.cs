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

        public App(object parent = null)
        {
            IdentityManager.ParentWindow = parent;
            MainPage = new NavigationPage(new SplashScreen());
        }

        protected override void OnStart()
        {
            AnalyticsService.Initialize();
        }

        protected override void OnSleep()
        {
            base.OnSleep();
            AnalyticsService.TrackLifecycleEvent("Sleep");
        }

        protected override void OnResume()
        {
            base.OnResume();
            AnalyticsService.TrackLifecycleEvent("Resume");
            
        }
    }
}
