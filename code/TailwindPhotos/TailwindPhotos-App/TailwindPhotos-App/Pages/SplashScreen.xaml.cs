using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;
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

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Analytics.TrackEvent("PAGE", new Dictionary<string, string>
            {
                { "Event", "Appear" },
                { "Page", GetType().Name }
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            Analytics.TrackEvent("PAGE", new Dictionary<string, string>
            {
                { "Event", "Disappear" },
                { "Page", GetType().Name }
            });
        }

        void OnCrashMeClicked(object sender, EventArgs args)
        {
            Crashes.GenerateTestCrash();
        }

        async void OnLoginClicked(object sender, EventArgs args)
        {
            Analytics.TrackEvent("Click", new Dictionary<string, string>
            {
                { "Event", "Login" },
                { "Page", GetType().Name }
            });
            await IdentityManager.Instance.Signin();
        }
    }
}