
using System.Net;
using System;
using System.Globalization;

namespace Maui_01
{
    public partial class MainPage : ContentPage
    {
        
        private bool _isCheckingLocation;
        

        public MainPage()
        {
            InitializeComponent();
        }

        public async void OnGeolocalizzaClicked(object sender, EventArgs e)
        {
            string citta = città.Text;
            
            try
            {
                
                
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            var permessi = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();

            if(permessi == PermissionStatus.Granted)
            {
                await GetCurrentLocation(citta);
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Diritto negato",
                    "Non avete concesso il permesso di accedere alla tua posizione.", "OK");
                var richiesti = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
                if(richiesti == PermissionStatus.Granted)
                {
                    await GetCurrentLocation(citta);
                }
                else
                {
                    if(DeviceInfo.Platform == DevicePlatform.iOS)
                    {
                        await App.Current.MainPage.DisplayAlert("Richiesta geolocalizzazione",
                    "Geolocalizzazione necessaria", "OK");
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Richiesta geolocalizzazione",
                    "Geolocalizzazione necessaria", "OK");
                    }
                }
            }


        }

        

        public async Task GetCurrentLocation(string city)
        {
            try
            {
                _isCheckingLocation = true;

                GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Best, TimeSpan.FromSeconds(10));

                



                IEnumerable<Location> locations = await Geocoding.Default.GetLocationsAsync(city);
                Location location = locations?.FirstOrDefault();
                if (location != null)
                    await App.Current.MainPage.DisplayAlert("GEOCALIZZATO!", $"La {city} ha Longitudine: {location.Longitude} e Latitudine: {location.Latitude}","OK");
            }
            // Catch one of the following exceptions:
            //   FeatureNotSupportedException
            //   FeatureNotEnabledException
            //   PermissionException
            catch (Exception ex1)
            {
                Console.WriteLine($"Processing failed: {ex1.Message}");
            }
            
            finally
            {
                _isCheckingLocation = false;
            }
        }

        
    }
}