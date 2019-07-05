using Xamarin.Forms;
using Tailwind.Photos.Pages;

namespace Tailwind.Photos
{
    public class App : Application
    {
        public App()
        {
            MainPage = new NavigationPage(new SplashScreen());
        }
    }
}
