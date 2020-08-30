using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TravulRecd.Model
{
    public class User:  INotifyPropertyChanged
    {
        //public int UserId { get; set; }
        //public string Username { get; set; }
        //public string Password { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

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
                OnPropertyChanged(nameof(Password));
            }
        }

        private string cpassword;

        [JsonIgnore]
        public string cPassword
        {
            get { return cpassword; }
            set
            {
                if (cpassword == value) return;
                cpassword = value;
                OnPropertyChanged(nameof(cPassword));
            }
        }

        void OnPropertyChanged(string properyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(properyName));
        }


        internal async static Task<Helpers.Account> Login(User user)
        {
            var login = new Helpers.Account();
            try
            {
                Uri uri = new Uri(App.baseUrl + "Users/Login");
                string json = JsonConvert.SerializeObject(user);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await App.http.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var userLogin = JsonConvert.DeserializeObject<User>(result);
                    login.username = user.Username;
                    login.password = user.Password;
                    login.userId = userLogin.UserId;
                    login.message = "Success";
                }
                else
                {
                    login.message = "Invalid User";
                }

            }
            catch (Exception ex)
            {
                login.message = ex.Message;
            }
            return login;
        }
        internal async static Task<string> Register(User user) 
        {
            string register = string.Empty;
            try
            {
                Uri uri = new Uri(App.baseUrl + "Users/Register");
                string json = JsonConvert.SerializeObject(user);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await App.http.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var userLogin = JsonConvert.DeserializeObject<User>(result);
                    //if (userLogin is null)
                    //    register = "Account already exists";
                    //register.username = username;
                    //register.password = password;
                    //register.userId = userLogin.UserId;
                    register = "Success";
                }
                else
                {
                    register = "Server Error";
                }

            }
            catch (Exception ex)
            {
                register = ex.Message;
            }
            return register;
        }

        
    }
}
