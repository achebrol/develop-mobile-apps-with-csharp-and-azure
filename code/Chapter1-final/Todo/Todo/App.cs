using Todo.Views;
using Xamarin.Forms;

namespace Todo
{
    public class App : Application
    {
        public App()
        {
            Resources = new ResourceDictionary
            {
                { "primaryGreen", Color.FromHex("91CA47") },
                { "primaryDarkGreen", Color.FromHex("6FA22E") }
            };

            var nav = new NavigationPage(new TodoItemList())
            {
                BarBackgroundColor = (Color)App.Current.Resources["primaryGreen"],
                BarTextColor = Color.White
            };

            MainPage = nav;
        }
    }
}
