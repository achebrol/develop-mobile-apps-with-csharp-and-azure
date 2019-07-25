using System;
using System.Threading.Tasks;
using Tailwind.Photos.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tailwind.Photos.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();

            var authUser = IdentityManager.Instance.AuthenticatedUser;
            nameField.Text = authUser.Name;
            emailField.Text = authUser.Email;
        }

        async void OnSignoutClicked(object sender, EventArgs args)
        {
            IdentityManager.Instance.Signout();
            if (!IdentityManager.Instance.IsAuthenticated)
            {
                await NavigateToSplashScreen();
            }
        }

        private async Task NavigateToSplashScreen()
        {
            // We want the splash screen to be the new "root" of the navigation
            var root = Navigation.NavigationStack[0];
            Navigation.InsertPageBefore(new SplashScreen(), root);
            await Navigation.PopToRootAsync();
        }
    }
}