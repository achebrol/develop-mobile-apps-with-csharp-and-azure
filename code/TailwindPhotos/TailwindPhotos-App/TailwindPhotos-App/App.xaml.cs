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
            InitializeComponent();

            IdentityManager.ParentWindow = parent;

            MainPage = new NavigationPage(new SplashScreen())
            {
                BarBackgroundColor = (Color) App.Current.Resources["PrimaryColor"],
                BarTextColor = (Color) App.Current.Resources["PrimaryTextColor"]
            };
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