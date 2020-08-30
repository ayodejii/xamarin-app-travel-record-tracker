using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using TravulRecd.Model;
using TravulRecd.ViewModel.Command;

namespace TravulRecd.ViewModel
{
    public class RegisterViewModel : INotifyPropertyChanged
    {
        public RegisterCommand RegisterCommand { get; set; }

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

        public LoginNavigationCommand LoginNavCommand { get; }

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
                    Password = Password,
                    cPassword = cPassword
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
                    Password = Password,
                    cPassword = cPassword
                };
                OnPropertyChanged(nameof(Password));
            }
        }

        private string cpassword;
        public string cPassword
        {
            get { return cpassword; }
            set
            {
                if (cpassword == value) return;
                cpassword = value;
                User = new User()
                {
                    Username = Username,
                    Password = Password,
                    cPassword = cPassword
                };
                OnPropertyChanged(nameof(cPassword));
            }
        }
        #endregion


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public RegisterViewModel()
        {
            RegisterCommand = new RegisterCommand(this);
            User = new User();
            LoginNavCommand = new LoginNavigationCommand(this);
        }

        public async void Register()
        {
            var register = await User.Register(User);
            if (register.ToLower() == "success")
            {
                await App.Current.MainPage.Navigation.PushAsync(new LoginPage());
            }
            else
                await App.Current.MainPage.DisplayAlert("Register Failed", register, "Ok");
        }

        public async void goToLogin()
        {
            await App.Current.MainPage.Navigation.PushAsync(new LoginPage(), false);
        }
    }
}
