using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using TravulRecd.Model;
using TravulRecd.Pages;
using TravulRecd.ViewModel.Command;

namespace TravulRecd.ViewModel
{
    public class LoginViewModel: INotifyPropertyChanged
    {

        public LoginViewModel()
        {
            User = new User();
            loginCommand = new LoginCommand(this);
            navigateToRegister = new RegisterNavigationCommand(this);

        }

        #region commandProperties
        public LoginCommand loginCommand { get; set; }
        public RegisterNavigationCommand navigateToRegister { get; set; }
        #endregion

        #region ModelProperties
        private User user;
        public User User
        {
            get { return user; }
            set
            {
                user = value;
                OnPropertyChanged(nameof(User));
            }
        }

        private int userId;

        public int UserId
        {
            get { return userId; }
            set
            {
                userId = value;
                OnPropertyChanged(nameof(UserId));
            }
        }
        private string username;

        public string Username
        {
            get { return username; }
            set
            {
                if (username == value) return; //if the value being set/typed in is same as the one in the variable username already
                username = value; //the variable username is only being updated to value here, it was an empty string before.
                User = new User()
                {
                    Username = Username,
                    Password = Password
                };

                OnPropertyChanged(nameof(Username));
            }
        }
        private string password;

        public string Password
        {
            get { return password; }
            set
            {
                if (password == value) return;
                password = value;
                User = new User()
                {
                    Username = Username,
                    Password = Password
                };
                OnPropertyChanged(nameof(Password));
            }
        }

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async void Login()
        {
            //bool isEmpty = string.IsNullOrEmpty(User.Username) || string.IsNullOrEmpty(User.Password);
            //if (isEmpty is true)
            //{
            //    await App.Current.MainPage.DisplayAlert("Login Failed", "Please Fill All Fields", "Ok");
            //    return;
            //}
            var login = await User.Login(User);
            if (login.message.ToLower() == "success")
            {
                App.user = login;
                await App.Current.MainPage.Navigation.PushAsync(new HomePage(), false);
            }
            else
                await App.Current.MainPage.DisplayAlert("Login Failed", login.message, "Ok");
        }
        public async void goToRegister()
        {
            await App.Current.MainPage.Navigation.PushAsync(new RegisterPage(), false);
        }
    }
}
