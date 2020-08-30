using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravulRecd.Model;
using TravulRecd.Pages;
using TravulRecd.ViewModel;
using Xamarin.Forms;

namespace TravulRecd
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(true)]
    public partial class LoginPage : ContentPage
    {
        //private LoginViewModel login;
        public LoginPage()
        {
            InitializeComponent();

            var assembly = typeof(LoginPage);
            iconImg.Source = ImageSource.FromResource("TravulRecd.Assets.images.aircraft.png", assembly);
            //login = new LoginViewModel();
            //BindingContext = login;
        }

        //private async void GoToRegister_Clicked(object sender, EventArgs e)
        //{
        //    await Navigation.PushAsync(new RegisterPage());
        //}
    }
}
