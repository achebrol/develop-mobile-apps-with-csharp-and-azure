using System;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tailwind.Photos.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex) {
                Debug.WriteLine("Error in XamlParseException");
            }
        }

        async void onProfileClicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new ProfilePage());
        }
    }
}