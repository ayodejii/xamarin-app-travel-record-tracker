using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace TravulRecd.ViewModel.Command
{
    public class LoginNavigationCommand : ICommand
    {
        private RegisterViewModel registerViewModel;

        public LoginNavigationCommand(RegisterViewModel registerViewModel)
        {
            this.registerViewModel = registerViewModel;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            registerViewModel.goToLogin();
        }
    }
}
