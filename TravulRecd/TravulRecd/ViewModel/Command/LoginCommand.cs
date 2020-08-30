using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using TravulRecd.Model;

namespace TravulRecd.ViewModel.Command
{
    public class LoginCommand : ICommand
    {
        private readonly LoginViewModel loginViewModel;

        public LoginCommand(LoginViewModel loginViewModel)
        {
            this.loginViewModel = loginViewModel;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            var user = parameter as User;
            if (user is null)
                return false;
            bool isEmpty = string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Password);
            if (isEmpty is true)
                return false;
            return true;
        }

        public void Execute(object parameter) => loginViewModel.Login();


    }
}
