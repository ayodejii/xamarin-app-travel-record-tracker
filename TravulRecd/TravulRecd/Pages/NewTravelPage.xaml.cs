using Plugin.Geolocator;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using TravulRecd.Model;
using TravulRecd.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravulRecd
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewTravelPage : ContentPage
    {
        //Post post;
        public NewTravelPage()
        {
            InitializeComponent();
            //post = new Post();
            //BindingContext = new NewTravelViewModel();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var location = CrossGeolocator.Current;
            var position = await location.GetPositionAsync(TimeSpan.FromSeconds(1));

            var venues = await Venue.GetVenues(position.Latitude, position.Longitude);
            venueListView.ItemsSource = venues;

        }


    }
}