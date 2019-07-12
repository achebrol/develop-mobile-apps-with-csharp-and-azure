using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tailwind.Photos.Services
{
    public class AnalyticsService
    {
        private readonly string iosAppId = "cff85ae5-452c-4e12-a258-1c525c6f0b4f";
        private readonly string androidAppId = "ad7b66de-74ea-4f10-b61f-2d1b2fb31ea4";

        /// <summary>
        /// Lazy initializer for the identity manager (do not touch)
        /// </summary>
        private static readonly Lazy<AnalyticsService> lazy
            = new Lazy<AnalyticsService>(() => new AnalyticsService());

        /// <summary>
        /// Obtain a reference to the identity manager.
        /// </summary>
        private static AnalyticsService Instance
        {
            get { return lazy.Value; }
        }
        
        private AnalyticsService()
        {
            AppCenter.Start($"ios={iosAppId};android={androidAppId}",
                typeof(Analytics), typeof(Crashes));
        }

        private void EnsureInitialized()
        {
            // Does nothing other than ensure it is instantiated!
        }

        public static void Initialize()
        {
            Instance.EnsureInitialized();
        }

        public static void TrackLifecycleEvent(string lifecycleEvent)
        {
            Analytics.TrackEvent("LIFECYCLE", new Dictionary<string, string>
            {
                { "Event", lifecycleEvent }
            });
        }

        public static void TrackPageAppearing(string page)
        {
            Analytics.TrackEvent("PAGE", new Dictionary<string, string>
            {
                { "Event", "Appear" },
                { "Page", page }
            });
        }

        public static void TrackPageDisappearing(string page)
        {
            Analytics.TrackEvent("PAGE", new Dictionary<string, string>
            {
                { "Event", "Disappear" },
                { "Page", page }
            });
        }
    }
}
