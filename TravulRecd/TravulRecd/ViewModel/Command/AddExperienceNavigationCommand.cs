using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace TravulRecd.ViewModel.Command
{
    public class AddExperienceNavigationCommand : ICommand
    {
        private HomeViewModel homeViewModel;

        public event EventHandler CanExecuteChanged;

        public AddExperienceNavigationCommand(HomeViewModel homeViewModel)
        {
            this.homeViewModel = homeViewModel;
        }

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            homeViewModel.Execute();
        }
    }
}
