using System;
using System.Collections.Generic;
using System.Text;
using TravulRecd.ViewModel.Command;

namespace TravulRecd.ViewModel
{
    public class HomeViewModel
    {
        public AddExperienceNavigationCommand NavCommand { get; set; }
        public HomeViewModel()
        {
            NavCommand = new AddExperienceNavigationCommand(this);
        }

        public async void Execute()
        {
            await App.Current.MainPage.Navigation.PushAsync(new NewTravelPage(), false);
        }
    }
}
