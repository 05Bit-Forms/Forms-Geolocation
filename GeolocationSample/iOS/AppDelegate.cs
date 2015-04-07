using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

using Xamarin.Forms;
using ZeroFiveBit.Geolocation;

namespace GeolocationSample.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        App app;

        IGeolocator geolocator;

        public override bool FinishedLaunching(UIApplication uiApplication, NSDictionary launchOptions)
        {
            global::Xamarin.Forms.Forms.Init();
            global::ZeroFiveBit.Geolocation.iOS.Geolocator.Init();

            app = new App();
            app.Ready += OnAppReady;
            LoadApplication(app);

            return base.FinishedLaunching(uiApplication, launchOptions);
        }

        void OnAppReady ()
        {
            // This code CAN be placed to shared part,
            // the reason we didn't do that here is
            // strange bug, 

            app.Label.Text = "Waiting for location...";

            geolocator = DependencyService.Get<IGeolocator>();

            geolocator.PositionChanged += (sender, e) => {
                app.Label.Text = String.Format("Lat: {0}, Lng: {1}",
                    e.Position.Latitude, e.Position.Longitude);
            };

            geolocator.PositionError += (sender, e) => {
                app.Label.Text = "Update location error!";
            };

            if (geolocator.IsGeolocationAvailable || geolocator.IsGeolocationEnabled)
            {
                geolocator.StartListening(10, 200);
            }
        }
    }
}