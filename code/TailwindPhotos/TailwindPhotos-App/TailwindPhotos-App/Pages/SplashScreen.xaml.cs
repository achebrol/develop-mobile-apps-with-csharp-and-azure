using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Tailwind.Photos.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tailwind.Photos.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SplashScreen : ContentPage
    {
        public SplashScreen()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            AnalyticsService.TrackPageAppearing(GetType().Name);

            // Try to get a token silently from the identity manager.
            // If it succeeds, then nothing more needs to be done -
            // we can move directly to the application.
            await IdentityManager.Instance.SilentlySignIn();
            if (IdentityManager.Instance.IsAuthenticated)
            {
                await NavigateToMainPage();
            }
            else
            {
                // Swap the activity indicator for the login button
                InitializerIndicator.IsRunning = false;
                InitializerIndicator.IsVisible = false;
                LoginButton.IsEnabled = true;
                LoginButton.IsVisible = true;
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            AnalyticsService.TrackPageDisappearing(GetType().Name);
        }

        async void OnLoginClicked(object sender, EventArgs args)
        {
            await IdentityManager.Instance.InteractivelySignin();
            if (IdentityManager.Instance.IsAuthenticated)
            {
                await NavigateToMainPage();
            }
        }

        private async Task NavigateToMainPage()
        {
            // We want the login page to be the new "root" of the navigation
            var root = Navigation.NavigationStack[0];
            Navigation.InsertPageBefore(new MainPage(), root);
            await Navigation.PopToRootAsync();
        }
    }
}