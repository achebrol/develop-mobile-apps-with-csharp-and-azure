using Tailwind.Photos.Pages;
using Tailwind.Photos.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tailwind.Photos
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class App : Application
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