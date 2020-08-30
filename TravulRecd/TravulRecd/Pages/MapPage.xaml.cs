using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravulRecd.Model;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;
using Position = Xamarin.Forms.Maps.Position;

namespace TravulRecd
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {
        public MapPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            try
            {
                base.OnAppearing();

                var locator = CrossGeolocator.Current;
                locator.PositionChanged += Locator_PositionChanged;
                await locator.StartListeningAsync(TimeSpan.FromSeconds(1), 0.2);

                var loc = await locator.GetPositionAsync();

                //var loc = await Xamarin.Essentials.Geolocation.GetLocationAsync();
                //here we are setting the current location of the user to the place the map should show.
                locationMap.MoveToRegion(new MapSpan(new Xamarin.Forms.Maps.Position(loc.Latitude, loc.Longitude), 2, 2));

                var postList = (await Post.GetPostByUser()); //should be per user
                DisplayInMap(postList.Posts);
            }
            catch (Exception ex)
            {
            }
            
        }

        protected override async void OnDisappearing()
        {
            base.OnDisappearing();
            var locator = CrossGeolocator.Current;
            locator.PositionChanged -= Locator_PositionChanged;
            await locator.StopListeningAsync();
        }

        private void DisplayInMap(List<Post> postList)
        {
            try
            {
                foreach (var post in postList)
                {
                    if (!string.IsNullOrEmpty(post.Venue))
                    {
                        var position = new Position(post.Latitude, post.Longitude);

                        var pin = new Pin()
                        {
                            Type = PinType.SavedPin,
                            Position = position,
                            Address = post.Address,
                            Label = post.Venue,
                        };

                        locationMap.Pins.Add(pin);
                    }
                    
                }
            }
            catch (Exception ex)
            {
            }
            
        }

        private void Locator_PositionChanged(object sender, PositionEventArgs e)
        {
            //here we want to set the loaction the user moves to as the current location of the user and also to be displayed on the application
            //locationMap.MoveToRegion(MapSpan.FromCenterAndRadius
            //    (new Xamarin.Forms.Maps.Position(e.Position.Latitude, e.Position.Longitude),
            //    Distance.FromKilometers(100)));
            locationMap.MoveToRegion(new MapSpan(new Xamarin.Forms.Maps.Position(e.Position.Latitude, e.Position.Longitude), 2, 2));

        }
    }
}