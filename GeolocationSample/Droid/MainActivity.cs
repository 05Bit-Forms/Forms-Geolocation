using System;
using Android.App;
using Android.Content.PM;
using Android.Gms.Location;
using Android.Gms.Common;
using Android.Gms.Common.Apis;
using Android.Util;
using Android.Locations;
using Android.Widget;
using Android.OS;

namespace GeolocationSample.Droid
{
    [Activity(Label = "GeolocationSample.Droid", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity,
                                IGoogleApiClientConnectionCallbacks,
                                IGoogleApiClientOnConnectionFailedListener,
                                Android.Gms.Location.ILocationListener 
    {
        IGoogleApiClient locClient;
        LocationRequest locRequest;

        bool _isGooglePlayServicesInstalled;

        App app;

        #region Lifecycle

        protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            app = new App();

            app.Ready += AppReady;

            LoadApplication(app);
        }

        void AppReady()
        {
            _isGooglePlayServicesInstalled = IsGooglePlayServicesInstalled ();

            if (_isGooglePlayServicesInstalled)
            {
                locRequest = new LocationRequest();

                var builder = new GoogleApiClientBuilder(this);
                builder.AddApi(LocationServices.Api);
                builder.AddConnectionCallbacks(this);
                builder.AddOnConnectionFailedListener(this);
                locClient = builder.Build();
            }
            else
            {
                Log.Error ("OnCreate", "Google Play Services is not installed");
                Toast.MakeText(this, "Google Play Services is not installed", ToastLength.Long).Show();
                Finish();
            }
        }

        bool IsGooglePlayServicesInstalled()
        {
            int queryResult = GooglePlayServicesUtil.IsGooglePlayServicesAvailable(this);
            if (queryResult == ConnectionResult.Success)
            {
                Log.Info("MainActivity", "Google Play Services is installed on this device.");
                return true;
            }

            if (GooglePlayServicesUtil.IsUserRecoverableError (queryResult))
            {
                string errorString = GooglePlayServicesUtil.GetErrorString(queryResult);
                Log.Error("ManActivity", "There is a problem with Google Play Services on this device: {0} - {1}",
                    queryResult, errorString);

                // Show error dialog to let user debug google play services
            }
            return false;
        }

        protected override void OnResume()
        {
            base.OnResume();

            Log.Debug("OnResume", "OnResume called, reconnecting...");

            locClient.Connect();
        }

        protected override void OnPause()
        {
            base.OnPause();

            Log.Debug("OnPause", "OnPause called, stopping location updates");

            if (locClient.IsConnected)
            {
                // stop location updates, passing in the LocationListener
                LocationServices.FusedLocationApi.RemoveLocationUpdates(locClient, this);

                locClient.Disconnect ();
            }
        }

        #endregion

        #region ILocationListener

        public void OnLocationChanged(Location location)
        {
            // This method returns changes in the user's location if they've been requested

            // You must implement this to implement the Android.Gms.Locations.ILocationListener Interface
            Log.Debug ("LocationClient", "Location updated");

            app.Label.Text = String.Format("Lat: {0}, Lng: {1}, Provider: {2}",
                location.Latitude, location.Longitude, location.Provider);
        }

        #endregion

        #region IGoogleApiClientConnectionCallbacks

        public void OnConnected(Bundle bundle)
        {
            // This method is called when we connect to the LocationClient. We can start location updated directly form
            // here if desired, or we can do it in a lifecycle method, as shown above 

            // You must implement this to implement the IGooglePlayServicesClientConnectionCallbacks Interface
            Log.Info("LocationClient", "Now connected to client");

            // Setting location priority to PRIORITY_HIGH_ACCURACY (100)
            locRequest.SetPriority(100);

            // Setting interval between updates, in milliseconds
            // NOTE: the default FastestInterval is 1 minute. If you want to receive location updates more than 
            // once a minute, you _must_ also change the FastestInterval to be less than or equal to your Interval
            locRequest.SetFastestInterval(500);
            locRequest.SetInterval(1000);

            Log.Debug("LocationRequest", "Request priority set to status code {0}, interval set to {1} ms", 
                locRequest.Priority.ToString(), locRequest.Interval.ToString());

            // pass in a location request and LocationListener
            LocationServices.FusedLocationApi.RequestLocationUpdates(locClient, locRequest, this);

            // In OnLocationChanged (below), we will make calls to update the UI
            // with the new location data
        }

        public void OnConnectionSuspended(int cause)
        {
            Log.Debug ("OnPause", "OnPause called, stopping location updates");

            if (locClient.IsConnected)
            {
                // stop location updates, passing in the LocationListener
                LocationServices.FusedLocationApi.RemoveLocationUpdates(locClient, this);

                locClient.Disconnect ();
            }
        }

        #endregion

        #region IGoogleApiClientOnConnectionFailedListener

        public void OnConnectionFailed(ConnectionResult result)
        {
            // This method is used to handle connection issues with the Google Play Services Client (LocationClient). 
            // You can check if the connection has a resolution (bundle.HasResolution) and attempt to resolve it

            // You must implement this to implement the IGooglePlayServicesClientOnConnectionFailedListener Interface
            Log.Info("LocationClient", "Connection failed, attempting to reach google play services");
        }

        void IGooglePlayServicesClientOnConnectionFailedListener.OnConnectionFailed(ConnectionResult result)
        {
            // This method is used to handle connection issues with the Google Play Services Client (LocationClient). 
            // You can check if the connection has a resolution (bundle.HasResolution) and attempt to resolve it

            // You must implement this to implement the IGooglePlayServicesClientOnConnectionFailedListener Interface
            Log.Info("LocationClient", "Connection failed, attempting to reach google play services");
        }

        #endregion
    }
}

