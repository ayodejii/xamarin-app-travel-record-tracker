using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using TravulRecd.Model;

namespace TravulRecd.ViewModel.Command
{
    public class RegisterCommand : ICommand
    {
        private readonly RegisterViewModel registerViewModel;

        public RegisterCommand(RegisterViewModel registerViewModel)
        {
            this.registerViewModel = registerViewModel;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            var user = parameter as User;
            if (user != null)
            {
                bool isEmpty = string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Password) || string.IsNullOrEmpty(user.cPassword);
                if (isEmpty is true)
                {
                    return false;
                }
                if (user.Password != user.cPassword)
                {
                    return false;
                }
                return true;
            }
            return false;
        }

        public void Execute(object parameter)
        {
            registerViewModel.Register();
        }
    }
}
