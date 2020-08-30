using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using TravulRecd.Model;
using TravulRecd.ViewModel.Command;

namespace TravulRecd.ViewModel
{
    public class NewTravelViewModel: INotifyPropertyChanged
    {
        public NewTravelViewModel()
        {
            InsertPostCommand = new SavePostCommand(this);
            Post = new Post();
            PostVenue = new Venue();
        }
        #region commandProperties
        public SavePostCommand InsertPostCommand { get; set; }
        #endregion
        #region ModelProperties

        private Post post;
        public Post Post
        {
            get { return post; }
            set 
            { 
                post = value;
                OnPropertyChanged(nameof(Post));
            }
        }

        private string experience;

        public string Experience
        {
            get { return experience; }
            set 
            { 
                //if(experience == value) return;

                experience = value;
                Post = new Post()
                {
                    Experience = Experience,
                    PostVenue = postVenue
                };
                OnPropertyChanged(nameof(Experience));
            }
        }

        private Venue postVenue;

        public Venue PostVenue
        {
            get { return postVenue; }
            set 
            {
                postVenue = value;
                Post = new Post()
                {
                    Experience = Experience,
                    PostVenue = postVenue
                };
                OnPropertyChanged(nameof(PostVenue));
            }
        }
        #endregion
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async void savePost()
        {
            try
            {
                string message = await Post.InsertPost(Post);
                if (message.ToLower() == "success")
                {
                    await App.Current.MainPage.DisplayAlert("Success", "Experience Added", "Ok");
                }
                else
                    await App.Current.MainPage.DisplayAlert("Failed", "Insert Failed", "Ok");
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "Ok");
            }
        }
    }
}
