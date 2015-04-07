using System;
using Xamarin.Forms;
//using ZeroFiveBit.Geolocation;

namespace GeolocationSample
{
    public class App : Application
    {
        public Label Label;

        public event Action Ready;

        public App()
        {
            Label = new Label
                {
                    XAlign = TextAlignment.Center,
                    Text = "Loading..."
                };

            // The root page of your application
            MainPage = new ContentPage
            {
                Content = new StackLayout
                {
                    VerticalOptions = LayoutOptions.Center,
                    Children =
                    {
                        Label
                    }
                }
            };
        }

        protected override void OnStart()
        {
            Ready();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

