using System;
using Tailwind.Photos.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tailwind.Photos.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        async void onProfileClicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new ProfilePage());
        }
    }
}