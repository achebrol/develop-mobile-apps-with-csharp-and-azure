using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;
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
                { "Page", this.GetType().Name }
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            Analytics.TrackEvent("PAGE", new Dictionary<string, string>
            {
                { "Event", "Disappear" },
                { "Page", this.GetType().Name }
            });
        }

        void OnCrashMeClicked(object sender, EventArgs args)
        {
            Crashes.GenerateTestCrash();
        }
    }
}