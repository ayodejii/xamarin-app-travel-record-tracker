using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravulRecd.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravulRecd
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : TabbedPage
    {
        HomeViewModel homeViewModel;
        public HomePage()
        {
            InitializeComponent();
            homeViewModel = new HomeViewModel();
            BindingContext = homeViewModel;
        }

    }
}