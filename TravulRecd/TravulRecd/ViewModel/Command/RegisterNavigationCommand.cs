using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace TravulRecd.ViewModel.Command
{
    public class RegisterNavigationCommand : ICommand
    {
        private readonly LoginViewModel loginViewModel;

        public RegisterNavigationCommand(LoginViewModel loginViewModel)
        {
            this.loginViewModel = loginViewModel;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            loginViewModel.goToRegister();
        }
    }
}
