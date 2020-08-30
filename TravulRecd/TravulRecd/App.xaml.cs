using System;
using System.Net.Http;
using TravulRecd.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravulRecd
{
    public partial class App : Application
    {
        public static string DatabaseLocation;
        public static HttpClient http = new HttpClient();
        public static string baseUrl = "https://miscapi.azurewebsites.net/api/";
        public static Helpers.Account user;

        public App()
        {
            InitializeComponent();
            user = new Helpers.Account();
            MainPage = new NavigationPage(new LoginPage());
        }

        public App(string databaseLocation)
        {
            InitializeComponent();

            MainPage = new NavigationPage(new LoginPage());
            DatabaseLocation = databaseLocation;
        }
        protected override void OnStart()
        {
            // Handle when your app starts
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
