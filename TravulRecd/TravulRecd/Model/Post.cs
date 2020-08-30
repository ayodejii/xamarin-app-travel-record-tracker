using SQLite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using TravulRecd.Helpers;

namespace TravulRecd.Model
{
    public class Post: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string id = string.Empty;
        public string Id
        {
            get => id;
            set
            {
                if (id  == value) return;
                id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        private string experience { get; set; }
        public string Experience
        {
            get => experience;
            set
            {
                if (experience  == value) return;
                experience = value;
                OnPropertyChanged(nameof(Experience));
            }
        }

        private string venue { get; set; }
        public string Venue
        {
            get => venue;
            set
            {
                if (venue  == value) return;
                venue = value;
                OnPropertyChanged(nameof(Venue));
            }
        }
        private string categoryId { get; set; }
        public string CategoryId
        {
            get => categoryId;
            set
            {
                if (categoryId  == value) return;
                categoryId = value;
                OnPropertyChanged(nameof(CategoryId));
            }
        }
        private string categoryName { get; set; }
        public string CategoryName
        {
            get => categoryName;
            set
            {
                if (categoryName  == value) return;
                categoryName = value;
                OnPropertyChanged(nameof(CategoryName));
            }
        }
        private string address { get; set; }
        public string Address
        {
            get => address;
            set
            {
                if (address == value) return;
                address = value;
                OnPropertyChanged(nameof(Address));
            }
        }
        private double latitude { get; set; }
        public double Latitude
        {
            get => latitude;
            set
            {
                latitude = value;
                OnPropertyChanged(nameof(Latitude));
            }
        }
        private double longitude { get; set; }
        public double Longitude
        {
            get => longitude;
            set
            {
                longitude = value;
                OnPropertyChanged(nameof(Longitude));
            }
        }
        private int distance { get; set; }
        public int Distance
        {
            get => distance;
            set
            {
                distance = value;
                OnPropertyChanged(nameof(Distance));
            }
        }
        private int userId { get; set; }
        public int UserId
        {
            get => userId;
            set
            {
                userId = value;
                OnPropertyChanged(nameof(UserId));
            }
        }

        private Venue postVenue;

        [JsonIgnore]
        public Venue PostVenue
        {
            get { return postVenue; }
            set 
            {
                if (postVenue == value) return;
                postVenue = value;

                if (postVenue.categories != null)
                {
                    var firstCategory = postVenue.categories.FirstOrDefault();
                    if (firstCategory != null)
                    {
                        CategoryId = firstCategory.id;
                        CategoryName = firstCategory.name;
                    }
                }

                if (postVenue.location != null)
                {
                    Address = postVenue.location.address;
                    Latitude = postVenue.location.lat;
                    Longitude = postVenue.location.lng;
                    Distance = postVenue.location.distance;
                }
                
                UserId = App.user.userId;
                Venue = postVenue.name;

                OnPropertyChanged(nameof(PostVenue));

            }
        }

        void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        internal static async Task<string> InsertPost(Post post)
        {
            string message;
            
            try
            {
                Uri uri = new Uri(App.baseUrl + "Posts");
                string json = JsonConvert.SerializeObject(post);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await App.http.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var newPost = JsonConvert.DeserializeObject<Post>(result);
                    if (newPost is null)
                        message = "Post Error";
                    message = "Success";
                }
                else
                {
                    message = "Server Error";
                }

            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return message;
        }

        internal static async Task<PostMessage> GetPostByUser()
        {
            var postList = new PostMessage();
            //using (var conn = new SQLiteConnection(App.DatabaseLocation))
            //{
            //    conn.CreateTable<Post>();
            //    postList = conn.Table<Post>().ToList();
            //}
            try
            {
                Uri uri = new Uri(String.Format(App.baseUrl + "Posts/{0}", App.user.userId));
                var response = await App.http.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    postList.Posts = JsonConvert.DeserializeObject<List<Post>>(result);
                    postList.message = "success";
                }
                else
                {
                    postList.message = "No Posts Found";
                }

            }
            catch (Exception ex)
            {
                postList.message = ex.Message;
            }
            return postList;
        }

        internal static async Task<Dictionary<string, int>> GetCategoryByCount()
        {
            var getPosts = (await GetPostByUser()).Posts;

            var categories = getPosts
                .OrderBy(x => x.CategoryId)
                .Select(x => x.CategoryName)
                .Distinct().ToList();

            var categoryByCount = new Dictionary<string, int>();

            foreach (var category in categories)
            {
                if (category is null) continue;
                var count = getPosts.Where(x => x.CategoryName == category).ToList().Count;
                categoryByCount.Add(category, count);
            }
            return categoryByCount;
        }

        public static async Task<PostMessage> GetPost()
        {
            var postList = new PostMessage();
            try
            {
                Uri uri = new Uri(App.baseUrl + "Posts");

                var response = await App.http.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    postList.Posts = JsonConvert.DeserializeObject<List<Post>>(result);
                    postList.message = "success";
                }
                else
                {
                    postList.message = "No Posts Found";
                }
            }
            catch (Exception ex)
            {
                postList.message = ex.Message;
            }
            return postList;
        }

        internal static async Task<bool> DeletePost(Post post)
        {
            bool status = false;
            try
            {
                Uri uri = new Uri(String.Format(App.baseUrl + "Posts/{0}/{1}", post.Id, App.user.userId));//Uri(App.baseUrl + "/" + post.Id + "/" +  App.user.userId);

                var response = await App.http.DeleteAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    status = true;
                }
                else
                {
                    status = false;
                }
            }
            catch (Exception ex)
            {
                status = false;
            }
            return status;
        }
    }
}
/*
 * 
 * public int Id { get; set; }
        public string Experience { get; set; }
        public string Venue { get; set; }
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Distance { get; set; }
        public string CategoryName { get; set; }
        public string CategoryId { get; set; }
        public int UserId { get; set; }
 * */
