using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravulRecd.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravulRecd.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        //User user;
        public RegisterPage()
        {
            InitializeComponent();
            //user = new User();
            //containerStackLayout.BindingContext = user;
        }

    }
}